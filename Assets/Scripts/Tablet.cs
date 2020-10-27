using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(isActive) {
            GetComponent<Rigidbody>().AddForce(Physics.gravity * 0.1f, ForceMode.Acceleration);
        }
    }
}
