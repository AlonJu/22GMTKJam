using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePhysics : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidBody;
    public bool hit;
    public Vector3 hitVector;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(new Vector3(3.0f,10.0f, 2.0f), ForceMode.Impulse);
        rigidBody.AddTorque(new Vector3(3.0f,10.0f, 2.0f), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (hit){
            rigidBody.AddForce(hitVector, ForceMode.Impulse);
            hit = false;
        }
    }
}
