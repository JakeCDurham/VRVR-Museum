using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject tablet = Instantiate(tabletPrefab, transform.position + transform.up, transform.rotation);
        tablet.GetComponent<Rigidbody>().isKinematic = true;
        tablet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Canvas TabletCam = tablet.GetComponentInChildren<Canvas>();
        TabletCam.worldCamera = UICam;
        initialTabletPos = tablet.transform.position;
        tabletObjects.Add(tablet);
        return tablet;   
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn) {
            bool isGrabbed = false;
            for(int i = 0; i < currentTablet.gameObject.transform.childCount; i++) {
                GameObject child = currentTablet.gameObject.transform.GetChild(i).gameObject;
                if (child.GetComponent<MyInteractable>() != null && child.GetComponent<MyInteractable>().activeHand != null) {
                    isGrabbed = true;
                }
            }   
            if (isGrabbed) {
                // user moved the tablet from the spawner
                isOn = false;
                currentTablet.GetComponent<Rigidbody>().isKinematic = false;
                // turn down gravity
                Vector3 newForce = -Physics.gravity * currentTablet.GetComponent<Rigidbody>().mass;
                currentTablet.GetComponent<Rigidbody>().AddForce(newForce);
            }
        }
        
        if (!isOn && (leftIn || rightIn)) {
                isOn = true;
                currentTablet = createTablet();
            }
    }
}
