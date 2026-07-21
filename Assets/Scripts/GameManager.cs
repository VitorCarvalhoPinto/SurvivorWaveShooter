using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private UIKillCounter uiKillCounter;
    [SerializeField] private UICountDown uiCountDown;
    [SerializeField] private EnemySpawn[] enemySpawns;
    [SerializeField] private int countdownSeconds = 5;

    public int killCount { get; private set; }
    private int aliveEnemies;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    

    public void RegisterEnemySpawned()
    {
        aliveEnemies++;
    }

    public void AddKill()
    {
        killCount++;
        aliveEnemies--;
        uiKillCounter.UpdateKillCounter();
    }

    public int GetKillAmount()
    {
        return killCount;
    }
    
    private IEnumerator WaveLoop()
    {
        while (true)
        {
            yield return StartCoroutine(RunCountdown());

            var spawnRoutines = new Coroutine[enemySpawns.Length];
            for (int i = 0; i < enemySpawns.Length; i++)
            {
                spawnRoutines[i] = StartCoroutine(enemySpawns[i].SpawnWave());
            }

            foreach (Coroutine spawnRoutine in spawnRoutines)
            {
                yield return spawnRoutine;
            }

            yield return new WaitUntil(() => aliveEnemies <= 0);
        }
    }

    private IEnumerator RunCountdown()
    {
        float secondsLeft = countdownSeconds;

        while (secondsLeft > 0f)
        {
            uiCountDown.UpdateCountDown(secondsLeft);
            yield return null;
            secondsLeft -= Time.deltaTime;
        }

        uiCountDown.UpdateCountDown(0f);
    }
}
