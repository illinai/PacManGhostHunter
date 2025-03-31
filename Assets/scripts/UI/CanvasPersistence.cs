using UnityEngine;

public class CanvasPersistence : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
