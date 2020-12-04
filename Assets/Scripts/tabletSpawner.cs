using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tabletSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> tabletObjects;
    GameObject currentTablet;
    Vector3 initialTabletPos;
    bool isOn;
    bool leftIn;
    bool rightIn;
    public GameObject tabletPrefab;
    public string text;
    private Camera UICam;

    // Start is called  the first frame update
    void Start()
    {
        UICam = GameObject.FindGameObjectWithTag("rightHand").GetComponentInChildren<Camera>();
        isOn = false;
        tabletObjects = new List<GameObject>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("leftHand")) {
            leftIn = true;
        } else if(other.gameObject.CompareTag("rightHand")) {
            rightIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("leftHand")) {
            leftIn = false;
        } else if(other.gameObject.CompareTag("rightHand")) {
            rightIn = false;
        }
    }

    GameObject createTablet()
    {
        Vector3 finalPos = transform.position + transform.up * 0.3f;
        GameObject tablet = Instantiate(tabletPrefab, transform.position + transform.up * 0.2f, transform.rotation);
        tablet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        tablet.GetComponent<Rigidbody>().isKinematic = true;
        Canvas TabletCam = tablet.GetComponentInChildren<Canvas>();
        TabletCam.worldCamera = UICam;
        tabletObjects.Add(tablet);
        TextMeshProUGUI textElement = tablet.GetComponentInChildren<TextMeshProUGUI>();
        textElement.text = text.Replace("\\n", "\n");
        

        tablet.GetComponent<Tablet>().StartSpawn(finalPos);
        return tablet;
    }

    bool isHandleGrabbed(string name) {
        GameObject child = currentTablet.gameObject.transform.Find("positioning").Find(name).gameObject;
        if (child.GetComponent<MyInteractable>() != null && child.GetComponent<MyInteractable>().isGrabbed) {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn) {
            bool isGrabbed = isHandleGrabbed("handleLeft") || isHandleGrabbed("handleRight");
            
            if (isGrabbed) {
                // user moved the tablet from the spawner
                isOn = false;
                // turn on light gravity
                currentTablet.GetComponent<Tablet>().isActive = true;
                currentTablet.GetComponent<Tablet>().FinishSpawn();
                currentTablet.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        
        if (!isOn && (leftIn || rightIn)) {
                isOn = true;
                currentTablet = createTablet();
            }
    }
}
