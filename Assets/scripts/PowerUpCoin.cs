using UnityEngine;

public class PowerUpCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.IncrementBullets();
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound("ammo");
            }
            Destroy(gameObject); // Remove coin
        }

    }
}
