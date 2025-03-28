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
        //Destroy(gameObject);
        ResetManager.Instance.ResetObject(transform);

        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(true);
        agent.isStopped = false;
        agent.SetDestination(player.position);

        destroyRoutine = null;
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // Enemy chases player
        }
    }
}

