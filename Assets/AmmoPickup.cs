using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 10; // Amount of ammo to give

    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the player
        if (other.CompareTag("Player"))
        {
            // Get the Weapon script from the player's child objects (like the weapon)
            Weapon weapon = other.GetComponentInChildren<Weapon>();

            // If the player has a weapon, add ammo
            if (weapon != null)
            {
                weapon.AddAmmo(ammoAmount);
                Destroy(gameObject); // Destroy the ammo kit after being picked up
            }
        }
    }
}