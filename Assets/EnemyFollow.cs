using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public int damage = 10;
    public float attackCooldown = 1f;

    private float lastAttackTime;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Debug.Log($"{name} attacking {other.name} at {Time.time}");
                player.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(name + " OnTriggerExit with " + other.name);
    }
}
