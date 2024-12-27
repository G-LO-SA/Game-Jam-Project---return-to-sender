using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove;
    [SerializeField] AudioSource sound;

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.CompareTag("EnemyBullet"))
        {
           playerMove.bulletamount++;
           sound.Play();
            
        }
    }
}
