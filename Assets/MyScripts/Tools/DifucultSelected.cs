using UnityEngine;
using UnityEngine.UI;

public class DifucultSelected : MonoBehaviour
{
    [Tooltip("Dificulty level to set when this button is clicked")]
    [Range(0.1f, 0.25f)]
    [SerializeField] private float dificultyLevel = 0.15f;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDificulty);
    }
    /// <summary>
    /// Sets the difficulty level in the GameManager and raises the start game event
    /// </summary>
    public void SetDificulty()
    {
        GameManager.Instance.SetDificultyLevel(dificultyLevel);
        MyEventSystem.RaiseStartGame();
    }

}
