using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Text component to display the time")]
    [SerializeField] private TextMeshProUGUI timeText;

    private float _currentTime;
    private bool _isRunning;

    void OnEnable()
    {
        MyEventSystem.OnStartGame += StartTimer;
        MyEventSystem.OnPauseGame += StopTimer;
    }
    void OnDisable()
    {
        MyEventSystem.OnStartGame -= StartTimer;
        MyEventSystem.OnPauseGame -= StopTimer;
    }
    private void Update()
    {
        if (_isRunning)
        {
            // Adds the time that has elapsed since the last frame.
            _currentTime += Time.deltaTime;

            // Updates the text on the screen.
            UpdateTimerDisplay(_currentTime);
        }
    }
    /// <summary>
    /// Starts the timer
    /// </summary>
    private void StartTimer()
    {
        this.enabled = true;
        _isRunning = true;
        _currentTime = 0; // Reset timer
    }
    /// <summary>
    /// Alternates the timer running state
    /// </summary>
    private void PauseGame()
    {
        _isRunning = !_isRunning;
    }
    /// <summary>
    /// Stops the timer
    /// </summary>
    private void StopTimer()
    {
        _isRunning = false;
    }

    // Format the time to display Minutes:Seconds (e.g., 02:45)
    private void UpdateTimerDisplay(float timeToDisplay)
    {
        // Optional: Add +1 to start by displaying "01" if preferred
        // timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // String formatting:
        // {0:00} means "first number with at least 2 digits"
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}