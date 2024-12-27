using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Jobs;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject ebullet;
    [SerializeField] EnemyStats enemyStats;
    [SerializeField] Camera cam;
    [SerializeField] Transform wshoot;
    [SerializeField] Transform bulletcontainer;
    [SerializeField] GameObject killcount;
    public float changetimecount = 3f;
    private float frequency = 1f;

    public float firetimecount = 3f;
    private float ffrequency = 1f;

    private float random_z;
    private float randDistance;

    private float currentHealth;

    public float damaging => enemyStats.enemyDamage;


    private void Start()
    {
        random_z = UnityEngine.Random.Range(-1, 2);
        randDistance = UnityEngine.Random.Range(5, 7);
        //Debug.Log(randDistance);
        currentHealth = enemyStats.enemyHealth;
        
    }

    private void Update()
    { 
        

        if (frequency < changetimecount)
        {
            frequency += Time.deltaTime;

        }

        else
        {
            frequency = 0f;
            random_z = UnityEngine.Random.Range(-5f, 6f);

        }
         movetoPlayer();
        fire();
    }
    
    private void movetoPlayer()
    {   Vector3 _playerp = player.transform.position;
        Vector3 _enemyp = transform.position;
        float _speedn = enemyStats.enemySpeed * Time.deltaTime;
        float _distance = Vector2.Distance(_enemyp , _playerp);

        Vector2 fdirection = new Vector2(_playerp.x - transform.position.x, _playerp.y - transform.position.y);
        transform.up = fdirection;

        if (_distance > randDistance) 
        {
            transform.position = Vector2.MoveTowards(_enemyp, _playerp, _speedn * 1);
        }
        
        else
        {
            

            transform.RotateAround(_playerp, new Vector3(0, 0, random_z) , _speedn * 5);
            //Debug.Log(random_z);


        }
        
    }

    private void fire()
    {
        Vector3 _fplayerp = player.transform.position;
        Vector3 _fenemyp = transform.position;
        float _fdistance = Vector2.Distance(_fenemyp , _fplayerp);

       if (_fdistance <= 6) { 

        if (ffrequency < firetimecount)
        {
            ffrequency += Time.deltaTime;

        }

        else
        {

            var enemybullet = Instantiate(ebullet, wshoot.position, transform.rotation, bulletcontainer );
            enemybullet.GetComponent<Rigidbody2D>().AddForce(wshoot.up * 5, ForceMode2D.Impulse);
            
            Destroy(enemybullet, 2f);
           
            ffrequency = 0f;

        }
       }
    }


        private void resetlocation()
    {
        var spawnX = UnityEngine.Random.Range(0f, 1f);
        if (spawnX < 0.5f)
        {
            spawnX = 0 - UnityEngine.Random.Range(0f, 1f);
        }
        else
        {
            spawnX = 1 + UnityEngine.Random.Range(0f, 1f);
        }

        var spawnY = UnityEngine.Random.Range(0f, 1f);
        if (spawnY < 0.5f)
        {
            spawnY = 0 - UnityEngine.Random.Range(0f, 1f);
        }
        else
        {
            spawnY = 1 + UnityEngine.Random.Range(0f, 1f);
        }

        var camedge = cam.ViewportToWorldPoint(new Vector3(spawnX, spawnY, 0));
        transform.position = camedge;
    }

    private void OnDisable()
    {
        if (currentHealth <= 0)
        {
            UI ui = killcount.GetComponent<UI>();
            ui.killed = ui.killed + 1;
            gameObject.SetActive(false);
        }
        currentHealth = enemyStats.enemyHealth;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            
            currentHealth--;
            //Debug.Log("Enemy current health" + currentHealth);
            

            if(currentHealth <= 0) 
            { 
             gameObject.SetActive(false);
             resetlocation(); 
            }

             
           
        }

    }


}
