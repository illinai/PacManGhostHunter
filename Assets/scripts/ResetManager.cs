using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResetManager : MonoBehaviour
{
    public static ResetManager Instance {
        get;
        private set;
    }

    private Dictionary<Transform, (Vector3, Quaternion)> initialStates = new Dictionary<Transform, (Vector3, Quaternion)>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Multiple instances of ResetManager found!");
            Destroy(gameObject);
            return;
        }
    }

    public void RegisterObject(Transform obj)
    {
        if (!initialStates.ContainsKey(obj))
        {
            initialStates[obj] = (obj.position, obj.rotation);
        }
    }

    public void ResetObject(Transform obj)
    {
        if (initialStates.ContainsKey(obj))
        {
            (Vector3 pos, Quaternion rot) = initialStates[obj];
            obj.position = pos;
            obj.rotation = rot;
        }
        else
        {
            Debug.LogWarning($"ResetManager: Object {obj.name} was not registered.");
        }
    }

}
