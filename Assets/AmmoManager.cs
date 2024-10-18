using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    public int maxAmmo = 10; // Adjust to your game's max ammo
    public int currentAmmo;
    public Slider ammoBar;

    void Start()
    {
        currentAmmo = maxAmmo;
        ammoBar.maxValue = maxAmmo;
        ammoBar.value = currentAmmo;
    }

    public void UseAmmo(int amount)
    {
        currentAmmo -= amount;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo); // Ensure current ammo doesn't go below 0
        ammoBar.value = currentAmmo; // Update the slider
    }

    public void ReloadAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        ammoBar.value = currentAmmo; // Update the slider
    }
}