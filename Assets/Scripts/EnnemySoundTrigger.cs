using UnityEngine;

public class EnemySoundTrigger : MonoBehaviour
{
    public AudioClip hitSound; //  Le son 
    public float volume = 0.5f; // Le volume du son

    // Détection de collision avec le joueur**
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Joueur a touché l'ennemi !");

            // === Création d'un GameObject temporaire pour jouer le son ===
            GameObject tempAudio = new GameObject("TempAudio");
            tempAudio.transform.position = transform.position;

            // Ajoute un AudioSource, configure et joue le son
            AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
            audioSource.clip = hitSound;
            audioSource.volume = volume;
            audioSource.Play();

            // Destruction de l'objet après la fin du son
            Destroy(tempAudio, hitSound.length);
        }
    }
}
