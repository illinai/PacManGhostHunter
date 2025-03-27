using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    private NavMeshAgent agent;

    private Coroutine destroyRoutine = null;
    private ResetEnemy enemyReset;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyReset = GetComponent<ResetEnemy>();
    }

    private void OnCollisionEnter(Collision other)
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
        //Destroy(gameObject);
        if (enemyReset != null)
        {
            enemyReset.ResetTransform();
        }
        else
        {
            Debug.LogError("EnemyReset script not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // Enemy chases player
        }
    }
}

