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

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        hasPosition = UpdatePointer();
        pointer.SetActive(hasPosition);

        if (teleportAction.GetStateUp(pose.inputSource))
        {
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
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            pointer.transform.position = hit.point;
            return true;
        }
        return false;
    }
}
