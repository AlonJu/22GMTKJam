
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // will be used to chase or target player
    [SerializeField]
    private GameObject _player;

    //enemy rigidbdoy
    private Rigidbody _rb;

    [SerializeField]
    private GameObject _explosion;


    //enemy

    //enemy speed
    [SerializeField]
    private float _enemySpeed;
    //enemy health
    [SerializeField]
    private int _enemyHealth;
    //enemy damage
    [SerializeField]
    private int _enemyDamage;
    //enemy frequency
    [SerializeField]
    private float _enemyFrequency;
    //enemy resistance
    [SerializeField]
    private float _enemyResistance;
    [SerializeField]
    private float _enemyForce;
    // Start is called before the first frame update
    [SerializeField]
    private bool AIToggleNavMesh;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
       // _player = GameObject.FindGameObjectWithTag("Player");
        
        //_explosion = GameObject.FindGameObjectWithTag("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        KnockBack();
        ChasePlayer();
    }
    public void KnockBack()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
      
        _explosion.SetActive(true);
       // _player.GetComponent<Rigidbody>().AddForce(-transform.forward * _enemySpeed, ForceMode.Impulse);
        //player  lose health
        //_player.GetComponent<PlayerMovement>().LoseHealth(_enemyDamage);
    }
    public void ChasePlayer()
    {
        if (AIToggleNavMesh == true)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = _player.transform.position;
        }
        
        else // this should be a lunge attack of sorts -- make it so that when they are within a certain distance of the player, they lunge
        {
        _rb.AddForce((_player.transform.position - transform.position).normalized * _enemySpeed);
        //transform.Translate(_player.transform.position - transform.position*_enemySpeed*Time.deltaTime);
        transform.LookAt(_player.transform);        
        }
    }
}
