using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Transform self;
    private GameObject gBox;
    private InputHandler inputHandler;
    public GMTKJam controls;
    
    // Start is called before the first frame update

    void Awake()
    {
        controls = new GMTKJam();
        
        rigidBody = GetComponent<Rigidbody>();
        hitbox = GetComponent<CapsuleCollider>();  
        //groundBox = GetComponent<BoxCollider>(); 
        inputHandler = GetComponent<InputHandler>();    
        diceP = GetComponent<DicePhysics>();
        self = GetComponent<Transform>();
        winch = GameObject.Find("Aiming Winch").GetComponent<Transform>();
        SetUpJumpVars();
        rigidBody.freezeRotation = true;
    }
    private void OnEnable(){
        controls.Player.Enable();
    }
    private void OnDisable(){
        controls.Player.Disable();
    }
    //Collisions

    public Rigidbody rigidBody;
    private CapsuleCollider hitbox;
    [SerializeField]
    private BoxCollider groundBox;
    [SerializeField]
    private LayerMask ground;
    private RaycastHit rayHit;
    public float groundedFactor= 0.25f;
    public bool grounded=false;
    private float _slopeAngle;

    //private int jumpLimit=2;
    /*void IsGrounded(){
        if (Physics.BoxCast( new Vector3(groundBox.center.x , groundBox.center.y  , groundBox.center.z  ),
                            new Vector3((groundBox.size.x * 0.9f), groundBox.size.y, (groundBox.size.z * 0.9f)) * 0.5f,
                            -Vector3.up,
                            out rayHit,
                            groundBox.transform.rotation,
                            groundBox.size.y * groundedFactor)) {
           
           
            _slopeAngle = (Vector3.Angle(rayHit.normal, transform.forward) - 90);
            //Debug.Log("Grounded on " + rayHit.transform.name);
            //Debug.Log("\nSlope Angle: " + _slopeAngle.ToString("N0") + "°");
            grounded = true;
            jumpLimit =2;
        } else {
            //Debug.Log("Not Grounded");
            grounded = false;
            jumpLimit = 0;
        }
    }*/
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(groundBox.center.x, groundBox.center.y - (groundBox.size.y*groundedFactor), groundBox.center.z), transform.localScale);
    }
    //Horizontal Movement
    #region Running
    [Header("Horizontal Movement")][Range(0.0f,20.0f)]
        [SerializeField]
        public float speed = 0.0f;
        public float turnFastTime = 1.0f;
        public float targetAngle;
        float angle;
        private Vector3 moveVector;
    void Move(float speed)
    {
        
        Vector2 mInput = controls.Player.Move.ReadValue<Vector2>();  
        moveVector = new Vector3(mInput.x * speed, moveVector.y, mInput.y * speed); // basically translate the vector2 into a vector 3 for horizontal movement  
        moveVector *= Time.deltaTime;   
        moveVector = cam.rotation * moveVector;
    }

    #endregion
    //Vertical Movement
    #region Jumping
    [Header("Jumping")]
        [SerializeField]
        private float jumpSpeed = 0.0f;
        private float gravity = 1f;
        private float groundedGravity = 0.2f;
        float initialJumpVelocity;
        public float maxJumpHeight = 1.0f;
        public float maxJumpTime = 0.5f;
        float[] gravRange = new float[]{
            1.0f, 3.0f
        };
        bool isJumping = false;
        //bool jumpPressed = false;
    void SetUpJumpVars(){
        float timeToApex = maxJumpTime/2;
        gravity = (-2 * maxJumpHeight) /Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight)/timeToApex;
    }
        //[SerializeField]
        //private float jumpBoost = 0.0f, ledgeBoostSpeed = 0.0f, airBrake = 0.0f, airSpeed = 0.0f;
        void HandleGravity(){

        }
    void Jump(){
        float jInput = controls.Player.Jump.ReadValue<float>();
        //jumping system with 5 features -- a double jump, 
        //a vertical boost at the zenith of your jump, 
        //a boost to help you climb ledges, 
        //an airbrake, 
        //and variable jump height
        //*all of these features are optional -- let's get the basic player controller down first
        
        if (grounded){
            if(jInput != 0.0f){
                Debug.Log("Yay.");
                rigidBody.AddForce(new Vector3(0,jumpSpeed,0), ForceMode.Impulse);
            }
            //rigidBody.mass = 0.1f;
            gravity = gravRange[0];
            rigidBody.AddForce(new Vector3(0,groundedGravity,0));
        }else{
            //rigidBody.mass *= 1.1f;
            if (gravity < gravRange[1])
                gravity -= 0.1f;
            rigidBody.AddForce(new Vector3(0,gravity,0));
        }
/*
        if (rigidBody.velocity.y > 0){ // hacky jump fix
           // rigidBody.AddForce(new Vector3(0,-20,0));
        }

        if (grounded==true && jumpLimit>0){
            //start the jump
            if (jInput){
                Debug.Log(jInput);
                rigidBody.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
                jumpLimit--;
                
            }
        }else {
            if (jInput){
            Debug.Log(jInput);
            //rigidBody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            }
            //moveVector += new Vector3(moveVector.x, 0.0f, moveVector.z);
          //moveVector.y -= (1 - jInput); //variable jump
        }*/

        //rigidBody.AddForce(moveVector, ForceMode.Impulse); 
    }
    #endregion
    //Dice interactions
    #region Dice
    [Header("Dice")]
    public GameObject currentDice = null;
    private Rigidbody diceRB = null;

    [Range(0.0f, 20.0f)][SerializeField]
    private float diceOffset;
    [Range(0.0f, 30.0f)][SerializeField]
    private float angleOffset;
    public float throwSpeed;
    private ClickRaycast clickRay;
    public DicePhysics diceP;
    public Transform winch;
    public GrabBoxScript gbScript;
    public GameObject thrownDicePrefab;

    public bool holding = false;
    void HoldDice(GameObject dice){
        if (dice){
            dice.transform.position = self.position + new Vector3(0.0f, diceOffset, 0.0f);
            diceRB = dice.GetComponent<Rigidbody>();
            diceRB.useGravity = false;
            diceRB.isKinematic = true;
            Vector3 _camRot = cam.transform.rotation.eulerAngles;
            dice.transform.rotation = Quaternion.Euler(new Vector3(0.0f, _camRot.y, 0.0f));
            holding = true;
        } else{
            holding = false;
        }
    }
    void ThrowDice(Vector3 targetPoint, GameObject dice){
        if (dice){
            //throw dice in direction of pointer
            //
            /*
            float xAngle = CalculateDiceXAngle(targetPoint, throwSpeed, dice);
            Quaternion angleQuat = Quaternion.AngleAxis(xAngle, Vector3.right);
            Vector3 angleVector = angleQuat.eulerAngles;*/

            Debug.Log("Dice Thrown.");
            Vector3 angleVector = cam.rotation.eulerAngles;
            
            diceRB = dice.GetComponent<Rigidbody>();
            diceRB.isKinematic = false;
            diceRB.useGravity = true;
            Vector3 position = dice.transform.position;
            Quaternion angle = Quaternion.LookRotation(dice.transform.forward, Vector3.up);
            angle *= dice.transform.rotation;
            
            Destroy(dice);
            GameObject thrownDice = Instantiate(thrownDicePrefab);
            //calculate the throw, then rotate it.
            Vector3 forceVector = winch.rotation.eulerAngles;
            //angle = Quaternion.AngleAxis(forceVector.y, Vector3.up);
            thrownDice.GetComponent<Rigidbody>().transform.SetPositionAndRotation(position, winch.rotation);
            thrownDice.GetComponent<Rigidbody>().AddForce(thrownDice.GetComponent<Rigidbody>().transform.forward * throwSpeed, ForceMode.Impulse);
            diceRB = null;
            dice = null;

        }
    }
    public float CalculateDiceXAngle(Vector3 targetPoint, float shotSpeed, GameObject dice){ //never used because its too complicated lol
        float gravity = 6.67f;

        float sinAngle = (gravity * Vector3.Distance(dice.transform.position, targetPoint))/(shotSpeed*shotSpeed);
        float finalAngle = Mathf.Asin(sinAngle);
        return finalAngle;
    }
    void ClickEvent(bool clicked){
        
        if(clicked == true){
            ThrowDice(Vector3.zero, currentDice);
            Debug.Log("Click.");
            //ClickRaycast.RayHitData rayData = new ClickRaycast.RayHitData();
            /*
            rayData = clickRay.CastReticle(1);
            if (rayData.hitLocation != Vector3.zero){
                Debug.Log("YES!!!!");
            }
            if (rayData.hitObject.transform.name == "DiceTest"){
                diceP.hit = true;
                diceP.hitVector = clickRay.cam.transform.forward * 5.0f;
                Debug.Log("DICE HIT");
            } else {

            }*/
            /*rayData = clickRay.PickUp();
            if (rayData.hitLocation != Vector3.zero){
                Debug.Log("YES!!!!");
                if (currentDice){
                    ThrowDice(rayData.hitLocation, currentDice);
                }
            }*/
        }
    }
    #endregion

    //cam
    public Transform cam;
    //Utilities
    [Header("Misc.")]
    public int health;

    public int maxHealth;

    void FixedUpdate() 
    {
        //Debug.Log(jumpLimit);
        //use states to control velocity based on whetehr or not youre grounded
     Move(speed);   
     //IsGrounded();
     //HandleGravity();
     Jump();

     rigidBody.AddForce(moveVector * speed, ForceMode.Impulse);
     Vector3 camRotationVector = cam.rotation.eulerAngles;  
     Quaternion  selfRotation = self.rotation;
     Vector3 selfRotationVector = selfRotation.eulerAngles;
     rigidBody.rotation = Quaternion.Euler(new Vector3(selfRotationVector.x, camRotationVector.y, selfRotationVector.z));
     
    //rigidBody.velocity = moveVector * speed;
     if (inputHandler.left_mInput){
        Debug.Log("Click handled");
     }
      /* m_playerIsGrounded = m_distanceFromPlayerToGround <= 1f;

            if (isJumping && m_playerJumpStarted &&
                (m_playerIsGrounded || MaxAllowJump > m_currentNumberOfJumpsMade)) StartCoroutine(ApplyJump());

            if (m_playerIsGrounded) m_currentNumberOfJumpsMade = 0;*/
    }
   void Update(){
     HoldDice(currentDice);
     ClickEvent(inputHandler.left_mInput);
     /*isJumping = Input.GetKeyDown(KeyCode.Space);
     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * raycastDistance, Color.blue);
            //added layermask for those dealing with complex ground objects.
    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out m_hit,
    raycastDistance, m_groundLayerMask))
    {
                m_groundLocation = m_hit.point;
                m_distanceFromPlayerToGround = transform.position.y - m_groundLocation.y;
    }
    */
   }


   //dumb bullshit time lets go
  /* private float m_xAxis;
        private float m_zAxis;
        //private Rigidbody rigidBody;
        private RaycastHit m_hit;
        private Vector3 m_groundLocation;
        //private bool m_leftShiftPressed;
        private int m_groundLayerMask = 3;
        private float m_distanceFromPlayerToGround;
        private bool m_playerIsGrounded;
        private bool m_playerJumpStarted;
        private const int MaxAllowJump = 2; //maximum allowed jumps
        private int m_currentNumberOfJumpsMade; //current number of jumps processed
        public float raycastDistance;

     private void PlayerJump(float jumpForce, ForceMode forceMode)
        {
            rigidBody.AddForce(jumpForce * rigidBody.mass * Time.deltaTime * Vector3.up, forceMode);
        }

        /// <summary>
        /// handles single and double jump
        /// waits until space bar pressed is terminated before
        /// next jump is initiated.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApplyJump()
        {
            PlayerJump(jumpSpeed, ForceMode.Impulse);
            m_playerIsGrounded = false;
            m_playerJumpStarted = false;
            yield return new WaitUntil(() => !isJumping);
            ++m_currentNumberOfJumpsMade;
            m_playerJumpStarted = true;
        }*/
}
