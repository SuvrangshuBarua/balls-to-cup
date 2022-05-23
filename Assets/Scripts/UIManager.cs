using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Public Variables
    [Header("UI Dependencies")]
    public Text levelNo;
    public Text TargetScoreText;
    public Text CurrentScoreText;
    public RectTransform winPanel;
    public Button nextButton;
    public RectTransform losePanel;
    public Button reloadButton;
    [Header("Tutorial UI")]
    public bool showTutorial;
    public GameObject tutorialGroup;
    public Image tapIcon_Image;
    public float arrowTraverseDuration;
    public float tapCenterX, tapLeftX, tapRightX;
    public Text dragToStart_Text;

    [Header("Variables")]
    public IntReference targetScore;
    public IntReference currentScore;
    #endregion
    #region Private Variables
    private bool isCalledOnce;
    #endregion
    private void Start()
    {
        if(showTutorial)
        {
            ShowTutorial();
        }
        DragHandler.instance.onDragStart += DragStart;
        DOVirtual.DelayedCall(0.05f, () =>
        {
            ConfigureUI();
        });
    }

    

    private void OnDestroy()
    {
        nextButton.onClick.RemoveAllListeners();
        reloadButton.onClick.RemoveAllListeners();
        DragHandler.instance.onDragStart -= DragStart;
    }
    private void ConfigureUI()
    {
        TargetScoreText.text = targetScore.Value.ToString();
        CurrentScoreText.text = currentScore.Value.ToString();
        levelNo.text = GameManager.Instance.GetLevel().ToString();
        nextButton.onClick.AddListener(() => GameManager.Instance.NextLevel());
        reloadButton.onClick.AddListener(() => GameManager.Instance.ReloadLevel()); ;
        isCalledOnce = false;
    }
    private void DragStart(Vector2 dragAmount)
    {
        tutorialGroup.SetActive(false);
        DragHandler.instance.onDragStart -= DragStart;
    }
    public void UpdateCurrentScoreUI()
    {
        CurrentScoreText.text = currentScore.Value.ToString();
    }
    public void LevelWinCallback()
    {
        if(!isCalledOnce)
        {
            isCalledOnce = false;
            winPanel.gameObject.SetActive(true);
        }
    }
    public void LevelLoseCallback()
    {
        if (!isCalledOnce)
        {
            isCalledOnce = false;
            losePanel.gameObject.SetActive(true);
        }
    }


    void ShowTutorial()
    {
        tutorialGroup.SetActive(true);

        tapIcon_Image.rectTransform.anchoredPosition = new Vector2(tapCenterX, tapIcon_Image.rectTransform.anchoredPosition.y);
        tapIcon_Image.rectTransform.DOLocalMoveX(tapLeftX, arrowTraverseDuration / 2f).OnComplete(() =>
        {
            DOVirtual.Float(1f, 0f, arrowTraverseDuration, (float value) => dragToStart_Text.alpha = value).SetLoops(-1, LoopType.Yoyo).SetSpeedBased();
            tapIcon_Image.rectTransform.DOLocalMoveX(tapRightX, arrowTraverseDuration).SetLoops(-1, LoopType.Yoyo);
        });

    }
}
