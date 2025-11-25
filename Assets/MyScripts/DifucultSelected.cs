using UnityEngine;
using UnityEngine.UI;

public class DifucultSelected : MonoBehaviour
{
    [Range(0.1f, 0.25f)]
    [SerializeField] private float dificultyLevel = 0.15f;


    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDificulty);
    }
    public void SetDificulty()
    {
        GameManager.Instance.SetDificultyLevel(dificultyLevel);
        GameManager.Instance.StartGame();
    }

}
