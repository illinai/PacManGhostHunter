using UnityEngine;

public class Coins : MonoBehaviour
{

    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GameManager.instance.AddScore(1); // Add to score
            gm.incrementScore();
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound("coin_collect");
            }
            Destroy(gameObject); // Remove coin
        }

    }
}
