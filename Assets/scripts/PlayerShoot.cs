using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;  // Drag your bullet prefab here in the Inspector
    public Transform shootingPoint;
    public GameManager gm;


    void Update()
    {
        
       
        if (Input.GetKeyDown(KeyCode.Space)) // Check for spacebar press
        {
            if (gm.bullets != 0)
            {
                Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation); // Spawn a bullet
                gm.bullets--;
            }
                                                                                       
        }
    }
}
