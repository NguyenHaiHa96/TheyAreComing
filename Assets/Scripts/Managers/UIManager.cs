using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Manager<UIManager>, ISubcribers
{
    private GameManager gameManager;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI currentLevelText;

    public TextMeshProUGUI CurrentLevelText { get => currentLevelText; set => currentLevelText = value; }

    public override void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public override void InitializeVariables()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        InitializeSingleton();
        InitializeVariables();
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    public void ShowStartPanel(bool isShow)
    {
        startPanel.SetActive(isShow);
    }

    public void ShowGameplayPanel(bool isShow)
    {
        gameplayPanel.SetActive(isShow);
    }

    public void ShowLevelCompletePanel(bool isShow)
    {
        levelCompletePanel.SetActive(isShow);
    }
    public void ShowGameOverPanel(bool isShow)
    {
        gameOverPanel.SetActive(isShow);
    }

    public void NextLevel(bool loadNextLevel)
    {
        if (loadNextLevel)
        {
            ShowGameplayPanel(true);
            ShowLevelCompletePanel(false);
        }
    }

    public void SubscribeEvent()
    {
        gameManager.OnGameStarted += GameStarted;
        gameManager.OnLevelCompleted += LevelCompleted;
        gameManager.OnGameOver += GameOver;
    }

    public void UnsubscribeEvent()
    {
        gameManager.OnGameStarted -= GameStarted;
        gameManager.OnLevelCompleted -= LevelCompleted;
        gameManager.OnGameOver -= GameOver;
    }

    private void GameStarted(int currentLevel)
    {
        ShowStartPanel(false);
        ShowGameplayPanel(true);
        currentLevelText.text = "LEVEL " + currentLevel.ToString();

        StartCoroutine(DisableUI());
    }

    IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(3f);
        currentLevelText.text = "";
    }

    private void LevelCompleted()
    {
        StartCoroutine(DelayShowingUI());
    }

    IEnumerator DelayShowingUI()
    {
        yield return new WaitForSeconds(1.5f);
        ShowGameplayPanel(false);
        ShowLevelCompletePanel(true);
    }

    private void GameOver()
    {
        ShowGameplayPanel(false);
        ShowGameOverPanel(true);
    }
}
