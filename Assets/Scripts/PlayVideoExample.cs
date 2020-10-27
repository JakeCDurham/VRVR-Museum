using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideoExample : MonoBehaviour
{
    [SerializeField] private UnityEngine.Video.VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(vp.isPlaying)
            {
                vp.Pause();
            }
            else
            {
                vp.Play();
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            vp.Stop();
            vp.Play();
        }
    }
}
