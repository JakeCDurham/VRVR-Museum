using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MenuTransport : MonoBehaviour
{
    [SerializeField] SteamVR_Action_Boolean menuAction;
    private SteamVR_Behaviour_Pose pose = null;
    [SerializeField] private GameObject uiPointer;
    [SerializeField] private GameObject menuMarker;
    [SerializeField] private GameObject returnMarker;
    private bool inMenuRoom;
    [SerializeField] private float fadeTime;

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuAction.GetStateDown(pose.inputSource))
        {
            Transport();
        }
    }

    public void Transport()
    {
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        if (!inMenuRoom)
        {
            Vector3 translateVec = menuMarker.transform.position - groundPosition;
            returnMarker.transform.position = groundPosition;
            //uiPointer.SetActive(true);
            StartCoroutine(MoveRig(cameraRig, translateVec));
        }
        else
        {
            Vector3 translateVec = returnMarker.transform.position - groundPosition;
            //uiPointer.SetActive(false);
            StartCoroutine(MoveRig(cameraRig, translateVec));
        }

        inMenuRoom = !inMenuRoom;
    }
    
    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        SteamVR_Fade.Start(Color.black, fadeTime, true);
        yield return new WaitForSeconds(fadeTime);
        cameraRig.position += translation;
        SteamVR_Fade.Start(Color.clear, fadeTime, true);
    }
}
