using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class VideoController : MonoBehaviour
{
    [SerializeField] private UnityEngine.Video.VideoPlayer vp;
    [SerializeField] private List<UnityEngine.Video.VideoPlayer> vps;
    private bool playing;

    // Start is called before the first frame update
    void Start()
    {
        playing = false;
    }

    void modifyPlayState()
    {
        if (playing)
        {
            vp.Pause();
        }
        else
        {
            foreach(UnityEngine.Video.VideoPlayer sel in vps)
            {
                if (sel != vp)
                {
                    sel.Pause();
                }
            }
            vp.Play();   
        }

        playing = !playing;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("leftHand") || other.gameObject.CompareTag("rightHand"))
        {
            modifyPlayState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // do nothing on exit
    }

    // Update is called once per frame
    void Update()
    {


    }
}
