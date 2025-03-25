using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;

    public void incrementScore()
    {
        score++;
     
    }
}
