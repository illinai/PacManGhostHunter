using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] public int bullets = 20;

    public void incrementScore()
    {
        score += 10;
     
    }

    public void incrementBullet()
    {
        bullets += 10;
        score += 5;

    }
}
