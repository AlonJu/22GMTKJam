using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDice : MonoBehaviour
{
    public bool showControls = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
