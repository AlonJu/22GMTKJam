using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        transform.LookAt(player, Vector3.up);
        transform.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 40.0f, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer ==3){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
