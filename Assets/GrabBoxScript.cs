using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBoxScript : MonoBehaviour
{
     //take the camera transform
    public Transform cam;

    public Transform self;

    public Transform player;
    public GameObject playerObj;
    public PlayerMovement playerMovement;

    [SerializeField]
    private float rotationGuideAmount = 0.1f;

    void Start()
    {
        self = GetComponent<Transform>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion camRotation = cam.rotation;
        Vector3 camRotationVector = camRotation.eulerAngles;

        Quaternion  selfRotation = self.localRotation;
        Vector3 selfRotationVector = selfRotation.eulerAngles;

        float rotationGuide = Vector3.Angle(self.position, player.position);
        selfRotationVector = new Vector3(rotationGuide * rotationGuideAmount, camRotationVector.y, selfRotationVector.z);
        

        self.SetPositionAndRotation(player.position, Quaternion.Euler(selfRotationVector));
        self.position = player.position;

    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Dice"){
            //player will pick it up.
            playerMovement.currentDice = other.gameObject;
        }
    }
}
