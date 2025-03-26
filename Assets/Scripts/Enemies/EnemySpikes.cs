using UnityEngine;

public class EnemySpikes : MonoBehaviour
{
    [Tooltip("Indique si les piques sont sortis")]
    public bool areSpikesOut = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Méthode appelée par l'animation pour synchroniser l'état des piques
    public void SetSpikesState(bool state)
    {
        areSpikesOut = state;
    }
}