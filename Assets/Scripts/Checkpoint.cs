using UnityEngine;

public class Checkpoint : MonoBehaviour
{
public Animator animator;
    
    public BoxCollider2D bc2d;
    public Vector3Variable lastCheckpointPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Collision");
            PlayerSpawn playerSpawn = collision.GetComponent<PlayerSpawn>();
            if (playerSpawn != null)
            {
                playerSpawn.currentSpawnPosition = transform.position;
                bc2d.enabled = false;
                lastCheckpointPosition.CurrentValue = transform.position;
            }
        }
    }
}
