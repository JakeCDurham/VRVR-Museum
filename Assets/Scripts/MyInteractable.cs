using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class MyInteractable : MonoBehaviour
{
   public Vector3 grabbedOffset;
   public GameObject target;
   public bool isGrabbed;
   [HideInInspector]
   public MyHand activeHand = null;

   void Awake() {
      isGrabbed = false;
   }  
}
