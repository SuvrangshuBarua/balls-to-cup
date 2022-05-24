using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimateUI : MonoBehaviour
{
    public float duration = 1;
    private RectTransform rect;
    private Tweener rectTweener;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        Vector2 result = new Vector2(1.25f, 1.25f);
        rectTweener = rect.DOScale(result, duration).OnComplete(()=> {
            rect.DOScale(Vector2.one, duration);
        }).SetLoops(-1, LoopType.Yoyo).SetSpeedBased();
       
    }
    private void OnDisable()
    {
        rectTweener.Kill();
    }
}
