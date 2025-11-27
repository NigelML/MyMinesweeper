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

    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject Grid_Tilemap;

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
        MyEventSystem.OnPauseGame += OnPauseGame;
        MyEventSystem.OnStartGame += OnStartGame;
        uiManager.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        MyEventSystem.ClearAllEvents();
    }
    
    private void OnStartGame()
    {
        Grid_Tilemap.SetActive(true);
    }
    private void OnPauseGame()
    {
        pauseGame = !pauseGame;
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
