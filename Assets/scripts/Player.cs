using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;
    [SerializeField] float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform gunTransform;
    private Vector2 moveInput = Vector2.zero;

    void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager != null)
        {  
            inputManager = gameManager.InputManager;
            if (inputManager != null)
            {
                inputManager.OnMove.AddListener(SetPlayerMovement);
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
        
        rb = GetComponent<Rigidbody>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        ResetManager.Instance.RegisterObject(transform);
    }
    private void SetPlayerMovement(Vector2 direction)
    {
        moveInput = direction.normalized;
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveInput.x * speed, 0f, moveInput.y * speed);
    }

    void Update()
    {
        RotateToMouse();
    }

    private void RotateToMouse()
    {
        // Create a ray from the mouse position into the world
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Plane at y = 0

        if (groundPlane.Raycast(ray, out float hitDistance))
        {
            Vector3 hitPoint = ray.GetPoint(hitDistance); // Get the point where the mouse points
            Vector3 direction = hitPoint - transform.position;
            direction.y = 0; // Keep it 2D-like, only rotate on the XZ plane

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;

                // Optional: Also rotate the gun if it's separate from the player
                if (gunTransform != null)
                {
                    gunTransform.rotation = targetRotation;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.DecreaseLives();
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound("damage");
            }
            ResetPlayer();
        }
    }

    public void ResetPlayer()
    {
        ResetManager.Instance.ResetObject(transform);
        // Player keeps bullets, so no additional changes needed here
    }
}
