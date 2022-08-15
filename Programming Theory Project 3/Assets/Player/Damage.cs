using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int maxHealth = 100;
    public int damageSword = 10;
    public float damageExplosion = 35;
    public float currentHealth;

    [SerializeField] HealthBar healthBar;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sword_Of_Spider(Clone)")
        {
            currentHealth -= damageSword;
            healthBar.setHealth(currentHealth);
        }

        if (collision.gameObject.name == "Damage(Clone)")
        {
            currentHealth -= damageExplosion/2;
            healthBar.setHealth(currentHealth);
        }
    }
}
