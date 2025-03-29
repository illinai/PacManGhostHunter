using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;

public class BulletUI : MonoBehaviour
{
    private static BulletUI Instance;
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform bulletTextContainer;
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
        current.SetText("10");
        toUpdate.SetText("10");
        containerInitPosition = bulletTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }
    public void UpdateBullet(int bullets)
    {
        toUpdate.SetText($"{bullets}");
        bulletTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetBulletContainer(bullets));
    }
    private IEnumerator ResetBulletContainer(int bullets)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{bullets}");
        Vector3 localPosition = bulletTextContainer.localPosition;
        bulletTextContainer.localPosition = new Vector3(localPosition.x,
            containerInitPosition, localPosition.z);
    }
}
