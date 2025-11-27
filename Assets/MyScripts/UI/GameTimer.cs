using UnityEngine;
using TMPro; 

public class GameTimer : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timeText;

    private float _currentTime;
    private bool _isRunning;

    void OnEnable()
    {
        MyEventSystem.OnStartGame += StartTimer;  
        MyEventSystem.OnPauseGame += OnPauseGame;            
    }
    void OnDisable()
    {
        MyEventSystem.OnStartGame -= StartTimer;
        MyEventSystem.OnPauseGame -= OnPauseGame;
    }
    private void Update()
    {
        if (_isRunning)
        {
            // Adiciona o tempo que passou desde o último frame
            _currentTime += Time.deltaTime;
            
            // Atualiza o texto na tela
            UpdateTimerDisplay(_currentTime);
        }
    }

    public void StartTimer()
    {
        this.enabled = true;
        _isRunning = true;
        _currentTime = 0; // Reseta se quiser começar do zero
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    // Formata o tempo para mostrar Minutos:Segundos (ex: 02:45)
    private void UpdateTimerDisplay(float timeToDisplay)
    {
        // Opcional: Soma +1 para começar mostrando "01" se preferir
        // timeToDisplay += 1; 

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Formatação de string:
        // {0:00} significa "primeiro numero com pelo menos 2 digitos"
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnPauseGame()
    {
        _isRunning = !_isRunning;
    }

}