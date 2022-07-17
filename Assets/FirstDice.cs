using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDice : MonoBehaviour
{
    public bool showControls = false;
    public AudioSource bgm;
    public AudioSource wind;
    public AudioSource bgmLoop;

    public EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "grabBox"){
            showControls = true;
        }
    }

    void OnDisable(){
        showControls = false;
        bgm.GetComponent<BGMBehavior2>().started = true;
        enemySpawner.roundStarted = true;
        GameObject.Destroy(wind.gameObject);
        //bgmLoop.PlayScheduled(AudioSettings.dspTime * bgm.GetComponent<AudioClip>().length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
