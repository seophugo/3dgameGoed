using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime); // verdwijnt automatisch na 2 sec
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Als het een enemy raakt
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // bullet weg na hit
        }
        // Als het iets anders raakt (muur, vloer, etc)
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject); // raak je iets anders? Bullet ook weg
        }
    }
}