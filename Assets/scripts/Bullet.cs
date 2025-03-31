using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;

    private Rigidbody rb;
    
    void Start()
    {
        Destroy(gameObject, lifetime);

        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        rb.angularVelocity = Vector3.zero;
    }

    
    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        rb.linearVelocity = transform.forward * speed;

    }
}
