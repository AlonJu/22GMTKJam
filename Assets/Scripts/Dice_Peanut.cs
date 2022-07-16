using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Peanut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckSides()
    {
        if(transform.up == new Vector3(0,1,0))
        Debug.Log("Detected");
        /*
        //z
        transform.forward;

        -transform.forward;

        transform.up;

        -transform.up;

        transform.right;

        transform.left;
*/return false;
    }
    
}
