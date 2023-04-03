using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    public int maxHealth = 100; // The maximum health value
    private int currentHealth; // The current health value

    void Start() {
        currentHealth = maxHealth; // Set the current health to the maximum health at the start of the game
    }

    public void TakeDamage(int damage) {
        // Reduce the current health by the damage value
        currentHealth -= damage;

        // Clamp the current health value between 0 and maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Check if the character's health has reached 0
        if (currentHealth <= 0) {
            Die();
        }
    }

    public void Heal(int healAmount) {
        // Increase the current health by the heal amount
        currentHealth += healAmount;

        // Clamp the current health value between 0 and maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die() {
        // Handle the character's death (e.g., play a death animation, disable the character, etc.)
        // This method can be customized for the Player and Enemy GameObjects as needed
        // Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
