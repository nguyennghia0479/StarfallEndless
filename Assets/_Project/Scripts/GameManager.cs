using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int enemiesKill;
    private int bossesKill;

    private void OnEnable()
    {
        GameEvents.OnGameRetry += ResetCount;
        GameEvents.OnEnemyDestroyed += CountEnemiesKill;
    }

    private void OnDisable()
    {
        GameEvents.OnGameRetry -= ResetCount;
        GameEvents.OnEnemyDestroyed -= CountEnemiesKill;
    }

    private void ResetCount()
    {
        enemiesKill = 0;
        bossesKill = 0;
    }

    private void CountEnemiesKill(Enemy enemy)
    {
        if (enemy.IsBoss)
            bossesKill++;
        else
            enemiesKill++;
    }

    public int EnemiesKill => enemiesKill;
    public int BossesKill => bossesKill;
}
