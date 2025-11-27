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
}
