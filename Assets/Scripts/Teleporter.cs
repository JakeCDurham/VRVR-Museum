using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    public SteamVR_Action_Boolean teleportAction;
    private SteamVR_Behaviour_Pose pose = null;
    private bool hasPosition = false;
    private bool isTeleporting = false;
    [SerializeField] private float fadeTime = 0.5f;
    private bool showPointer = false;
    private LineRenderer lineRenderer;
    [Range(1,1000000)][SerializeField] private float archSmoothness = 500f;
    [Range(2,10000)][SerializeField] private int archResolution = 100;
    [HideInInspector] public bool usingUI = false;
    

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = archResolution + 1;
    }

    // Update is called once per frame
    void Update()
    {
        hasPosition = UpdatePointer();
        pointer.SetActive(hasPosition && showPointer && !usingUI);
        
        if (usingUI)
        {
            showPointer = false;
            return;
        }
        
        if (teleportAction.GetStateDown(pose.inputSource))
        {
            showPointer = true;
        }
        
        if (teleportAction.GetStateUp(pose.inputSource) && showPointer)
        {
            showPointer = false;
            TryTeleport();
        }
    }

    private void TryTeleport()
    {
        if (!hasPosition || isTeleporting)
        {
            return;
        }

        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;
        
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVec = pointer.transform.position - groundPosition;
        
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);

        StartCoroutine(MoveRig(cameraRig, translateVec));
    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        isTeleporting = true;
        SteamVR_Fade.Start(Color.black, fadeTime, true);
        yield return new WaitForSeconds(fadeTime);
        cameraRig.position += translation;
        SteamVR_Fade.Start(Color.clear, fadeTime, true);
        isTeleporting = false;
    }

    private bool UpdatePointer()
    {
        Ray ray = new Ray(transform.position, (transform.forward + transform.up * -1).normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (showPointer && !usingUI)
            {
                lineRenderer.startWidth = 0.01f;
                lineRenderer.endWidth = 0.1f;
                //lineRenderer.SetPosition(0, transform.position);
                //lineRenderer.SetPosition(1, pointer.transform.position);
                // y=ai^2+bi+c
                // x = delta x * i
                // z = delta z * i
                float deltaX = (pointer.transform.position.x - transform.position.x) / archResolution;
                float deltaZ = (pointer.transform.position.z - transform.position.z) / archResolution;
                float h = transform.position.y - pointer.transform.position.y;
                float l = Vector3.Distance(
                    new Vector3(transform.position.x, pointer.transform.position.y, transform.position.z),
                    pointer.transform.position);
                float a = -(h / archSmoothness * l);
                float b = -(h / l) + -a * l;
                float c = h;
                Vector3 origin = new Vector3(transform.position.x,  pointer.transform.position.y , transform.position.z);
                for (int i = 0; i < archResolution; i++)
                {
                    float x = i * deltaX;
                    float y = a * Mathf.Pow(i * l/archResolution, 2) + b * i * l/archResolution + c;
                    float z = i * deltaZ;
                    lineRenderer.SetPosition(i, origin + new Vector3(x, y, z));
                }
                lineRenderer.SetPosition(archResolution, pointer.transform.position);
            }
            else
            {
                for (int i = 0; i < archResolution + 1; i++)
                {
                    lineRenderer.SetPosition(i, transform.position);
                }
            }

            pointer.transform.position = hit.point;
            return true;
        }

        for (int i = 0; i < archResolution + 1; i++)
        {
            lineRenderer.SetPosition(i, transform.position);
        }
        return false;
    }
}
