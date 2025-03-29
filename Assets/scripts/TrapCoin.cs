using UnityEngine;

public class TrapCoin : MonoBehaviour
{
    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trap Coin collected! Spawning extra enemy.");

            ResetManager.Instance.SpawnExtraEnemy();

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound("trap");
            }
            Destroy(gameObject); // Remove coin
        }

    }
}
