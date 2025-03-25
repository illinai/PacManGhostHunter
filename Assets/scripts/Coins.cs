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
            Destroy(gameObject); // Remove coin
        }

    }
}
