using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool pauseGame;
    public bool PauseGame => pauseGame;
    private float dificultyLevel;
    public float DificultyLevel => dificultyLevel;
    private int flagsAvailable;
    public int FlagsAvailable { get { return flagsAvailable; } set { flagsAvailable = value; } }    

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
        pauseGame = false;
    }

    void OnDisable()
    {
        MyEventSystem.ClearAllEvents();
    }

    public void SetDificultyLevel(float level)
    {
        dificultyLevel = level;
    }

    public void RestartGame()
    {
        MyEventSystem.ClearAllEvents();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
