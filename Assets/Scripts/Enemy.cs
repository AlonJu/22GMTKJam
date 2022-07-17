using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // will be used to chase or target player
    private GameObject _player;

    //enemy rigidbdoy
    private Rigidbody _rb;



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
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void KnockBack()
    {
        _player.GetComponent<Rigidbody>().AddForce(-transform.forward * _enemySpeed, ForceMode.Impulse);
        //player  lose health
        _player.GetComponent<PlayerController>().LoseHealth(_enemyDamage);
    }
    public void ChasePlayer()
    {
        
        
        _rb.AddForce((_player.transform.position - transform.position).normalized * _enemySpeed);
        //transform.Translate(_player.transform.position - transform.position*_enemySpeed*Time.deltaTime);
        transform.LookAt(_player.transform);        
    }
}
