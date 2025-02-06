using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100;
    public float health = 100;
    private float lerpSpeed = 0.05f;

    void Start()
    {
        health = maxHealth;
        SetMaxHealth(maxHealth); // Initialize the health bar
    }

    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }
    }

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        easeHealthSlider.maxValue = health;
        easeHealthSlider.value = health;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public void UpdateHealthBar(float newHealth)
    {
        health = newHealth;
        healthSlider.value = health; // Update main health bar
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}