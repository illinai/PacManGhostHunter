using UnityEngine;
using System.Collections;
using DG.Tweening;
using TMPro;

public class LivesUI : MonoBehaviour
{
    private static LivesUI Instance;
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform livesTextContainer;
    [SerializeField] private float duration;
    [SerializeField] private Ease animationCurve;
    private float containerInitPosition;
    private float moveAmount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("3");
        toUpdate.SetText("3");
        containerInitPosition = livesTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }
    public void UpdateLives(int lives)
    {
        toUpdate.SetText($"{lives}");
        livesTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetLivesContainer(lives));
    }
    private IEnumerator ResetLivesContainer(int lives)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{lives}");
        Vector3 localPosition = livesTextContainer.localPosition;
        livesTextContainer.localPosition = new Vector3(localPosition.x,
        containerInitPosition, localPosition.z);
    }
}
