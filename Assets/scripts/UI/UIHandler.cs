using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;

public class UIHandler : MonoBehaviour
{
    public enum UIType { Score, Lives, Bullets }
    [SerializeField] private UIType uIType;
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform textContainer;
    [SerializeField] private float duration;
    [SerializeField] private Ease animationCurve;
    private float containerInitPosition;
    private float moveAmount;
    //private int initialValue;
    private void Start()
    {
        switch (uIType)
        {
            case UIType.Score:
                GameManager.Instance.OnScoreChanged += UpdateValue;
                break;
            case UIType.Lives:
                GameManager.Instance.OnLivesChanged += UpdateValue;
                break;
            case UIType.Bullets:
                GameManager.Instance.OnBulletsChanged += UpdateValue;
                break;
        }
        Canvas.ForceUpdateCanvases();
        containerInitPosition = textContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }
    public void UpdateValue(int value)
    {
        toUpdate.SetText($"{value}");
        textContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetTextContainer(value));
    }
    private IEnumerator ResetTextContainer(int value)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{value}");
        Vector3 localPosition = textContainer.localPosition;
        textContainer.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;

        switch (uIType)
        {
            case UIType.Score:
                GameManager.Instance.OnScoreChanged -= UpdateValue;
                break;
            case UIType.Lives:
                GameManager.Instance.OnLivesChanged -= UpdateValue;
                break;
            case UIType.Bullets:
                GameManager.Instance.OnBulletsChanged -= UpdateValue;
                break;
        }    
    }
}
