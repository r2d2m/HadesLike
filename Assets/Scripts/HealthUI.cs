using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public HealthSystem playerHealthSystem; // Reference to the player's HealthSystem component
    private TextMeshProUGUI healthText; // Reference to the TextMeshProUGUI component

    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component
    }

    void Update()
    {
        // Update the health text to display the player's current health
        healthText.text = "Health: " + playerHealthSystem.GetCurrentHealth();
    }
}
