using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int coinValue = 10;
    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterCoin(this);
        }
    }
    private void OnDestroy()
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
                GameManager.Instance.IncrementScore(coinValue);
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound("coin_collect");
            }
            Destroy(gameObject); // Remove coin
        }
    }
}
