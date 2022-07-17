using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTurnScript : MonoBehaviour
{
    //take the camera transform
    public Transform cam;

    public Transform self;

    public Transform player;
    public bool playerSprite = false;
    public bool healthBar = false;
    public bool billboard = false;
    public bool nonTurning = false;
    public int whoAmI;
    [Range(0.0f, 20.0f)]
    public float offset = 0.0f;
    float originalY;
    public float offsetBillboard = 1.0f;
    int health = 3;
    public Sprite lowHealth;
    public Sprite fullHealth;

    [SerializeField]
    private float rotationGuideAmount = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (billboard){
            transform.position =new Vector3(transform.position.x, originalY+ (Mathf.Sin(Time.time) * offsetBillboard), transform.position.z);
            if(GameObject.Find("Pickup Dice").GetComponent<FirstDice>().showControls == true){
                Destroy(gameObject);
            }
        } else if(healthBar){
            health = player.gameObject.GetComponent<PlayerMovement>().health;
            if (health < whoAmI){
                self.GetComponent<SpriteRenderer>().sprite = lowHealth;
            } else{
               self.GetComponent<SpriteRenderer>().sprite = fullHealth; 
            }
        }
        Quaternion camRotation = cam.rotation;
        Vector3 camRotationVector = camRotation.eulerAngles;

        Quaternion  selfRotation = self.rotation;
        Vector3 selfRotationVector = selfRotation.eulerAngles;
        if (!playerSprite || !nonTurning){
            if (self.position.y < player.position.y){
                float rotationGuide = Vector3.Angle(self.position, player.position);
                selfRotationVector = new Vector3(selfRotationVector.x + rotationGuide * rotationGuideAmount, camRotationVector.y, selfRotationVector.z);
            } else{
                selfRotationVector = new Vector3(selfRotationVector.x, camRotationVector.y, selfRotationVector.z);
            }
        self.SetPositionAndRotation(self.position, Quaternion.Euler(selfRotationVector));
        }else {
            selfRotationVector = new Vector3(selfRotationVector.x, camRotationVector.y, selfRotationVector.z);

            self.SetPositionAndRotation(self.position, Quaternion.Euler(selfRotationVector));
            self.position = player.position + new Vector3(0.0f, offset, 0.0f);
        }
    }
}
