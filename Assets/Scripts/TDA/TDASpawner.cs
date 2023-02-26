using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Slow,
    Regular,
    Fast
}

public class TDASpawner : MonoBehaviour
{
    [SerializeField] private Transform[] tdaEnemiesWaypoints;
    [SerializeField] private int amountToInstantiate = 2;
    [SerializeField] private EnemyType enemyToSpawn;

    private Queue<TDAEnemy2> spawnedEnemies;
    private Stack<TDAEnemy2> spawnedenemiesStack;
    private Dictionary<EnemyType, GameObject> enemyPrefabsDictionary =
        new Dictionary<EnemyType, GameObject>();
    private void Start()
    {
        spawnedEnemies = new Queue<TDAEnemy2>();
        spawnedenemiesStack = new Stack<TDAEnemy2>();

    }

    private void Awake()
    {
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        // Load prefabs and store in dictionary
        enemyPrefabsDictionary.Add(EnemyType.Slow, Resources.Load<GameObject>("Prefabs/SlowEnemy"));
        enemyPrefabsDictionary.Add(EnemyType.Regular, Resources.Load<GameObject>("Prefabs/RegularEnemy"));
        enemyPrefabsDictionary.Add(EnemyType.Fast, Resources.Load<GameObject>("Prefabs/FastEnemy"));
    }

    private void SpawnEnemy(GameObject prefab)
    {
        var l_spawnPosition = GetRandomWaypoint().position;
        var l_currEnemy = Instantiate(prefab, l_spawnPosition, Quaternion.identity).GetComponent<TDAEnemy2>();
        l_currEnemy.ReceiveWaypoints(tdaEnemiesWaypoints);
        l_currEnemy.Init();
        spawnedEnemies.Enqueue(l_currEnemy);
        spawnedenemiesStack.Push(l_currEnemy);
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
            if (enemyPrefabsDictionary.TryGetValue(enemyToSpawn, out var spawnedEnemy))
            {
                SpawnEnemy(spawnedEnemy);
            }
        }
    }
}