using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 mInput {get; set;}
    public bool jInputUp {get; set;}
    public bool jInputDown {get; set;}

    public bool left_mInput {get; set;}
    public bool right_mInput {get; set;}
    // Update is called once per frame
    private void Update(){
        mInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        jInputUp = Input.GetButtonUp("Jump");
        jInputDown = Input.GetButtonDown("Jump");
        left_mInput = Input.GetMouseButtonDown(0);
        right_mInput = Input.GetMouseButtonDown(1);
        if (left_mInput){
        Debug.Log("Click registered");
     }
    }
}
