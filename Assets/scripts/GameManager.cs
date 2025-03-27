using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private int score = 0;
    [SerializeField] public int bullets = 20;
    [SerializeField] private LivesCounterUI livesCounter;
    [SerializeField] private ScoreUI scoreCounter;

    public void incrementScore()
    {
        score += 10;
     
    }

    public void KillBall()
    {
        maxLives--;
        livesCounter.UpdateLives(maxLives);

        if (maxLives == 0)
        {
            AudioManager.Instance.StopMusic(); // Stop the ambient music
            AudioManager.Instance.PlaySound("game-over"); // Play game-over sound
            SceneHandler.Instance.LoadGameOverScene();
            StartCoroutine(Wait());
        }
        ball.ResetBall();
    }

    public void incrementBullet()
    {
        bullets += 10;
        score += 5;

    }
}
