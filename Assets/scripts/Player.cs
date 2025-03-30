using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] float speed;
    [SerializeField] private Rigidbody rb;

    //
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform gunTransform;


    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();

        //
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        ResetManager.Instance.RegisterObject(transform);

    }

    //
    void Update()
    {
        RotateToMouse();
    }



    private void MovePlayer(Vector2 direction)
    {

        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        rb.AddForce(speed * moveDirection);
    }




    //
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
