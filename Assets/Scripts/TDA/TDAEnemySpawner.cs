using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    Slow,
    Regular,
    Fast
}

public class TDAEnemySpawner : MonoBehaviour
{
    [SerializeField] private TDAEnemy2[] tdaEnemiesPrefabs;
    [SerializeField] private Transform[] tdaEnemiesWaypoints;
    [SerializeField] private int amountToInstantiate = 2;
    [SerializeField] private TDAEnemy2 slowTDAEnemy, regularTDAEnemy, fastTDAEnemy;
    [SerializeField] private EnemyType enemyToSpawn;
    private Dictionary<EnemyType, TDAEnemy2> enemyTypesDictionary =
    new Dictionary<EnemyType, TDAEnemy2>();
    private void Awake()
    {
        PopulateDictionary();
    }
    private void PopulateDictionary()
    {
        enemyTypesDictionary.Add(EnemyType.Slow, slowTDAEnemy);
        enemyTypesDictionary.Add(EnemyType.Regular, regularTDAEnemy);
        enemyTypesDictionary.Add(EnemyType.Fast, fastTDAEnemy);
    }

    private void SpawnEnemy(TDAEnemy2 p_enemyToSpawn)
    {
        var l_spawnPosition = GetRandomWaypoint().position;
        var l_currEnemy = Instantiate(p_enemyToSpawn, l_spawnPosition, Quaternion.identity);
        l_currEnemy.ReceiveWaypoints(tdaEnemiesWaypoints);
        l_currEnemy.Init();
    }
    private Transform GetRandomWaypoint()
    {
        var l_chosenWaypoint = Random.Range(0, tdaEnemiesWaypoints.Length);
        return tdaEnemiesWaypoints[l_chosenWaypoint];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K key pressed...");
            if (enemyTypesDictionary.TryGetValue(enemyToSpawn, out var l_enemyToSpawn))
            {
                SpawnEnemy(l_enemyToSpawn);
            }
        }
    }
}
