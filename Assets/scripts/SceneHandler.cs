using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : SingletonMonoBehavior<SceneHandler>
{
    [Header("Scene Data")]
    [SerializeField] private List<string> levels;
    [SerializeField] private string menuScene;
    [SerializeField] private string gameOverScene;
    
    [Header("Transition Animation Data")]
    [SerializeField] private Ease menuAnimationType;
    [SerializeField] private float animationDuration;
    [SerializeField] private RectTransform transitionCanvas;
    private int nextLevelIndex;
    private float initYPosition;
    protected override void Awake()
    {
        base.Awake();
        initYPosition = transitionCanvas.transform.localPosition.y;
        SceneManager.LoadScene(menuScene);
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode _)
    {
        transitionCanvas.DOLocalMoveY(initYPosition, animationDuration).SetEase(menuAnimationType);
        Debug.Log("Loaded scene: " + scene.name);

        if (scene.name != menuScene && scene.name != gameOverScene && scene.name != "_Preload")
        {
            if (GameManager.Instance == null)
            {
                Debug.LogWarning("GameManager is missing.");
                GameObject gm = GameObject.Find("GameManager");
                Debug.Log(gm != null ? "GameManager found in hierarchy." : "GameManager not found.");
            }
        }
    }

    public void LoadNextScene()
    {
        if(nextLevelIndex >= levels.Count)
        {
            LoadMenuScene();
        }
        else
        {
            LoadTransitionAnimation(initYPosition - transitionCanvas.rect.height);
            StartCoroutine(LoadSceneAfterTransition(levels[nextLevelIndex]));
            nextLevelIndex++;
        }
    }

    public void LoadMenuScene()
    {
        LoadTransitionAnimation(initYPosition - transitionCanvas.rect.height);
        StartCoroutine(LoadSceneAfterTransition(menuScene));
        nextLevelIndex = 0;
    }

    public void LoadGameOverScene()
    {
        LoadTransitionAnimation(initYPosition - transitionCanvas.rect.height);
        StartCoroutine(LoadSceneAfterTransition(gameOverScene));
        nextLevelIndex = 0;
    }

    private IEnumerator LoadSceneAfterTransition(string scene)
    {
        yield return new WaitForSeconds(animationDuration);
        SceneManager.LoadScene(scene);
    }

    private void LoadTransitionAnimation(float targetYPosition)
    {
        transitionCanvas.DOLocalMoveY(targetYPosition, animationDuration).SetEase(menuAnimationType);
    }
}
