using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class UIManager : MonoBehaviour
{
    #region Public Variables
    [Header("UI Dependencies")]
    public Text TargetScoreText;
    public Text CurrentScoreText;
    public RectTransform winPanel;
    public RectTransform losePanel;
    [Header("Variables")]
    public IntReference targetScore;
    public IntReference currentScore;
    #endregion

    private void Start()
    {
        TargetScoreText.text = targetScore.Value.ToString();
        CurrentScoreText.text = currentScore.Value.ToString();
    }
    public void UpdateCurrentScoreUI()
    {
        CurrentScoreText.text = currentScore.Value.ToString();
    }
    public void LevelWinCallback()
    {
        "-----------< Level Won >-----------".Debug("90EE90");
    }
    public void LevelLoseCallback()
    {
        "-----------< Level Lost >-----------".Debug("#FF5733");
    }
}
