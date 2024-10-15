
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public void takeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
        void Die()
        {
            Destroy(gameObject);
        }
    }
}
