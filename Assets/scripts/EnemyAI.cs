using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    private NavMeshAgent agent;
    private Coroutine destroyRoutine = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Transform playerTransform = ResetManager.Instance.GetPlayerTransform();
        if (playerTransform != null)
        {
            player = playerTransform;
        }
        else
        {
            Debug.LogError("Enemy AI: Player reference not found!");
        }
        ResetManager.Instance.RegisterObject(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyRoutine != null) return;
        if (!other.gameObject.CompareTag("bullet")) return;
        destroyRoutine = StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // two physics frames to ensure proper collision
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound("win");
        }

        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        int enemyCount = ResetManager.Instance.GetEnemyCount();

        //Destroy(gameObject);
        if (enemyCount <= 1)
        {
            // If it's the last enemy, reset it instead of destroying
            Debug.Log("Only one enemy left, resetting.");
            ResetManager.Instance.ResetObject(transform);
        }
        else
        {
            // Otherwise, remove this enemy from the game
            Debug.Log("Multiple enemies left, removing this one.");
            Debug.Log(enemyCount);
            ResetManager.Instance.RemoveEnemy(transform);
            yield break; // Stop coroutine to prevent further execution
        }

        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(true);
        agent.isStopped = false;
        agent.SetDestination(player.position);

        destroyRoutine = null;
    }

    public void AssignPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    void Update()
    {
        if (player != null && agent.enabled && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position); // Enemy chases player
        }
    }
}

