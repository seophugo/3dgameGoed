// EnemySpawner.cs
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    private List<GameObject> aliveEnemies = new List<GameObject>();

    public void SpawnWave(int waveNumber)
    {
        aliveEnemies.Clear(); // reset lijst voor nieuwe wave

        int enemyCount = 3 + (waveNumber - 1) * 2; // wave1 =3, wave2=5, wave3=7, etc.

        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            aliveEnemies.Add(enemy);

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
                enemyScript.spawner = this; // enemy kan zichzelf melden als hij doodgaat
        }
    }

    public void OnEnemyKilled(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);

        // check of alle enemies dood zijn
        if (aliveEnemies.Count == 0)
        {
            Debug.Log("Wave is klaar!");
            WaveManager.instance.OnWaveComplete();
        }
    }
}
