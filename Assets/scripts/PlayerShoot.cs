using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;
    public GameObject bulletPrefab;  // Drag your bullet prefab here in the Inspector
    public Transform shootingPoint;
    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager != null)
        {
            inputManager = gameManager.InputManager;
            if (inputManager != null)
            {
                inputManager.OnSpacePressed.AddListener(ShootBullet);
            }
            else
            {
                Debug.LogWarning("InputManager is missing.");
            }
        }
        else
        {
            Debug.LogError("GameManager is missing.");
        }
    }

    private void ShootBullet()
    {
        if (gameManager != null && gameManager.CanShootBullet())
        {
            Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation); // Spawn a bullet
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound("bullet");
            }
            gameManager.DecreaseBullets();
        }

    }
}
