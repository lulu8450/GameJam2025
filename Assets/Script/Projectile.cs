using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    private Vector2 direction;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyOnGround enemy = other.GetComponent<EnemyOnGround>();
            if (enemy != null)
            {
                enemy.TakeDamage(25f); // Valeur des dégâts, à ajuster
            }
            Destroy(gameObject);
        }
    }

}
