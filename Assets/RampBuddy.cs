using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampBuddy : MonoBehaviour
{
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    public float coolSpeed;
    void Start()
    {
        //playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.layer ==3)
            playerMovement.speed = coolSpeed;
    }
    private void OnTriggerExit(Collider other){
        if (other.gameObject.layer ==3)
            playerMovement.speed = 2.0f;
    }
    void Update()
    {
        
    }
}
