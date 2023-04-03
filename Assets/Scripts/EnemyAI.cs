using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : CombatSystem {
    public float speed = 3f; // The movement speed of the enemy
    public float followDistance = 5f; // The distance threshold for the enemy to start following the player

    private Rigidbody2D rb; // Reference to the enemy's Rigidbody2D component
    private Transform playerTransform; // Reference to the player's Transform component

    public float attackCooldown = 2f; // The time in seconds between each attack
    private float attackTimer; // A timer to keep track of the time since the last attack

    void Start() {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        playerTransform = GameObject.FindWithTag("Player").transform; // Find and store the player's Transform component
    }

    void Update() {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Check if the distance is within the followDistance threshold
        if (distanceToPlayer <= followDistance) {
            // Calculate the direction towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Move the enemy using the Rigidbody2D
            rb.velocity = direction * speed;
        } else {
            // If the player is too far away, stop moving
            rb.velocity = Vector2.zero;
        }

        // Update the attack timer
        attackTimer += Time.deltaTime;

        if (IsTargetInRange(playerTransform) && attackTimer >= attackCooldown) {
            EnemyAttack();
        }
    }

    private void EnemyAttack() {
        Attack(playerTransform);

        // Reset the attack timer
        attackTimer = 0f;
    }
}
