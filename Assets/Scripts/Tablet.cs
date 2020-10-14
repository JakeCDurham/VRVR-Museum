using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Tablet : MonoBehaviour
{
    List<GameObject> tabletObjects;
    public GameObject screenPrefab;
    public GameObject handlePrefab;
    public GameObject tablet;
    public TMP_Text textPrefab;

    // Start is called  the first frame update
    void Start()
    {
        tabletObjects = new List<GameObject>();
        createTablet();
    }

    void createTablet()
    {
        float height = 0.5f;
        float width = height * 2f;
        Instantiate(tablet, new Vector3(0, height * 2, 0), new Quaternion());
        /*
        GameObject parent = new GameObject();
        makeInteractable(parent);
        parent.transform.position = new Vector3(0, height*2f, 0);

        GameObject tablet = Instantiate(screenPrefab, new Vector3(0, 0, 0), Quaternion.identity, parent.transform);
        tablet.transform.localScale = new Vector3(height/50f, height, width);
        tablet.transform.localPosition = new Vector3(0, 0, 0);
        //tablet.AddComponent<Rigidbody>();

        TMP_Text text = Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity, parent.transform);
        RectTransform container = text.GetComponent<RectTransform>();
        container.sizeDelta = new Vector2(container.sizeDelta.x*width, container.sizeDelta.y*height);
        text.transform.localPosition = new Vector3(0, 0, 0);
        text.transform.Rotate(0, 90, 0);
        text.fontSize = height;
        float margintop = height / 20;
        float marginside = height / 8;
        text.margin = new Vector4(marginside, margintop, marginside, margintop);
        

        GameObject handleLeft = Instantiate(handlePrefab, new Vector3(0, 0, 0), Quaternion.identity, parent.transform);
        handleLeft.transform.localScale = new Vector3(height/5f, height/2f, height/5f);
        handleLeft.transform.localPosition = new Vector3(0, 0, -width/2f);
        //makeInteractable(handleLeft);
        

        GameObject handleRight = Instantiate(handlePrefab, new Vector3(0, 0, 0), Quaternion.identity, parent.transform);
        handleRight.transform.localScale = new Vector3(height/5f, height/2f, height/5f);
        handleRight.transform.localPosition = new Vector3(0, 0, width/2f);
        //makeInteractable(handleRight);
        
        //handleLeft.AddComponent<FixedJoint>();
        //handleLeft.GetComponent<FixedJoint>().connectedBody = tablet.GetComponent<Rigidbody>();
        //handleRight.AddComponent<FixedJoint>();
        //handleRight.GetComponent<FixedJoint>().connectedBody = tablet.GetComponent<Rigidbody>();
        //text.gameObject.AddComponent<FixedJoint>();
        //text.GetComponent<FixedJoint>().connectedBody = tablet.GetComponent<Rigidbody>();


        tabletObjects.Add(parent);
        */
    }

    void makeInteractable(GameObject obj) {
        //obj.AddComponent<BoxCollider>();
        obj.AddComponent<MyInteractable>();
        obj.AddComponent<Rigidbody>();
        obj.tag = "interactable";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
