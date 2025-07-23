using UnityEngine;

public class PCEnemyInteraction : MonoBehaviour
{
    [Header("Paramètres d'interaction")]
    public float interactionDistance = 2f;       // Distance maximale à partir de laquelle le joueur peut interagir
    public float holdDuration = 2f;              // Durée nécessaire de maintien du bouton pour désactiver le PC
    public KeyCode interactionKey = KeyCode.E;   // Touche à maintenir

    private GameObject player;                   // Référence au joueur
    private float holdTimer = 0f;                // Chronomètre de maintien du bouton
    private bool isPlayerNear = false;           // Le joueur est-il à portée ?
    private bool isDisabled = false;             // Le PC a-t-il déjà été désactivé ?

    private void Start()
    {
        // Trouve automatiquement le joueur en utilisant le tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isDisabled || player == null)
            return;

        // Vérifie la distance entre le joueur et le PC Enemy
        float distance = Vector2.Distance(transform.position, player.transform.position);
        isPlayerNear = distance <= interactionDistance;

        // Si le joueur est proche et maintient la touche
        if (isPlayerNear && Input.GetKey(interactionKey))
        {
            // Incrémente le timer
            holdTimer += Time.deltaTime;

            // (Optionnel) Afficher la progression d'interaction à l'écran
            Debug.Log($"Interaction en cours : {Mathf.Clamp01(holdTimer / holdDuration) * 100f:F0}%");

            // Si le temps de maintien est suffisant, désactiver
            if (holdTimer >= holdDuration)
            {
                DisablePC();
            }
        }
        else
        {
            // Si la touche n’est pas maintenue ou le joueur s’éloigne, reset du timer
            if (holdTimer > 0f)
            {
                Debug.Log("Interaction annulée.");
            }
            holdTimer = 0f;
        }
    }

    private void DisablePC()
    {
        isDisabled = true;
        Debug.Log("PC Enemy désactivé.");

        //TODO
        // Action à effectuer lors de la désactivation (exemples ci-dessous) :

        // 1. Désactiver l'objet
        // gameObject.SetActive(false);

        // 2. Désactiver un script d'ennemi si rattaché au PC
        // GetComponent<EnemyOnGround>()?.enabled = false;

        // 3. Changer l'apparence visuelle (par exemple, sprite éteint)
        // GetComponent<SpriteRenderer>().color = Color.gray;

        // 4. Déclencher une animation ou un son
        // GetComponent<Animator>()?.SetTrigger("Off");

        // Ajoute ici ce que tu veux que le PC fasse une fois désactivé
    }
}
