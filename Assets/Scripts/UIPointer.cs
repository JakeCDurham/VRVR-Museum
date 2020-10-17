using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointer : MonoBehaviour
{
    public float defaultLength = 5f;
    public GameObject dot;
    public UIInput uiInput;

    private LineRenderer linerenderer = null;
    private void Awake()
    {
        linerenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
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
