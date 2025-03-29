using UnityEngine;

public class PowerUpCoin : MonoBehaviour
{
    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.IncrementBullet();
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound("ammo");
            }
            Destroy(gameObject); // Remove coin
        }

    }
}
