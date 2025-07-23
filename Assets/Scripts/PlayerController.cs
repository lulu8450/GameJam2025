using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;      // Vitesse horizontale du joueur
    public float jumpForce = 7f;      // Intensité du saut
    private bool isAbove = false;  // Le joueur est-il au sol ?

    private Rigidbody2D rb;           // Référence au Rigidbody2D du joueur

    void Start()
    {
        // On récupère le composant Rigidbody2D à l'initialisation
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        isAbove = isJumping();
    }

    bool isJumping()
    {
        return Mathf.Abs(rb.linearVelocity.y) < 0.05f;
    }

    void Update()
    {
        // Lecture de l'axe horizontal (clavier : flèches ou A/D)
        float moveInput = Input.GetAxisRaw("Horizontal");
        // Application du déplacement horizontal (input manette) tout en conservant la vitesse verticale actuelle
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        // Saut : la touche Espace déclenche le saut uniquement si le joueur est bien au sol
        if (Input.GetKey("up") && isAbove)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

}
