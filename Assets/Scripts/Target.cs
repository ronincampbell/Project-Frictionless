using UnityEngine;

public class Target : MonoBehaviour
{
    // Declare necessary variables
    public float health = 50f;

    // When called, check if the damage taken is more than the objects heath - if so, die
    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    // Destroy the gameObject (duh)
    private void Die()
    {
        Destroy(gameObject);
    }
}
