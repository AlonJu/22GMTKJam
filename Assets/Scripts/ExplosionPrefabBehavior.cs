using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPrefabBehavior : MonoBehaviour
{
    //on trigger stay creates a list of unique gameobjects
    //on enable will start a 1 frame timer to then 
    //explode them, instantiate the explosion sprite, and then kill this one in the late update
    // Start is called before the first frame update
    int frameTimer = 5;
    List<Collider> entityList = new List<Collider>();
    List<Rigidbody> hitboxList = new List<Rigidbody>();
    
    SphereCollider radius;
    public GameObject explosionSprite;
    public GameObject newDiceObj;
    public float diceModifier;
    public float explosionForce;
    public float slamDownOffset;
    private void OnTriggerStay(Collider other) {
        //add entity to list if not already on list
        if(!entityList.Contains(other))
        {
            //add the object to the list
            entityList.Add(other);
        }
    }
    private void OnEnable() {
        radius = GetComponent<SphereCollider>();
        //explosionSprite = (GameObject) Resources.Load("Assets/Explosion Sprite.prefab");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void update(){
        if(frameTimer <= 0){
            Destroy(gameObject);
        }
    }
    void LateUpdate()
    {
        if ( frameTimer == 0){
            foreach(Collider entity in entityList){
                if (entity.GetComponent<Rigidbody>())
                    hitboxList.Add(entity.GetComponent<Rigidbody>());
            }
            foreach(Rigidbody hitbox in hitboxList){
                hitbox.AddExplosionForce(explosionForce * (diceModifier), transform.position, radius.radius *  diceModifier * 2.5f, -slamDownOffset);
            }
            GameObject i = Instantiate(explosionSprite, transform.position, new Quaternion(0.0f,0.0f,0.0f,0.0f));
            i.transform.localScale *= diceModifier * 2;
            GameObject newDice = Instantiate(newDiceObj, transform.position, transform.rotation);
            newDice.GetComponent<Rigidbody>().velocity = Vector3.zero;
            newDice.GetComponent<Rigidbody>().AddTorque(60f, Random.Range(-180f, 180f), -60f, ForceMode.Impulse);
            newDice.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(4.0f, 8.0f) * (diceModifier % 2 == 0 ? -1 : 1),10.0f,Random.Range(4.0f, 8.0f) * (diceModifier % 2 == 0 ? -1 : 1)), ForceMode.Impulse);
            newDice.GetComponent<PickUpBehavior>().hopping = true;
            Debug.Log(diceModifier);
            Destroy(gameObject);
        } else{
            frameTimer--;
        }
    }
}
