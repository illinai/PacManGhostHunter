using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class ResetManager : MonoBehaviour
{
    public static ResetManager Instance {
        get;
        private set;
    }

    public GameObject[] enemyPrefabs;
    private Transform player;

    private List<Transform> activeEnemies = new List<Transform>();
    
    private Dictionary<Transform, (Vector3, Quaternion)> initialStates = new Dictionary<Transform, (Vector3, Quaternion)>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Multiple instances of ResetManager found!");
            Destroy(gameObject);
            return;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure the Player has the 'Player' tag.");
        }
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }

    public void RegisterObject(Transform obj)
    {
        if (obj.CompareTag("Player")) return;

        if (!initialStates.ContainsKey(obj))
        {
            initialStates[obj] = (obj.position, obj.rotation);
        }
        if (!activeEnemies.Contains(obj) && obj.gameObject.activeInHierarchy)
        {
            activeEnemies.Add(obj);
            Debug.Log("Registered enemy: " + obj.name + " | Active enemies: " + activeEnemies.Count);
        }
    }

    public void ResetObject(Transform obj)
    {
        if (initialStates.ContainsKey(obj))
        {
            (Vector3 pos, Quaternion rot) = initialStates[obj];
            obj.position = pos;
            obj.rotation = rot;
            obj.gameObject.SetActive(true);

            if (!activeEnemies.Contains(obj))
            {
                activeEnemies.Add(obj);
                Debug.Log("Reset enemy: " + obj.name + " | Active enemies: " + activeEnemies.Count);
            }
        }
        else
        {
            Debug.LogWarning($"ResetManager: Object {obj.name} was not registered.");
        }
    }

    public void RemoveEnemy(Transform obj)
    {
        if (activeEnemies.Contains(obj))
        {
            activeEnemies.Remove(obj);
            Debug.Log("Removed enemy: " + obj.name + " | Remaining enemies: " + activeEnemies.Count);
        }
        Destroy(obj.gameObject);

        // Ensure at least one enemy exists
        if (activeEnemies.Count == 0)
        {
            Debug.Log("All enemies gone, respawning one...");
            SpawnExtraEnemy();
        }
    }

    public int GetEnemyCount()
    {
        return activeEnemies.Count;
    }

    public void SpawnExtraEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy Prefab not assigned in ResetManager!");
            return;            
        }

        Vector3 spawnPosition = new Vector3(15f, 3f, 0.31f);

        NavMeshHit hit;
        float maxDistance = 5.0f;
        if (NavMesh.SamplePosition(spawnPosition, out hit, maxDistance, NavMesh.AllAreas))
        {
            spawnPosition = hit.position; // Adjust to a valid NavMesh location
        }
        else
        {
            Debug.LogError("Spawn position is not on the NavMesh!");
        }

        GameObject selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject newEnemy = Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);

        Debug.Log("Spawned new enemy at " + spawnPosition);
        RegisterObject(newEnemy.transform);

        NavMeshAgent agent = newEnemy.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false; // Temporarily disable to reset correctly
            agent.Warp(spawnPosition); // Move to the correct NavMesh location
            agent.enabled = true; // Re-enable agent to start movement

            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.AssignPlayer(ResetManager.Instance.GetPlayerTransform());
            }
        }
        else
        {
            Debug.LogError($"Spawned enemy {newEnemy.name} is missing a NavMeshAgent!");
        }
    }

}
