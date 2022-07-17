using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceExplosion : MonoBehaviour
{
    public Dice_Peanut diceIndex;
    public bool active = false; // if the dice has been thrown
    // Start is called before the first frame update
    //if the dice hits the ground, start the timer
    public int timerSeconds = 3;
    int intSeconds;
    public float realSeconds = 0.0f;
    public GameObject explosion;
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.layer ==3){
            active = true;
        }
    }
    void OnEnable()
    {
        diceIndex = GetComponent<Dice_Peanut>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active){
            realSeconds += Time.deltaTime;
            intSeconds = (int) realSeconds % 60;
        }
        if (timerSeconds == intSeconds){
            GameObject i =Instantiate(explosion, transform.position,transform.rotation);
            ExplosionPrefabBehavior o = i.GetComponent<ExplosionPrefabBehavior>();
            o.diceModifier = (float) diceIndex.returnNumber;
            Destroy(gameObject);
        }
    }
}
