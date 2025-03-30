using UnityEngine;

public class Coins : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterCoin(this);
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UnregisterCoin(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.IncrementScore();
            }

            if (AudioManager.Instance != null)
            {
                //AudioManager.Instance.PlaySound("coin_collect");
            }
            Destroy(gameObject); // Remove coin
        }
    }
}
