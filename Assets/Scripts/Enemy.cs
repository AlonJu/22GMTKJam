
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
    [SerializeField]
    private Vector3 _enemyDistance;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
       _player = GameObject.FindGameObjectWithTag("Player");
        
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
        if (transform.position+_enemyDistance == _player.transform.position)
        {
         Instantiate(_explosion, transform.position, Quaternion.identity);
      
        _explosion.SetActive(true);   
        }
        
       // _player.GetComponent<Rigidbody>().AddForce(-transform.forward * _enemySpeed, ForceMode.Impulse);
        //player  lose health
        //_player.GetComponent<PlayerMovement>().LoseHealth(_enemyDamage);
    }
    public void ChasePlayer()
    {
        if (AIToggleNavMesh=true)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = _player.transform.position;
        }
        
        else
        {
        _rb.AddForce((_player.transform.position - transform.position).normalized * _enemySpeed);
        //transform.Translate(_player.transform.position - transform.position*_enemySpeed*Time.deltaTime);
        transform.LookAt(_player.transform);        
        }
    }
}
