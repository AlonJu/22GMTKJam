using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehavior : MonoBehaviour
{
    public bool hopping = false;
    //private bool didSubtract = false;
    private int currentTimer;
    public Rigidbody rigidBody;
    public int timer = 200;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.layer ==3){
            if (hopping){
                
                rigidBody.AddForce(new Vector3(Random.Range(-2.0f, 2.0f),30.0f,Random.Range(-2.0f, 2.0f)), ForceMode.Impulse);
                rigidBody.AddTorque(Random.Range(2f, 10f), Random.Range(2f, 10f), Random.Range(2f, 10f));
                if (timer ==currentTimer)
                    timer --;
                //didSubtract = true;
                if (timer <=0){
                    Destroy(gameObject);
                }
            }
        } else if (other.gameObject.layer == 6){
                Destroy(gameObject);
            }
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentTimer = timer;
        //didSubtract = false;
    }
}
