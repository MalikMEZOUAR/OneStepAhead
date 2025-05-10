using UnityEngine;
using System.Collections; 

[RequireComponent(typeof(BoxCollider2D))]
public class EndLevel : MonoBehaviour
{
    [Header("Effets Visuels")]
    public ParticleSystem particles;

    [Header("Sons")]
    public AudioClip audioClip;
    public AudioClip victorySound;
    private AudioSource audioSource;

    [Space(10)]
    [Header("Scene's name to load after the collider is triggered")]
    public string nextLevelName;

    [Space(10)]
    [Header("Broadcast event channels")]
    public StringEventChannel onLevelEnded;
    public PlaySoundAtEventChannel sfxAudioChannel;

    private bool hasBeenTriggered = false;
    public float delayBeforeNextLevel = 2f; // Délai de 2 secondes pour entendre le son

    private void Start()
    {
        // Ajout automatique de l'AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true;

            if (nextLevelName != null)
            {
                particles.Play();

                if (victorySound != null)
                {
                    Debug.Log("🎉 Son de victoire joué !");
                    audioSource.PlayOneShot(victorySound);
                }
                else
                {
                    Debug.LogWarning("⚠️ Aucun son de victoire assigné.");
                }

                // On attend un peu avant de charger le prochain niveau**
                StartCoroutine(LoadNextLevelWithDelay());
            } 
            else 
            {
                Debug.LogError("Niveau manquant !");
            }
        }
    }

    // **Coroutine pour attendre avant de changer de niveau**
    IEnumerator LoadNextLevelWithDelay()
    {
        Debug.Log($"Attente de {delayBeforeNextLevel} secondes avant de changer de niveau...");
        yield return new WaitForSeconds(delayBeforeNextLevel);

        Debug.Log("Fin du niveau, chargement du suivant : " + nextLevelName);
        onLevelEnded.Raise(nextLevelName);
    }
}
