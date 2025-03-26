using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Vector3Variable lastCheckpointPosition;
    
    [Tooltip("Define where the player will spawn if there is an issue"), ReadOnlyInspector]
    public Vector3 currentSpawnPosition;

    [Tooltip("Define where the player started the level"), ReadOnlyInspector]
    public Vector3 initialSpawnPosition;

    private void Awake()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position; // Déplace le joueur au spawn défini dans la scène
        }
        else if (lastCheckpointPosition.CurrentValue != null)
        {
            transform.position = lastCheckpointPosition.CurrentValue.Value; // Utilise le dernier checkpoint
        }
        else
        {
            lastCheckpointPosition.CurrentValue = transform.position; // Sauvegarde la position de départ
        }

        currentSpawnPosition = transform.position;
        initialSpawnPosition = transform.position;
    }
}
