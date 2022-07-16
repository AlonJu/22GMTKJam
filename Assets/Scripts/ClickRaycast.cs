using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRaycast : MonoBehaviour
{
    //The point of this script is to tell the player controller what and where the player hit when they click on an object. Depending on code within the player controller, certain things will happen as a result.

    // Start is called before the first frame update
    private InputHandler inputHandler;
    [SerializeField]
    private Camera cam;
    private RaycastHit rayHit;
    public float grabRange = 100.0f;
    bool didHit;

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
    }
    // Update is called once per frame
    void Update()
    {
        //When the player clicks, it casts a ray tangent to the camera, from the camera, and then the player movement script can see what gets hit by the ray and how far away it is
        //bool lmb = inputHandler.left_mInput;

        if (inputHandler.left_mInput){

        }
    }

    public RayHitData CastReticle(int type)
    { // casts a ray at the reticle and returns rayhitdata
         // yeah i dont know theres probably a better way screw you
         RayHitData rayHitData = new RayHitData();
         switch (type){
            case 0: //aiming the dice
                //tell me the point it hit... tell me it's story...
                didHit = Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, 100.0f, 0);
                rayHitData.hitLocation = rayHit.point;
                return rayHitData;
            case 1: // picking up the dice
                //tell me where it hit, and if it hit a dice
                didHit = Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit, grabRange, 0);
                rayHitData.hitLocation = rayHit.point;
                rayHitData.hitObject = rayHit.transform.gameObject;
                return rayHitData;
         }  
        return rayHitData;                   
    }
    public struct RayHitData{ //stores only what we need for player interaction 
        public Vector3 hitLocation;
        public GameObject hitObject;
    }
    
}