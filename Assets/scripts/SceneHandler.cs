using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
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
    }

    public void LoadNextScene()
    {
        if(nextLevelIndex >= levels.Count)
        {
            LoadMenuScene();
        }
        else
        {
            transitionCanvas.DOLocalMoveY(initYPosition - transitionCanvas.rect.height, animationDuration).SetEase(menuAnimationType);
            StartCoroutine(LoadSceneAfterTransition(levels[nextLevelIndex]));
            nextLevelIndex++;
        }
    }

    public void LoadMenuScene()
    {
        transitionCanvas.DOLocalMoveY(initYPosition - transitionCanvas.rect.height, animationDuration).SetEase(menuAnimationType);
        StartCoroutine(LoadSceneAfterTransition(menuScene));
        nextLevelIndex = 0;
    }

    public void LoadGameOverScene()
    {
        transitionCanvas.DOLocalMoveY(initYPosition - transitionCanvas.rect.height, animationDuration).SetEase(menuAnimationType);
        StartCoroutine(LoadSceneAfterTransition(gameOverScene));
        nextLevelIndex = 0;
    }

    private IEnumerator LoadSceneAfterTransition(string scene)
    {
        yield return new WaitForSeconds(animationDuration);
        SceneManager.LoadScene(scene);
    }

}
