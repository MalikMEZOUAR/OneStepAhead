using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    public Enemy enemy;

    [Header("Extra non mandatory components")]
    public EnemySplitting enemySplitting;
    public EnemyUnshelled enemyUnshelled;
    public EnemySpikes enemySpikes;

    public void Hurt()
    {
        if (enemy != null)
        {
            enemy.Hurt();
        }

        if (enemySplitting != null)
        {
            enemySplitting.Hurt();
        }

        if (enemyUnshelled != null)
        {
            enemyUnshelled.Hurt();
        }
        if (enemySpikes != null && enemySpikes.areSpikesOut)
        {
            Debug.Log("Impossible de frapper la tortue ! Les piques sont sortis !");
            return; // Empêche de frapper si les piques sont dehors
        }
    }
}
