using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehavior : MonoBehaviour
{
    public bool hopping = false;
    public Rigidbody rigidBody;
    public int timer = 2;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.layer ==3){
            if (hopping){
            rigidBody.AddForce(new Vector3(Random.Range(-2.0f, 2.0f),30.0f,Random.Range(-2.0f, 2.0f)), ForceMode.Impulse);
            rigidBody.AddTorque(Random.Range(2f, 10f), Random.Range(2f, 10f), Random.Range(2f, 10f));
            timer --;
            if (timer >=0){
                Destroy(gameObject);
            }
            } else{

            }
        }
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
