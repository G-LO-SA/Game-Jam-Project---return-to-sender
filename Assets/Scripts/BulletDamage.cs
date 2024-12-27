using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] EnemyStats enemyStats;

    public float damaging => enemyStats.enemyDamage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Catch"))
        {
            Destroy(gameObject);
        }
    }

}
