using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MyHand : MonoBehaviour
{
    [SerializeField] public SteamVR_Action_Boolean grabAction;
    private SteamVR_Behaviour_Pose pose;
    private FixedJoint joint;
    private bool usedInteract;

    private MyInteractable currentInteractable;
    private List<MyInteractable> contactInteractables = new List<MyInteractable>();

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
        usedInteract = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (grabAction.GetStateDown(pose.inputSource))
        {
            if (!usedInteract) {
              Pickup();
            }
        }

        if (grabAction.GetStateUp(pose.inputSource))
        {
            if (usedInteract) {
                usedInteract = false;
            } else {
                Drop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check interactable tag and that collider is on the specific object
        if (other.gameObject.CompareTag("interactable"))
        {
            contactInteractables.Add(other.GetComponent<MyInteractable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("interactable"))
        {
            contactInteractables.Remove(other.GetComponent<MyInteractable>());
        }
    }

    public void Pickup()
    {
        currentInteractable = GetNearestInteractable();
        if (currentInteractable == null)
        {
            return;
        }

        if (currentInteractable.activeHand != null)
        {
            currentInteractable.activeHand.Drop();
        }

        OnInteract onInteract = currentInteractable.gameObject.GetComponent<OnInteract>();

        if(onInteract) {
            usedInteract = true;
            onInteract.Interact();
        } else {
            currentInteractable.isGrabbed = true;
            Vector3 graboffset = -currentInteractable.grabbedOffset;
            graboffset = currentInteractable.transform.rotation * graboffset;

            GameObject toGrab = currentInteractable.gameObject;
            if(currentInteractable.target) {
                toGrab = currentInteractable.target;
            }

            toGrab.transform.position = transform.position + graboffset;
            
            joint.connectedBody = toGrab.GetComponent<Rigidbody>();

            currentInteractable.activeHand = this;
        }
    }

    public void Drop()
    {
        if (!currentInteractable)
        {
            return;
        }

        Rigidbody target;
        if(currentInteractable.target) {
            target =  currentInteractable.target.GetComponent<Rigidbody>();  
        } else {
            target = currentInteractable.GetComponent<Rigidbody>();
        }

        float yRotation = transform.parent.transform.eulerAngles.y;
        target.velocity = Quaternion.AngleAxis(yRotation, Vector3.up) * pose.GetVelocity();
        target.angularVelocity = Quaternion.AngleAxis(yRotation, Vector3.up) * pose.GetAngularVelocity();

        joint.connectedBody = null;

        currentInteractable.activeHand = null;
        currentInteractable = null;
        currentInteractable.isGrabbed = false;
    }

    private MyInteractable GetNearestInteractable()
    {
        MyInteractable nearest = null;
        float minDist = float.MaxValue;
        float dist = 0.0f;

        foreach (MyInteractable interactable in contactInteractables)
        {
            dist = (interactable.transform.position - transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = interactable;
            }
        }
        
        return nearest;
    }
}
