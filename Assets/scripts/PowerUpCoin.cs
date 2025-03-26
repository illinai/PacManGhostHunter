using UnityEngine;

public class PowerUpCoin : MonoBehaviour
{
    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GameManager.instance.AddScore(1); // Add to score
            gm.incrementBullet();
            Destroy(gameObject); // Remove coin
        }

    }
}
