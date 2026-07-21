using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float respawnTime = 2f;
    private Transform playerLocation;

    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public IEnumerator SpawnWave()
    {
        if (playerLocation == null)
            yield break;

        for (int i = 0; i < maxEnemies; i++)
        {
            Vector3 relativePos = playerLocation.position - transform.position;
            Quaternion enemyRotation = Quaternion.LookRotation(relativePos, Vector3.up);

            Instantiate(enemyPrefab, transform.position, enemyRotation);
            GameManager.Instance.RegisterEnemySpawned();

            yield return new WaitForSeconds(respawnTime);
        }
    }
}