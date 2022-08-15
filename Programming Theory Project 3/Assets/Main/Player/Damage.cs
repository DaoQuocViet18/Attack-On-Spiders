using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sword_Of_Spider(Clone)" && gameObject.name != "Shield_Player")
        {
            currentHealth -= damageSword;
            healthBar.setHealth(currentHealth);
        }

        if (collision.gameObject.name == "Damage(Clone)")
        {
            currentHealth -= damageExplosion/2;
            healthBar.setHealth(currentHealth);            
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        //Time.timeScale = 0;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
