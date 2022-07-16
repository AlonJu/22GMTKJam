using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour //Tracks and aims the camera around the player
{ 

    public GameObject tracked;
    public GameObject playerObj;
    public PlayerMovement playerMovement;
    
    public int camState;
    float targetAngle, angle;
    float maxCamSpeed = 0.15f;
    float camAccel = 0.01f;
    float camSpeed = 0f;

    void Start (){
        
    }

    void Update()
    {
        
        /*if (!playerMovement.grounded){ //make grounded public!!!
            camState = 2;
        } else{
            if(playerMovement.rigidBody.velocity <= 0.2f){ //THIS TOO!!!!!!!!!!!!!!!!!!!!
                camState = 1;
            } else{
                camState = 0;
            }
        }*/
        switch(camState){
            case 0: //just follow
                transform.localPosition = new Vector3(tracked.transform.localPosition.x,tracked.transform.localPosition.y,tracked.transform.localPosition.z); 
                camSpeed = 0f;
            break;
            case 1: //follow and aim
                camSpeed = 0f;     
            break;
            default:
                //im surprised by how little of this code matters now that everything is mouse controlled
            break;
        }      
        
    }

}
