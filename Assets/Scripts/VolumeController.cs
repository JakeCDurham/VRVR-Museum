using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> sources;
    [Range(0, 100)] [SerializeField] private int volume;
    private const int MAXVOLUME = 100;

    [SerializeField] private MenuTextUpdater textUpdater;
    // Start is called before the first frame update
    void Start()
    {
        ChangeVolume(0);
    }

    public void ChangeVolume(int c)
    {
        volume += c;
        if (volume > 100)
        {
            volume = 100;
        }
        else if(volume < 0)
        {
            volume = 0;
        }
        foreach (AudioSource source in sources)
        {
            if(source.CompareTag("backgroundAS"))
            {
                source.volume = (float)volume / (float)MAXVOLUME * 0.1f;
            }
            else
            {
                source.volume = (float)volume / MAXVOLUME;
            }
        }
        textUpdater.UpdateValue(volume.ToString());
    }
}
