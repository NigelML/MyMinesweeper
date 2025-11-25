using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool pauseGame;
    public bool PauseGame => pauseGame;
    private float dificultyLevel;
    public float DificultyLevel => dificultyLevel;

    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject MineField;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;            
        }
        else
        {
            Destroy(gameObject);
        }
        WinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
    void OnEnable()
    {
        MyEventSystem.OnGameWin += OnGameWin;
        MyEventSystem.OnGameOver += OnGameOver;    
        pauseGame = false;    
    }

    void OnDisable()
    {
        MyEventSystem.OnGameWin -= OnGameWin;
        MyEventSystem.OnGameOver -= OnGameOver;
    }

    private void OnGameWin()
    {
        OnPauseGame();
        WinPanel.SetActive(true);
        Debug.Log("You Win!");
    }
    private void OnGameOver()
    {
        OnPauseGame();
        GameOverPanel.SetActive(true);
        Debug.Log("Game Over!");
    }

    private void OnPauseGame()
    {
        pauseGame = !pauseGame;
    }

    public void SetDificultyLevel(float level)
    {
        dificultyLevel = level;
    }
    public void StartGame()
    {
        StartPanel.SetActive(false);
        MineField.SetActive(true);
        pauseGame = false;
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
