using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject toShoot;
    [SerializeField] AudioSource hurt;
    [SerializeField] Transform wshoot;
    [SerializeField] Transform Parent;
    [SerializeField] GameObject Catcher;
    [SerializeField] GameObject FullWarning;
    [SerializeField] GameObject ReloadWarning;
    [SerializeField] GameObject exit;
    [SerializeField] float maxhealth = 30;
    [SerializeField] Image Bullet;
    [SerializeField] Image healhtfill;
    [SerializeField] Animator animator;
    [HideInInspector] public float bulletamount;
    [HideInInspector] public float currenthealth;
    [HideInInspector] public bool gothealth;
    

    private void Awake()
    {
        currenthealth = maxhealth;
        bulletamount = 5;
    }
    void Update()
    {
        if (Time.timeScale == 1)
        {
            mmovement();
            shieldup();
            bullets();
        }
       

       if(bulletamount <= 0) {
            bulletamount = 0;
            ReloadWarning.SetActive(true);
            gothealth = false;
        }

       else if (bulletamount >= 10) {

            bulletamount = 10;            
            if (currenthealth < maxhealth && gothealth == false)
            {
                currenthealth += 10;
                gothealth = true;
                Debug.Log("You are now in " + currenthealth);
            }
            
            
            else if (currenthealth >= maxhealth)
            {
                currenthealth = maxhealth;
                Debug.Log("You are still full");
            }

            ReloadWarning.SetActive(false);
        }


        Bullet.fillAmount = bulletamount / 10f;
       healhtfill.fillAmount = currenthealth / maxhealth;
    } 

    private void mmovement()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 fdirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = fdirection;
    }

    private void bullets()
    {
        if (Input.GetMouseButtonDown(0) && (bulletamount > 0 && bulletamount <= 10))
        {
           var bullet = Instantiate(toShoot, wshoot.position, transform.rotation);
           bullet.GetComponent<Rigidbody2D>().AddForce(wshoot.position * 40, ForceMode2D.Impulse);
           Destroy(bullet, 3f);
           animator.SetBool("Shoot", true);
           bulletamount--;
           
        }
        
        else if (Input.GetMouseButtonDown (0) == false) 
        {
            animator.SetBool("Shoot", false);

        }

        if (bulletamount > 0 && bulletamount <= 10)
        {
            ReloadWarning.SetActive(false);
        }

    }

    private void shieldup()
    {
       
        if (Input.GetMouseButtonDown(1)  && bulletamount < 10 )
        {
            Catcher.SetActive(true);
        }

        else if (Input.GetMouseButtonUp(1) || bulletamount == 10)
        {
            Catcher.SetActive(false);
        }



        if (bulletamount < 10)
        {
         FullWarning.SetActive(false);
        }

        else if (bulletamount == 10)
        {
         FullWarning.SetActive(true);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletDamage bulletenemy = other.GetComponent<BulletDamage>();


        if (bulletenemy && currenthealth <= maxhealth)
        {
            currenthealth -= bulletenemy.damaging;
            Debug.Log("You getting damaged " + currenthealth);
            hurt.Play();
            Destroy(bulletenemy.gameObject);
        }

        if (currenthealth <= 0)
        {
            Debug.Log("you are dead");
            exit.SetActive(true);
            Time.timeScale = 0;
            Destroy(bulletenemy.gameObject);
        }

        
    }


}
