using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 25f; // Amount of health to restore

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth script from the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // If the player has the PlayerHealth component, restore health
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healthAmount);
                Destroy(gameObject); // Destroy the health kit after pickup
            }
        }
    }
}