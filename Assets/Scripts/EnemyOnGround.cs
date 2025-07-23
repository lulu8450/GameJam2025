using UnityEngine;

public class EnemyOnGround : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private int moveDirection = 1;

    [Header("Barre de vie")]
    public float maxHealth = 100f;
    private float currentHealth;



    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO
        // Animation, effets, score, etc.
        Destroy(gameObject);
    }

    public Transform healthBar; // À assigner dans l’inspecteur

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float scale = Mathf.Clamp01(currentHealth / maxHealth);
            Vector3 localScale = healthBar.localScale;
            // On ne modifie que la largeur (axe X), pas la hauteur (axe Y) ni la profondeur (axe Z) 
            healthBar.localScale = new Vector3(scale, localScale.y, localScale.z);
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        bool onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (!onGround)
        {
            Flip();
        }
    }

    void Flip()
    {
        moveDirection *= -1;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


}
