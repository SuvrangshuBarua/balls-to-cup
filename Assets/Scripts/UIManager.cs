using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class UIManager : MonoBehaviour
{
    public Text TargetScoreText;
    public Text CurrentScoreText;
    public IntReference targetScore;
    public IntReference currentScore;

    private void Start()
    {
        TargetScoreText.text = targetScore.Value.ToString();
        CurrentScoreText.text = currentScore.Value.ToString();
    }
    public void UpdateCurrentScoreUI()
    {
        CurrentScoreText.text = currentScore.Value.ToString();
    }
}
