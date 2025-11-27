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

    [Tooltip("Reference to UI Manager")]
    [SerializeField] private UIManager uiManager;
    [Tooltip("Reference to Grid Tilemap")]
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
        MyEventSystem.OnPauseGame += HandlePauseGame;
        MyEventSystem.OnStartGame += HandleStartGame;
        uiManager.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        MyEventSystem.ClearAllEvents();
    }
    
    private void HandleStartGame()
    {
        Grid_Tilemap.SetActive(true);
    }
    private void HandlePauseGame()
    {
        pauseGame = !pauseGame;
    }

    /// <summary>
    /// Set the dificulty level of the game
    /// </summary>
    /// <param name="level"></param>
    public void SetDificultyLevel(float level)
    {
        dificultyLevel = level;
    }

    /// <summary>
    /// Restart game by reloading the current scene
    /// </summary>
    public void RestartGame()
    {
        MyEventSystem.ClearAllEvents();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
