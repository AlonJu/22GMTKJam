using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsBehavior : MonoBehaviour
{
    public FirstDice firstDice;
    public GameObject dice;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Awake()
    {
        dice = GameObject.Find("Pickup Dice");
        firstDice = dice.GetComponent<FirstDice>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstDice.showControls == true){
            sprite.enabled = true;
        } else{
            sprite.enabled = false;
        }
        
    }
}
