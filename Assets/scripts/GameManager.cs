using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    public InputManager InputManager { get; private set; }
    public event Action<int> OnScoreChanged;
    public event Action<int> OnLivesChanged;
    public event Action<int> OnBulletsChanged;
    [SerializeField] private int score = 0;
    [SerializeField] private int maxLives = 3;
    [SerializeField] public int bullets = 10;
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

        OnScoreChanged?.Invoke(score);
        OnLivesChanged?.Invoke(maxLives);
        OnBulletsChanged?.Invoke(bullets);
    }

    public void IncrementScore(int amount = 10)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
        Debug.Log($"Coins {activeCoins.Count}/{totalCoinCount} remaining");

        if (activeCoins.Count == 0)
        {
            SceneHandler.Instance.LoadNextScene();
        }
          
        // quick test cases for advancing to other levels
        // if (score == 100)
        // {
        //     SceneHandler.Instance.LoadNextScene();
        // }
        // if (score == 250)
        // {
        //     SceneHandler.Instance.LoadNextScene();
        // }

    }
    public bool CanShootBullet()
    {
        return bullets > 0;
    }
    public void IncrementBullets(int amount = 10)
    {
        bullets += amount;
        OnBulletsChanged?.Invoke(bullets);
        Debug.Log("Bullets: " + bullets);

        IncrementScore(5);
    }
    public void DecreaseBullets()
    {
        if (bullets > 0)
        {
            bullets--;
            OnBulletsChanged?.Invoke(bullets);
        }
    }

    public void DecreaseLives()
    {
        maxLives--;
        OnLivesChanged?.Invoke(maxLives);

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

        OnScoreChanged?.Invoke(score);
        OnLivesChanged?.Invoke(maxLives);
        OnBulletsChanged?.Invoke(bullets);
    }

    public void RegisterCoin(Coins coin)
    {
        if (!activeCoins.Contains(coin))
        {
            activeCoins.Add(coin);
            totalCoinCount = CountCoins();
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
