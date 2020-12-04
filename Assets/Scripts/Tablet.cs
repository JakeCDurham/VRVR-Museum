using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    public bool isActive;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 fullScale;
    int frameCounter, spawnFramesDuration;

    void Awake() {
        frameCounter = -1;
        isActive = false;

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartSpawn(Vector3 endPosIn) {
        spawnFramesDuration = (int) (2.0f / Time.deltaTime);
        frameCounter = 0;
        startPos = gameObject.transform.position;
        endPos = endPosIn;
        fullScale = gameObject.transform.localScale;
        setSize();
    }

    // Update is called once per frame
    void Update()
    {
        setSize();
    }

    public void FinishSpawn() {
        frameCounter = spawnFramesDuration;
        setSize();
    }

    void setSize() {
        if(frameCounter >= 0 && frameCounter <= spawnFramesDuration) {
            float interp = (((float) frameCounter / (float) spawnFramesDuration));
            float scale = 0.5f + 0.5f*interp;
            gameObject.transform.localScale = fullScale * scale;
            Vector3 diff = (endPos - startPos);
            diff *= interp;
            gameObject.transform.position = startPos + diff;
            frameCounter += 1;
        }
    }

    void FixedUpdate()
    {
        if(isActive) {
            GetComponent<Rigidbody>().AddForce(Physics.gravity * 0.1f, ForceMode.Acceleration);
        }
    }
}
