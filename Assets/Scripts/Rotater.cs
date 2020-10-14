using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Rotater : MonoBehaviour
{
    [SerializeField] public SteamVR_Action_Boolean leftAction;
    [SerializeField] public SteamVR_Action_Boolean rightAction;
    private SteamVR_Behaviour_Pose pose;
    [SerializeField] private float rotateAngle = 15f;
    [SerializeField] private GameObject Player;

    private void Start()
    {
        pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        if (leftAction.GetStateDown(pose.inputSource))
        {
            Player.transform.Rotate(new Vector3(0, -rotateAngle, 0));
        }
        if (rightAction.GetStateDown(pose.inputSource))
        {
            Player.transform.Rotate(new Vector3(0, rotateAngle, 0));
        }
    }
}
