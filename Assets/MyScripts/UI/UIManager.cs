using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Tooltip("Reference to Start Panel")]
    [SerializeField] private GameObject StartPanel;
    [Tooltip("Reference to Info Panel")]
    [SerializeField] private GameObject infoPanel;
    [Tooltip("Reference to Win Panel")]
    [SerializeField] private GameObject WinPanel;
    [Tooltip("Reference to Game Over Panel")]
    [SerializeField] private GameObject GameOverPanel;
    [Tooltip("Reference to Mine Field")]
    [SerializeField] private GameObject MineField;


    void OnEnable()
    {
        DisablePanels();
        MyEventSystem.OnGameWin += HandleGameWin;
        MyEventSystem.OnGameOver += HandleGameOver;
        MyEventSystem.OnStartGame += HandleStartGame;
    }
    void OnDisable()
    {
        MyEventSystem.OnGameWin -= HandleGameWin;
        MyEventSystem.OnGameOver -= HandleGameOver;
        MyEventSystem.OnStartGame -= HandleStartGame;
    }

    void Start()
    {
        StartPanel.SetActive(true);
    }
    /// <summary>
    /// Handles the game win event
    /// </summary>
    private void HandleGameWin()
    {
        PauseGame();
        WinPanel.SetActive(true);
    }
    /// <summary>
    /// Handles the game over event
    /// </summary>
    private void HandleGameOver()
    {
        PauseGame();
        GameOverPanel.SetActive(true);
    }
    /// <summary>
    /// Inveke the pause game event
    /// </summary>
    private void PauseGame()
    {
        MyEventSystem.RaisePauseGame();
    }
    /// <summary>
    /// Handles the start game event
    /// </summary>
    public void HandleStartGame()
    {
        StartPanel.SetActive(false);
        infoPanel.SetActive(true);
        MineField.SetActive(true);
    }
    /// <summary>
    /// Disable all UI panels except the StartPanel.
    /// </summary>
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
