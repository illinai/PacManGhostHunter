using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    public InputManager InputManager { get; private set; }
    [SerializeField] private int maxLives = 3;
    [SerializeField] private LivesUI livesUI;
    [SerializeField] private int score = 0;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] public int bullets = 10;
    [SerializeField] private BulletUI bulletUI;
    private List<Coins> activeCoins = new List<Coins>();
    private int totalCoinCount;
    protected override void Awake()
    {
        base.Awake();
        InputManager = GetComponent<InputManager>();
        if (InputManager == null)
        {
            Debug.LogError("InputManager not found.");
        }
    }
    private void Start()
    {
        totalCoinCount = CountCoins();
        Debug.Log("Total coins in the scene: " + totalCoinCount);
    }

    public void IncrementScore()
    {
        score += 10;
        scoreUI.UpdateScore(score);
        Debug.Log($"Coins {activeCoins.Count}/{totalCoinCount} remaining");
        if (score == 50) SceneHandler.Instance.LoadNextScene(); // test scene handling
        //if (activeCoins.Count == 0) SceneHandler.Instance.LoadNextScene();
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
            ResetGame();
            SceneHandler.Instance.LoadGameOverScene();
        }
    }
    public void ResetGame()
    {
        score = 0;
        maxLives = 3;
        bullets = 10;
        scoreUI.UpdateScore(score);
        livesUI.UpdateLives(maxLives);
        bulletUI.UpdateBullet(bullets);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
        SceneHandler.Instance.LoadMenuScene();
        AudioManager.Instance.RestartMusicAfterDelay(2.5f);
    }
    public void RegisterCoin(Coins coin)
    {
        if (!activeCoins.Contains(coin))
        {
            activeCoins.Add(coin);
        }
    }
    public void UnregisterCoin(Coins coin)
    {
        if (activeCoins.Contains(coin))
        {
            activeCoins.Remove(coin);
        }
    }

    private int CountCoins()
    {
        return activeCoins.Count;
    }
}
