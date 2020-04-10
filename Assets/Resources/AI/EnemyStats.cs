using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float speed = 1;
    public int MaxHealth = 5;
    public float lookRange = 40f;
    public float lookSphereCastRadius = 1f;

    public float attackRange = 1f;
    public float attackRate = 1f;
    public int attackDamage = 1;

    public float searchDuration = 4f;
}
