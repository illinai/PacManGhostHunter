using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
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

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
        //SceneHandler.Instance.LoadMenuScene();
        AudioManager.Instance.RestartMusicAfterDelay(2.5f);
    }
}
