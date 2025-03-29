using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private LivesUI livesUI;
    [SerializeField] private int score = 0;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] public int bullets = 10;
    [SerializeField] private BulletUI bulletUI;

    public void IncrementScore()
    {
        score += 10;
        scoreUI.UpdateScore(score);
        Debug.Log("Score: " + score);
    }

    public void IncrementBullet()
    {
        bullets += 10;
        bulletUI.UpdateBullet(bullets);
        Debug.Log("Bullets: " + bullets);
        score += 5;
        scoreUI.UpdateScore(score);
        Debug.Log("Score: " + score);
    }

    public void DecreaseLives()
    {
        maxLives--;
        livesUI.UpdateLives(maxLives);

        if (maxLives <= 0) {
            AudioManager.Instance.StopMusic();
            SceneHandler.Instance.LoadGameOverScene();
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
        //SceneHandler.Instance.LoadMenuScene();
        AudioManager.Instance.RestartMusicAfterDelay(2.5f);
    }
}
