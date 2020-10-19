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
    public GameObject tabletPrefab;
    public string text;

    // Start is called  the first frame update
    void Start()
    {
        isOn = false;
        tabletObjects = new List<GameObject>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("leftHand") || other.gameObject.CompareTag("rightHand")) {
            if (!isOn) {
                isOn = true;
                currentTablet = createTablet();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("leftHand") || other.gameObject.CompareTag("rightHand")) {
            if (isOn) {
                isOn = false;
            }
        }
    }

    GameObject createTablet()
    {
        GameObject tablet = Instantiate(tabletPrefab, transform.position + transform.up, transform.rotation);
        tablet.GetComponent<Rigidbody>().useGravity = false;
        tablet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        initialTabletPos = tablet.transform.position;
        tabletObjects.Add(tablet);
        return tablet;   
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn) {
            if (currentTablet.transform.position != initialTabletPos) {
                // user moved the tablet from the spawner
                isOn = false;
                currentTablet.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        
    }
}
