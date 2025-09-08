using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public EnemySpawner spawner;
    public Slider healthBar;

    public float speed = 3f;         // snelheid van enemy
    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        player = GameObject.FindWithTag("Player").transform; // zoekt de speler
    }

    void Update()
    {
        // Healthbar draait altijd naar camera
        if (healthBar != null)
            healthBar.transform.rotation = Camera.main.transform.rotation;

        // Enemy volgt speler
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.value = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        if (spawner != null)
            spawner.OnEnemyKilled(gameObject);

        Destroy(gameObject);
    }
}
