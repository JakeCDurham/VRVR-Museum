using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class UIPointer : MonoBehaviour
{
    public float defaultLength = 5f;
    public GameObject dot;
    public UIInput uiInput;
    public SteamVR_Action_Boolean RightAction;
    public SteamVR_Action_Boolean LeftAction;
    [SerializeField] private SteamVR_Behaviour_Pose Leftpose = null;
    [SerializeField] private SteamVR_Behaviour_Pose Rightpose = null;

    private LineRenderer linerenderer = null;
    private void Awake()
    {
        linerenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();

        if (LeftAction.GetStateDown(Leftpose.inputSource))
        {
            transform.parent = Leftpose.gameObject.transform;
            transform.localPosition = new Vector3(0,0,0);
            transform.localRotation = new Quaternion();
            transform.localEulerAngles = new Vector3(45, 0, 0);
        }
        else if (RightAction.GetStateDown(Rightpose.inputSource))
        {
            transform.parent = Rightpose.gameObject.transform;
            transform.localPosition = new Vector3(0,0,0);
            transform.localRotation = new Quaternion();
            transform.localEulerAngles = new Vector3(45, 0, 0);
        }
    }

    private void UpdateLine()
    {
        //get distance
        PointerEventData data = uiInput.getData();
        float length = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;
        
        //Raycast
        RaycastHit hit = CreateRaycast(length);

        //Default
        Vector3 targetPosition = transform.position + (transform.forward * length);

        //or hit
        if (hit.collider != null)
        {
            targetPosition = hit.point;
        }

        //move the dot
        dot.transform.position = targetPosition;

        //set linerenderer.
        linerenderer.SetPosition(0, transform.position);
        linerenderer.SetPosition(1, targetPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, defaultLength);
        
        return hit;
    }
}
