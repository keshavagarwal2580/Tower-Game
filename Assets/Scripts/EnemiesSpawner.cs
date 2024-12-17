//using System.Collections;
//using UnityEngine;
//using UnityEngine.AI;

//public class EnemiesSpawner : MonoBehaviour
//{
//    [SerializeField] private GameObject[] enemies;  // The pre-assigned enemies in the Inspector
//    [SerializeField] private int initialWaveCount = 10; // Enemies in the first wave
//    [SerializeField] private float spawnInterval = 0.1f; // Time between spawns in a wave
//    [SerializeField] private float waveDelay = 2f; // Time between waves
//    [SerializeField] private Transform target; // The target to which enemies will move

//    private int currentWaveCount;
//    private bool waveInProgress = false;

//    void Start()
//    {
//        foreach (var enemy in enemies)
//        {
//            enemy.SetActive(false);
//        }
//        currentWaveCount = initialWaveCount;
//        StartCoroutine(SpawnWave());
//    }

//    IEnumerator SpawnWave()
//    {
//        while (true)
//        {
//            if (!waveInProgress)
//            {
//                waveInProgress = true;

//                // Activate the enemies for this wave
//                for (int i = 0; i < currentWaveCount; i++)
//                {
//                    GameObject enemy = enemies[i];

//                    if (enemy != null && !enemy.activeInHierarchy)
//                    {
//                        enemy.SetActive(true); // Activate the enemy

//                        // Start the movement of the enemy towards the target
//                        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
//                        if (enemyMovement != null)
//                        {
//                            enemyMovement.MoveToTarget();
//                        }
//                    }

//                    yield return new WaitForSeconds(spawnInterval); // Wait for the next enemy to spawn
//                }

//                // Wait for all enemies in this wave to finish moving
//                yield return new WaitUntil(() => AllEnemiesHandled());

//                // After finishing the wave, deactivate all the active enemies
//                yield return new WaitForSeconds(waveDelay); // Delay before next wave

//                // Return all active enemies to their original position
//                ReturnEnemiesToOriginalPosition();

//                // Wait for all enemies to return before starting the next wave
//                yield return new WaitUntil(() => AllEnemiesReturned());

//                // Increase the number of enemies for the next wave
//                currentWaveCount = Mathf.Min(currentWaveCount + 10, enemies.Length); // Ensure it doesn't exceed total enemies
//                waveInProgress = false;
//            }
//            else
//            {
//                // Wait until the current wave is finished
//                yield return null;
//            }
//        }
//    }

//    bool AllEnemiesHandled()
//    {
//        foreach (var enemy in enemies)
//        {
//            if (enemy.activeInHierarchy)
//            {
//                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
//                if (agent != null && agent.pathPending) // If the path is still pending, the enemy is still moving
//                {
//                    return false;
//                }
//            }
//        }
//        return true; // All enemies have finished their movement
//    }

//    void ReturnEnemiesToOriginalPosition()
//    {
//        foreach (var enemy in enemies)
//        {
//            if (enemy.activeInHierarchy)
//            {
//                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
//                if (enemyMovement != null)
//                {
//                    enemyMovement.ReturnToOriginalPosition();
//                }
//            }
//        }
//    }

//    bool AllEnemiesReturned()
//    {
//        foreach (var enemy in enemies)
//        {
//            if (enemy.activeInHierarchy)
//            {
//                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
//                if (agent != null && agent.pathPending) // If the path is still pending, the enemy is still moving
//                {
//                    return false;
//                }
//            }
//        }
//        return true; // All enemies have returned to their original position
//    }
//}



using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;  // The pre-assigned enemies in the Inspector
    [SerializeField] private int initialWaveCount = 10; // Enemies in the first wave
    [SerializeField] private float spawnInterval = 0.1f; // Time between spawns in a wave
    [SerializeField] private float waveDelay = 2f; // Time between waves

    private int currentWaveCount;
    private bool waveInProgress = false;

    void Start()
    {
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }
        currentWaveCount = initialWaveCount;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            if (!waveInProgress)
            {
                waveInProgress = true;

                // Activate the enemies for this wave
                for (int i = 0; i < currentWaveCount; i++)
                {
                    GameObject enemy = enemies[i];

                    if (enemy != null && !enemy.activeInHierarchy)
                    {
                        ResetEnemyPosition(enemy);
                        enemy.SetActive(true); // Activate the enemy

                        // Start the movement of the enemy towards the target
                        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                        if (enemyMovement != null)
                        {
                            enemyMovement.MoveToTarget();
                        }
                    }

                    yield return new WaitForSeconds(spawnInterval); // Wait for the next enemy to spawn
                }

                // Wait for all enemies in this wave to be handled
                yield return new WaitUntil(() => AllEnemiesHandled());

                // Delay before the next wave
                yield return new WaitForSeconds(waveDelay);

                // Increase the number of enemies for the next wave
                currentWaveCount = Mathf.Min(currentWaveCount + 10, enemies.Length); // Ensure it doesn't exceed total enemies
                waveInProgress = false;
            }
            else
            {
                // Wait until the current wave is finished
                yield return null;
            }
        }
    }

    void ResetEnemyPosition(GameObject enemy)
    {
        if (enemy != null)
        {
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.ResetPosition();
            }
        }
    }

    bool AllEnemiesHandled()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                return false;
            }
        }
        return true; // All enemies have been deactivated
    }
}
