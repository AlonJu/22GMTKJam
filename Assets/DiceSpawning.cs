using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawning : MonoBehaviour
{
    public bool diceExists = true;
    public float diceTimer = 15.0f;
    // Start is called before the first frame update
    public GameObject[] spawnpoint;
    public int spawnerKey = 0;
    void Awake()
    {
         spawnpoint = GameObject.FindGameObjectsWithTag ("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Dice") !=null || GameObject.FindGameObjectWithTag("Thrown") !=null){
            diceExists = true;
        } else{
            diceExists = false;
        }
        if (!diceExists){
            randomDiceSpawn();

        }
    }
    void randomDiceSpawn(){
        //start a countdown, when it hits zero pick a random "diceSpawner" to spawn the dice at
        spawnerKey = Random.Range(0, spawnpoint.Length);
        diceTimer -=Time.deltaTime;
        if (!diceExists){
            if (diceTimer < 0.0f){
                spawnpoint[spawnerKey].GetComponent<SpawnerBehavior>().SpawnDice();
                diceTimer = 15.0f;
                diceExists = true;
            }
        } else{
            diceTimer = 15.0f;
        }
    }
}
