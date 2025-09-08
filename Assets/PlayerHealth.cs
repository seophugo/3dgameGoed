using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("PlayerHealth: healthSlider is NOT assigned in inspector!");
        }

        Debug.Log("PlayerHealth Start: " + currentHealth);
    }

    public void TakeDamage(int amount)
    {
        Debug.Log($"PlayerHealth.TakeDamage called with {amount} at time {Time.time}");
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }
}
