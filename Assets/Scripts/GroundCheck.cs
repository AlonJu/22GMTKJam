using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement player;

    public BoxCollider coll;
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == 3){
            player.grounded = false;
            //Debug.Log("Grounded.");
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.layer == 3){
            player.grounded = true;
            //Debug.Log("Grounded.");
        } else {
            
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
