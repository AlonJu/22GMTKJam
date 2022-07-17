using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPrefabBehavior : MonoBehaviour
{
    //on trigger stay creates a list of unique gameobjects
    //on enable will start a 1 frame timer to then 
    //explode them, instantiate the explosion sprite, and then kill this one in the late update
    // Start is called before the first frame update
    int frameTimer = 2;
    List<Collider> entityList = new List<Collider>();
    List<Rigidbody> hitboxList = new List<Rigidbody>();
    
    SphereCollider radius;
    public GameObject explosionSprite;
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
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach(Collider entity in entityList){
            if (entity.GetComponent<Rigidbody>())
                hitboxList.Add(entity.GetComponent<Rigidbody>());
        }
        foreach(Rigidbody hitbox in hitboxList){
            hitbox.AddExplosionForce(explosionForce, transform.position, radius.radius, -slamDownOffset);
        }
        Instantiate(explosionSprite, transform.position, new Quaternion(0.0f,0.0f,0.0f,0.0f));
        Destroy(transform.gameObject);
    }
}
