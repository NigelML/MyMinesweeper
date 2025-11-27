using UnityEngine;
using TMPro;

public class ShowFlags : MonoBehaviour
{
[SerializeField] TextMeshProUGUI FlagsText;

    void OnEnable()
    {
        MyEventSystem.OnSetFlags += UpdateFlagsDisplay;              
    }
    void OnDisable()
    {
        MyEventSystem.OnSetFlags -= UpdateFlagsDisplay;
    }
    void Start()
    {
        UpdateFlagsDisplay();
    }

    public void UpdateFlagsDisplay()
    {
        int flags = GameManager.Instance.FlagsAvailable;       
        FlagsText.text = $"{flags:00}";
    }
}
