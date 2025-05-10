using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;

    public SpriteRenderer sr;

    public PlayerInvulnerable playerInvulnerable;

    [Tooltip("Please uncheck it on production")]
    public bool needResetHP = true;

    [Header("ScriptableObjects")]
    public PlayerData playerData;

    [Header("Debug")]
    public VoidEventChannel onDebugDeathEvent;

    [Header("Broadcast event channels")]
    public VoidEventChannel onPlayerDeath;

    [Header("Audio Settings")]
    public AudioSource deathSound; // Ajout de l'AudioSource pour le son de mort
    
    [Header("Camera Shake")]
public CameraShakeEventChannel cameraShakeEvent; // Le canal d'évènement pour le shake
public ShakeTypeVariable damageShakeType; // Le type de secousse pour les dégâts
public ShakeTypeVariable deathShakeType;  // Le type de secousse pour la mort

    private void Awake()
    {
        if (needResetHP || playerData.currentHealth <= 0)
        {
            playerData.currentHealth = playerData.maxHealth;
        }
    }

    private void OnEnable()
    {
        onDebugDeathEvent.OnEventRaised += Die;
    }

    public void TakeDamage(float damage)
    {
        if (playerInvulnerable.isInvulnerable && damage < float.MaxValue) return;

        playerData.currentHealth -= damage;
        if (playerData.currentHealth <= 0)
        {
            Die();
        }

        // Déclencher le Camera Shake à la blessure
        if (cameraShakeEvent != null && damageShakeType != null)
{
    cameraShakeEvent.Raise(damageShakeType);
    Debug.Log("Secousse caméra (blessure) !");
}

        else
        {
            StartCoroutine(playerInvulnerable.Invulnerable());
        }
    }

    private void Die()
    {
      // **Déclencher le Camera Shake à la mort**
if (cameraShakeEvent != null && deathShakeType != null)
{
    cameraShakeEvent.Raise(deathShakeType);
    Debug.Log("Secousse caméra (mort) !");
}  
        onPlayerDeath?.Raise();
        GetComponent<Rigidbody2D>().simulated = false;
        transform.Rotate(0f, 0f, 45f);
        //son pour la mort
         if (deathSound != null && !deathSound.isPlaying)
        {
            deathSound.Play();
        }

        animator.SetTrigger("Death");
    }

    public void OnPlayerDeathAnimationCallback()
    {
        sr.enabled = false;
    }

    private void OnDisable()
    {
        onDebugDeathEvent.OnEventRaised -= Die;
    }
}
