using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource, loopSourcce;
    // Start is called before the first frame update
    void Start()
    {
        introSource.Play();
        loopSourcce.PlayScheduled(AudioSettings.dspTime*introSource.clip.length);
    }


}
