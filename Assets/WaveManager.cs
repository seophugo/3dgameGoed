// WaveManager.cs
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [Header("Wave Settings")]
    public EnemySpawner enemySpawner;
    public TextMeshProUGUI waveText;

    private int currentWave = 0;
    private bool waveInProgress = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartWave(); // start eerste wave automatisch
    }

    public void StartWave()
    {
        currentWave++;
        waveInProgress = true;

        enemySpawner.SpawnWave(currentWave); // waveNumber bepaalt het aantal enemies
        UpdateWaveText();
    }

    public void OnWaveComplete()
    {
        waveInProgress = false;

        // start automatisch de volgende wave
        StartWave();
    }

    void UpdateWaveText()
    {
        if (waveText != null)
            waveText.text = "Wave " + currentWave;
    }
}
