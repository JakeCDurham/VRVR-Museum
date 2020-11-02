using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class TestButton : MonoBehaviour
{
    public SteamVR_Action_Boolean testAction;
    private SteamVR_Behaviour_Pose pose;
    [SerializeField] private UnityEvent testEvent;
    // Start is called before the first frame update
    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        if (testAction.GetStateDown(pose.inputSource))
        {
            testEvent.Invoke();
        }
    }
}
