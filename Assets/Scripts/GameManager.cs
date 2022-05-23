using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int levelNumber;

    private const int defaultValue = 0;
    private const string saveFileString = "_LevelNumber";
    private const int totalLevelNumber = 3;
    public int LevelNumber
    {
        get
        {
            return PlayerPrefs.GetInt(saveFileString, defaultValue);
        }
        private set { }
    }
    #region Singleton Pattern
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Input.backButtonLeavesApp = true;
    }
    #endregion
    private void Start()
    {
        if(PlayerPrefs.HasKey(saveFileString))
        {
            LoadLevel();
        }
        else
        {
            PlayerPrefs.SetInt(saveFileString, defaultValue);
            LoadLevel();
        }

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(GetSceneIndex());
    }
    private int GetSceneIndex()
    {
        return levelNumber % totalLevelNumber;
    }
    private void LevelSave()
    {
        PlayerPrefs.SetInt(saveFileString, levelNumber);
    }
    public void NextLevel()
    {
        levelNumber += 1;
        LevelSave();
        LoadLevel();
    }

    public void ReloadLevel()
    {
        LoadLevel();
    }

}
