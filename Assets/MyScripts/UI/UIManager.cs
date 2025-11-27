using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject MineField;


    void OnEnable()
    {
        DisablePanels();
        MyEventSystem.OnGameWin += OnGameWin;
        MyEventSystem.OnGameOver += OnGameOver;
        MyEventSystem.OnStartGame += OnStartGame;
    }
    void OnDisable()
    {
        MyEventSystem.OnGameWin -= OnGameWin;
        MyEventSystem.OnGameOver -= OnGameOver;
        MyEventSystem.OnStartGame -= OnStartGame;
    }

    void Start()
    {
        StartPanel.SetActive(true);
    }
    private void OnGameWin()
    {
        OnPauseGame();
        WinPanel.SetActive(true);
    }
    private void OnGameOver()
    {
        OnPauseGame();
        GameOverPanel.SetActive(true);
    }
    private void OnPauseGame()
    {
        MyEventSystem.RaisePauseGame();
    }
    public void OnStartGame()
    {
        StartPanel.SetActive(false);
        infoPanel.SetActive(true);
        MineField.SetActive(true);
    }
    private void DisablePanels()
    {
        if (infoPanel.activeSelf)
            infoPanel.SetActive(false);
        if (WinPanel.activeSelf)
            WinPanel.SetActive(false);
        if (GameOverPanel.activeSelf)
            GameOverPanel.SetActive(false);
    }
}
