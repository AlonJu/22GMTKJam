using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject gBox;
    private InputHandler inputHandler;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        hitbox = GetComponent<CapsuleCollider>();  
        //groundBox = GetComponent<BoxCollider>(); 
        inputHandler = GetComponent<InputHandler>();    
    }
    //Collisions

    private Rigidbody rigidBody;
    private CapsuleCollider hitbox;
    [SerializeField]
    private BoxCollider groundBox;
    [SerializeField]
    private LayerMask ground;
    private RaycastHit rayHit;
    public float groundedFactor= 0.25f;
    private bool grounded=false;
    private float _slopeAngle;

    private int jumpLimit=2;
    void IsGrounded(){
        RaycastHit hit;
        if (Physics.BoxCast( new Vector3(groundBox.center.x , groundBox.center.y  , groundBox.center.z  ),
                            new Vector3((groundBox.size.x * 0.9f), groundBox.size.y, (groundBox.size.z * 0.9f)) * 0.5f,
                            -Vector3.up,
                            out rayHit,
                            groundBox.transform.rotation,
                            groundBox.size.y * groundedFactor)) {
           
           
            _slopeAngle = (Vector3.Angle(rayHit.normal, transform.forward) - 90);
            Debug.Log("Grounded on " + rayHit.transform.name);
            Debug.Log("\nSlope Angle: " + _slopeAngle.ToString("N0") + "°");
            grounded = true;
            jumpLimit =2;
        } else {
            Debug.Log("Not Grounded");
            grounded = false;
            jumpLimit = 0;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(groundBox.center.x, groundBox.center.y - (groundBox.size.y*groundedFactor), groundBox.center.z), transform.localScale);
    }
    //Horizontal Movement
    #region Running
    [Header("Horizontal Movement")][Range(0.0f,20.0f)]
        [SerializeField]
        private float speed = 0.0f;

        private Vector3 moveVector;
    void Move(float speed)
    {
        Vector2 mInput = inputHandler.mInput;
        moveVector = new Vector3(mInput.x * speed, moveVector.y, mInput.y * speed); // basically translate the vector2 into a vector 3 for horizontal movement  
        moveVector *= Time.deltaTime;   

        //rigidBody.AddForce(moveVector, ForceMode.Impulse);
    }

    #endregion
    //Vertical Movement
    #region Jumping
    [Header("Jumping")]
        [SerializeField]
        private float jumpSpeed = 0.0f;
        [SerializeField]
        private float jumpBoost = 0.0f, ledgeBoostSpeed = 0.0f, airBrake = 0.0f, airSpeed = 0.0f;
    void Jump(){
        //jumping system with 5 features -- a double jump, 
        //a vertical boost at the zenith of your jump, 
        //a boost to help you climb ledges, 
        //an airbrake, 
        //and variable jump height
        //*all of these features are optional -- let's get the basic player controller down first
        float jInput = inputHandler.jInput;

        if (grounded==true && jumpLimit>0){
            //start the jump
            if (jInput != 0.0f){
                Debug.Log(jInput);
                moveVector += new Vector3(moveVector.x, jumpSpeed, moveVector.z);
                jumpLimit--;
                
            }
        }else {
            moveVector += new Vector3(moveVector.x, 0.0f, moveVector.z);
          //moveVector.y -= (1 - jInput); //variable jump
        }

        //rigidBody.AddForce(moveVector, ForceMode.Impulse); 
    }
    #endregion
    //Dice interactions

    //Camera

    //Utilities

        void FixedUpdate() 
    {
        Debug.Log(jumpLimit);
        //use states to control velocity based on whetehr or not youre grounded
     Move(speed);   
     Jump();
     IsGrounded();
     rigidBody.AddForce(moveVector, ForceMode.Impulse); 
    }
   
}
