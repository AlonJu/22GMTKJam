using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    //enemy spawner script
[SerializeField]
private GameObject[] _enemy;


//game object _spawnpoints
[SerializeField]
private GameObject[] _spawnPoints;

//random spawn bool
[SerializeField]
private bool _randomSpawning;


[SerializeField]
private int _enemyCount=0;
[SerializeField]
private bool _enemyCluster;
[SerializeField]
private int _enemyCountMax=50;

//delay between spawns
[SerializeField]
private float _spawnTime;

[SerializeField]
private int _round=0;
public bool roundStarted;


    
    public IEnumerator Spawn()
    {
        _round++;
        while (_enemyCount <  _enemyCountMax)
        {
            int i=0;
            //random spawn

            if (_randomSpawning)
            {
                int randomIndex = Random.Range(0, _spawnPoints.Length);
                int clusterNum = 1;
                if(_enemyCluster)
                    clusterNum = Random.Range(3, 10);
                for(int x = clusterNum; x > 0; x--){
                Instantiate(_enemy[i], _spawnPoints[randomIndex].transform.position, Quaternion.identity);
                _enemyCount++;
                }
            }
            //ordered spawn
            else
            {
                Instantiate(_enemy[i], _spawnPoints[_round].transform.position, Quaternion.identity);
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
        if (_enemyCount <1 && roundStarted)
        {
            _round++;
            _enemyCountMax++;
            StartCoroutine(Spawn()); //?

        }
        
    }
    
}
