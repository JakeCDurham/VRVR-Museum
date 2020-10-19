using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class MyInteractable : MonoBehaviour
{
   public Vector3 grabbedOffset;
   public GameObject target;
   [HideInInspector]
   public MyHand activeHand = null;
}
