using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class UIInput : BaseInputModule
{
    // Start is called before the first frame update
    public Camera camera;
    public SteamVR_Input_Sources source;
    public SteamVR_Action_Boolean clickAction;

    private GameObject currentObject;
    private PointerEventData data;

    protected override void Awake()
    {
        base.Awake();
        
        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        //reset data
        data.Reset();
        data.position = new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2);

        //raycast
        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = data.pointerCurrentRaycast.gameObject;

        //clear
        m_RaycastResultCache.Clear();

        //Hover
        HandlePointerExitAndEnter(data, currentObject);

        //Press
        if (clickAction.GetStateDown(source))
        {
            ProcessPress(data);
        }

        //Release
        if (clickAction.GetStateUp(source))
        {
            ProcessRelease(data);
        }

    }

    public PointerEventData getData()
    {
        return data;
    }

    private void ProcessPress(PointerEventData eventData)
    {
        // set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;
        
        //Get down handler
        GameObject newPointerPress =
            ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

        // If no down, get click handler.
        if (newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
        }

        //set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;
    }

    private void ProcessRelease(PointerEventData eventData)
    {
        // execute pointer up.
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        //check for click handler.
        GameObject pointerUphandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        //check if actual click.
        if (pointerUphandler == data.pointerPress)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        //clear selected gameobject.
        eventSystem.SetSelectedGameObject(null);
        
        //reset data.
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
