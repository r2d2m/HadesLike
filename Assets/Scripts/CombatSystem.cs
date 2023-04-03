using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public int damage = 10; // Damage dealt when attacking
    public float attackRange = 1f; // Range of the attack
    public LayerMask targetLayer; // Layer of the target to be attacked

    protected bool IsTargetInRange(Transform target)
    {
        // Calculate the distance between the attacker and the target
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Check if the target is within the attack range
        return distanceToTarget <= attackRange;
    }

    protected void Attack(Transform target)
    {
        if (IsTargetInRange(target))
        {
            // Get the target's HealthSystem component
            HealthSystem targetHealth = target.GetComponent<HealthSystem>();

            // Apply damage to the target's health
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }
    }
}
