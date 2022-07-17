using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{ //funfunfun!!!!
    // Start is called before the first frame update
    private PlayerMovement playerMovement;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool grounded, locked, landed;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        anim = GetComponent<Animator>();

        sprite = GetComponent<SpriteRenderer>();
        //initialize all da anims
    }

    // Update is called once per frame
    void Update()
    {
        //le ebin state machine  

    }
}
