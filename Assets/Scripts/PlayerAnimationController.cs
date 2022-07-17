using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{ //funfunfun!!!!
    // Start is called before the first frame update
    public PlayerMovement playerMovement;
    private Animator anim;
    private SpriteRenderer sprite;
    GMTKJam controlss;
    private bool dice;
    public int state;

    private bool grounded, locked, landed;

    void Awake()
    {
        controlss = new GMTKJam();

        //playerMovement = GetComponent<PlayerMovement>();

        anim = GetComponent<Animator>();

        sprite = GetComponent<SpriteRenderer>();
        
        //initialize all da anims
    }
    private void OnEnable(){
        controlss.Player.Enable();
    }
    private void OnDisable(){
        controlss.Player.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerMovement.holding){
            dice = true;
        }else{
            dice = false;
        }
        Vector2 moveInput = playerMovement.controls.Player.Move.ReadValue<Vector2>();

        state = dumbPersonMethod(moveInput);
        grounded = playerMovement.grounded;
        //le ebin state machine  
    anim.SetBool("Dice", dice);
    anim.SetInteger("State", state);

    }
    private int dumbPersonMethod(Vector2 vector){
        int states = 0;
        if (vector == Vector2.zero){
            states = 0;
        }
        else if(vector.x > 0){
            states = 3;
        }else if (vector.x < 0){
            states = 1;
        }else if (vector.y > 0){
            states = 4;
        }else if (vector.y < 0){
            states = 2;
        }
        return states;

    }
    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("RunLeft");
    private static readonly int Jump = Animator.StringToHash("RunRight");
    private static readonly int Fall = Animator.StringToHash("RunFwd");
    private static readonly int Land = Animator.StringToHash("RunBack");
    private static readonly int dWalk = Animator.StringToHash("DiceRunLeft");
    private static readonly int dJump = Animator.StringToHash("DiceRunRight");
    private static readonly int dFall = Animator.StringToHash("DiceRunFwd");
    private static readonly int dLand = Animator.StringToHash("DiceRunBack");

    
}
