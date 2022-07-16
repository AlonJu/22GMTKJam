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

    void Start (){
    }

    void FixedUpdate()
    {
                transform.localPosition = new Vector3(tracked.transform.localPosition.x,tracked.transform.localPosition.y,tracked.transform.localPosition.z);    
                //im surprised by how little of this code matters now that everything is mouse controlled
    }      
        
 }


