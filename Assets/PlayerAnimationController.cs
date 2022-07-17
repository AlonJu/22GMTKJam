using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{ //funfunfun!!!!
    // Start is called before the first frame update
    private PlayerMovement playerMovement;
    private Animator anim;
    private SpriteRenderer sprite;
    GMTKJam controls;

    private bool grounded, locked, landed;

    void Awake()
    {
        controls = new GMTKJam();
        playerMovement = GetComponent<PlayerMovement>();

        anim = GetComponent<Animator>();

        sprite = GetComponent<SpriteRenderer>();
        //initialize all da anims
    }
    private void OnEnable(){
        controls.Player.Enable();
    }
    private void OnDisable(){
        controls.Player.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        grounded = playerMovement.grounded;
        //le ebin state machine  

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
