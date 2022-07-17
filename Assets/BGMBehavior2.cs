using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMBehavior2 : MonoBehaviour
{
    public AudioSource other;
    public AudioClip otherSong;
    public bool started;
    // Start is called before the first frame update
    void Start()
    {
        
        
       // self.PlayScheduled(AudioSettings.dspTime * selfSong.length);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (started ==false){

        }else{
            other.PlayOneShot(otherSong);
            other.PlayScheduled(Time.deltaTime * otherSong.length);
            started = false;
        }
    }
}
