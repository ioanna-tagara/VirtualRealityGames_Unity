using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;        // Main health bar (instant)
    public Slider easeHealthSlider;    // Smooth easing health bar
    private float lerpSpeed = 5f;      // Speed for smooth effect

    // Initialize the health bar
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        easeHealthSlider.maxValue = health;
        easeHealthSlider.value = health;
    }

    // Update health instantly and smoothly decrease ease bar
    public void SetHealth(float health)
    {
        healthSlider.value = health; // Instantly update main bar
        StartCoroutine(SmoothHealthBar(health)); // Smooth effect for ease bar
    }

    // Coroutine to smoothly reduce the eased health bar
    private IEnumerator SmoothHealthBar(float health)
    {
        while (easeHealthSlider.value > health) // Only decrease, not increase
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, Time.deltaTime * lerpSpeed);
            yield return null;
        }
        easeHealthSlider.value = health; // Ensure it reaches exact health
    }
}
