using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteract : MonoBehaviour
{
    public GameObject teleportHand;
    public GameObject player;
    public GameObject boxPrefab;
    private GameObject currentBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        BoxState();
    }

    private void BoxState() {
        teleportHand.GetComponent<Teleporter>().TryTeleportTo(this.gameObject.transform.position);
        GameObject box = Instantiate(boxPrefab, player.transform.position + player.transform.forward * 3f, player.transform.rotation);
        box.transform.parent = player.transform;
        currentBox = box;
        teleportHand.GetComponent<Teleporter>().AddTodoNormalState(this);
        this.gameObject.active = false;
    }

    public void NormalState() {
        if(currentBox) {
            Destroy(currentBox);
            currentBox = null;
        }
        this.gameObject.active = true;
    }
}
