using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool pauseGame;
    public bool PauseGame => pauseGame;
    private float dificultyLevel;
    public float DificultyLevel => dificultyLevel;

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
        Debug.Log("You Win!");
    }
    private void OnGameOver()
    {
        OnPauseGame();
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
}
