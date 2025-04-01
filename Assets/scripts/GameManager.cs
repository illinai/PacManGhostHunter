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
    private string sceneName;
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
        totalCoinCount = activeCoins.Count;
        Debug.Log("Total coins in the scene: " + totalCoinCount);

        OnScoreChanged?.Invoke(score);
        OnLivesChanged?.Invoke(maxLives);
        OnBulletsChanged?.Invoke(bullets);
    }

    public void IncrementScore(int amount = 10)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
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
            ResetGame();
            SceneHandler.Instance.LoadGameOverScene();
        }
    }

    public void ResetGame()
    {
        score = 0;
        maxLives = 3;
        bullets = 10;

        activeCoins.Clear();
        totalCoinCount = 0;

        OnScoreChanged?.Invoke(score);
        OnLivesChanged?.Invoke(maxLives);
        OnBulletsChanged?.Invoke(bullets);
    }

    public void RegisterCoin(Coins coin)
    {
        if (!activeCoins.Contains(coin))
        {
            activeCoins.Add(coin);
            totalCoinCount = activeCoins.Count;
        }
    }

    public void UnregisterCoin(Coins coin)
    {
        if (activeCoins.Contains(coin))
        {
            activeCoins.Remove(coin);
            Debug.Log($"Coins remaining: {activeCoins.Count}/{totalCoinCount}");
            sceneName = SceneHandler.Instance.GetCurrentSceneName();
            if (activeCoins.Count == 0 && sceneName != "MainMenu" && sceneName != "GameOver" && sceneName != "_Preload")
            {
                SceneHandler.Instance.LoadNextScene();
            }
        }
    }
}
