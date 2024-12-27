using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamagePlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
