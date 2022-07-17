using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // will be used to chase or target player
    [SerializeField]
    GameObject _player;

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
    private float attackTimer = 10.0f;
    public float attackTimerInit = 10.0f;
    public float walkMaxTimeInit = 3.0f;
    private float walkMaxTime = 3.0f;
    public int walking = 0;
    private Vector3 _enemyDistance;

    void Awake(){
        _player = GameObject.Find("Player");
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
       //_player = GameObject.FindGameObjectWithTag("Player");
        
        //_explosion = GameObject.FindGameObjectWithTag("Explosion");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Explosion"){
            //whatever you want
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attacar();
        if (walking == 1){
            WalkBackAndForth(Random.Range(0,1) == 1 ? Vector3.right : Vector3.forward);
        }
    }
    public void KnockBack()
    {
        if (transform.position+ _enemyDistance == _player.transform.position)
        {
         Instantiate(_explosion, transform.position, Quaternion.identity);
      
        _explosion.SetActive(true);   
        }
        
       // _player.GetComponent<Rigidbody>().AddForce(-transform.forward * _enemySpeed, ForceMode.Impulse);
        //player  lose health
        //_player.GetComponent<PlayerMovement>().LoseHealth(_enemyDamage);
    }

    //attack
    void Attacar(){
        transform.LookAt(_player.transform); 
        attackTimer -= Time.deltaTime;
        if(attackTimer <=0){
            //attack for real
            AttackPlayer(_player.transform);
            attackTimer = attackTimerInit;
            walking = Random.Range(0, 1);
        }
        
    }
    public GameObject bullet;

    void AttackPlayer(Transform player){
        Instantiate(bullet, transform.position, transform.rotation);
    }

    void WalkBackAndForth(Vector3 direction){
        walkMaxTime--;
        if (walkMaxTime <=0){
        _rb.AddForce(-direction);
        } else{
        _rb.AddForce(direction);
        }
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
