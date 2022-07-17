using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinchAiming : MonoBehaviour
{ //THE TRANSFORM OF THIS OBJECT CAN BE USED FOR RAY CASTING
    private Transform self;
    public Transform cam;
    public Transform player;
    public float winchSpeed;
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
    }
    /*private void OnEnable() {
        
    }
    private void OnDisable() {
        
    }*/
    // Update is called once per frame
    void OnDrawGizmos(){

    }
    void Update()
    {
    Quaternion camRotation = cam.rotation;
    Vector3 camRotationVector = camRotation.eulerAngles;

    

     self.Rotate(Input.GetAxisRaw("Mouse Y") * winchSpeed, 0.0f, 0.0f);   
     Quaternion  selfRotation = self.rotation;
     Vector3 selfRotationVector = selfRotation.eulerAngles;
     self.rotation = Quaternion.Euler(new Vector3(selfRotationVector.x, camRotationVector.y, selfRotationVector.z));
     self.position = player.position;

     Vector3 line = self.position + ( self.forward * 100.0f);
     Vector3 rotatedLine = Quaternion.AngleAxis(self.rotation.x, transform.right ) * Quaternion.AngleAxis(self.rotation.y, transform.up ) * line;


     //i want to rotate this line aroudn the x and y axis
     Debug.DrawLine(transform.position, rotatedLine, Color.blue);
    }
}
