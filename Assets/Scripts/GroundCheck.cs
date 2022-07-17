using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement player;

    public BoxCollider coll;
    Collider lastOther;
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
        } else if (other.gameObject.layer == 6) {
            player.gravity = 0.0f;
            player.rigidBody.AddForce(new Vector3(Random.Range(1.0f, 2.0f) * (Random.Range(1, 2) % 2 == 0 ? -1 : 1),10.0f,Random.Range(1.0f, 2.0f) * (Random.Range(1, 2) % 2 == 0 ? -1 : 1)), ForceMode.Impulse);
            if( lastOther!= other){
                player.health--;
            }
        }
        lastOther = other;
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
