using UnityEngine;
using UnityEngine.UI;

public class PCEnemyInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public float interactionDistance = 2f;
    public float holdDuration = 2f;
    public KeyCode interactionKey = KeyCode.E;

    private GameObject player;
    private float holdTimer = 0f;
    private bool isPlayerNear = false;
    private bool isDisabled = false;

    [Header("UI")]
    public Slider progressBar;       // barre de progression (facultatif)
    public Text progressText;        // texte de progression en pourcentage

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (progressBar != null)
            progressBar.gameObject.SetActive(false);

        if (progressText != null)
            progressText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isDisabled || player == null)
        {
            if (progressBar != null) progressBar.gameObject.SetActive(false);
            if (progressText != null) progressText.gameObject.SetActive(false);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        isPlayerNear = distance <= interactionDistance;

        if (isPlayerNear && Input.GetKey(interactionKey))
        {
            holdTimer += Time.deltaTime;

            float progress = Mathf.Clamp01(holdTimer / holdDuration);

            // Affichage barre
            if (progressBar != null)
            {
                progressBar.gameObject.SetActive(true);
                progressBar.value = progress;
            }

            // Affichage texte
            if (progressText != null)
            {
                progressText.gameObject.SetActive(true);
                progressText.text = $"{Mathf.RoundToInt(progress * 100)} %";
            }

            if (holdTimer >= holdDuration)
            {
                DisablePC();
            }
        }
        else
        {
            holdTimer = 0f;

            if (progressBar != null)
            {
                progressBar.value = 0f;
                progressBar.gameObject.SetActive(false);
            }

            if (progressText != null)
            {
                progressText.gameObject.SetActive(false);
                progressText.text = "";
            }
        }
    }

    private void DisablePC()
    {
        isDisabled = true;

        if (progressBar != null)
            progressBar.gameObject.SetActive(false);

        if (progressText != null)
        {
            progressText.text = "DÉSACTIVÉ";
        }

        // Tu peux ajouter ici une animation, un changement de sprite, ou désactivation logique
        Debug.Log("PC Enemy désactivé.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
