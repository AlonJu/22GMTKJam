using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 mInput {get; set;}
    public float jInput {get; set;}

    public bool left_mInput {get; set;}
    public bool right_mInput {get; set;}
    // Update is called once per frame
    private void Update(){
        mInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        jInput = Input.GetAxisRaw("Jump");
        //left_mInput = Input.GetButtonDown("Mouse1");
       // right_mInput = Input.GetButtonDown("Mouse2");
    }
}
