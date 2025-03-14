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
        if (lastCheckpointPosition.CurrentValue != null){
            transform.position = lastCheckpointPosition.CurrentValue.Value;
        }else{
            lastCheckpointPosition.CurrentValue = transform.position;
        }
        currentSpawnPosition = gameObject.transform.position;
        initialSpawnPosition = gameObject.transform.position;
    }
}
