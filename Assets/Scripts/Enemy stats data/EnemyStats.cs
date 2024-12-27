using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Enemies/Enemy")]

public class EnemyStats : ScriptableObject
{
    [field: SerializeField] public float enemyHealth { get; private set; }
    [field: SerializeField] public float enemySpeed { get; private set; }
    [field: SerializeField] public float enemyDamage { get; private set; }

}
