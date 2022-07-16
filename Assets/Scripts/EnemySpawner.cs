using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enemy spawner script
[SerializeField]
private GameObject[] _enemy;


//game object _spawnpoints
[SerializeField]
private GameObject[] _spawnPoints;

//random spawn bool
[SerializeField]
private bool _randomSpawning;


//delay between spawns
[SerializeField]
private float _spawnTime;

[SerializeField]
private int _round=0;

[SerializeField]
private int _enemyCount;
[SerializeField]
private int _enemyCountMax;

public class EnemySpawner : MonoBehaviour
{
    IEnumerator Spawn()
    {
        round++;
        while (_enemyCount <  _enemyCountMax)
        {
            int i=0;
            //random spawn
            
            if (_randomSpawning)
            {
                int randomIndex = Random.Range(0, _spawnPoints.Length);
                Instantiate(_enemy[], _spawnPoints[randomIndex].transform.position, Quaternion.identity);
                _enemyCount++;
            }
            //ordered spawn
            else
            {
                Instantiate(_enemy[], _spawnPoints[_round].transform.position, Quaternion.identity);
                _enemyCount++;
                
                if (_round >= _spawnPoints.Length)
                {
                    _round = 0;
                }
            }
            yield return new WaitForSeconds(_spawnTime);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyCount <1)
        {
            round++;
            StartCoroutine(Spawn());

        }
        
    }
}
