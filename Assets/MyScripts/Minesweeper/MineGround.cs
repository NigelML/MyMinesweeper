using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MineGround : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Image to show mine or flag status")]
    [SerializeField] private Image statusImage;
    [Tooltip("Text to show amount of mines around")]
    [SerializeField] private TextMeshProUGUI amountText;
    [Tooltip("List of sister cells around this cell. It is automatically populated, the preview is for testing purposes only.")]
    [SerializeField] private List<MineGround> sisterCellsList = new List<MineGround>();

    private Image cellImage;
    private Button button;

    private bool haveMine;
    public bool HaveMine { get { return haveMine; } set { haveMine = value; } }

    private bool cellChecked;
    public bool CellChecked => cellChecked;

    private bool isFlagged;
    public bool IsFlagged => isFlagged;

    /// <summary>
    /// Configure the sister cells around this cell
    /// </summary>
    /// <param name="cells"></param>
    public void ConfigureSisterCells(List<MineGround> cells)
    {
        sisterCellsList = cells;
    }
    void OnEnable()
    {
        sisterCellsList.Clear();
    }

    void Start()
    {
        cellImage = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    /// <summary>
    /// Main method called by click
    /// </summary>
    private void ActiveCell()
    {
        if (GameManager.Instance.PauseGame) return;
        //Stop Condition (Base Case)
        //If it has already been checked or flagged, do nothing.
        if (cellChecked || isFlagged) return;

        // Mark as visited and update visuals.
        cellChecked = true;
        cellImage.enabled = false;
        button.enabled = false;

        if (haveMine)
        {
            MyEventSystem.RaiseTryGameOver();
            Debug.Log("BOOM!");
            return;
        }
        else
        {
            MyEventSystem.RaiseCellChecked();
            int minesAround = CountMinesAround();

            if (minesAround > 0)
            {
                // If there are mines nearby, it shows the number and STOPS.                
                amountText.text = minesAround.ToString();
                amountText.gameObject.SetActive(true);
            }
            else
            {
                // 4. FLOOD FILL (Recursion)
                // We're only here if there are NO mines around (empty hole).
                foreach (MineGround sister in sisterCellsList)
                {
                    sister.ActiveCell();
                }
            }
        }
    }

    /// <summary>
    /// Count the amount of mines in the vicinity
    /// </summary>
    /// <returns></returns>
    private int CountMinesAround()
    {
        int count = 0;
        foreach (MineGround sister in sisterCellsList)
        {
            if (sister.haveMine)
            {
                count++;
            }
        }
        return count;
    }
    /// <summary>
    /// Reveals the mine graphic on this cell
    /// </summary>
    public void RevealMine()
    {
        if (haveMine)
        {
            statusImage.sprite = Resources.Load<Sprite>("mine");
            statusImage.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// Toggles a flag on this cell
    /// </summary>
    private void ToggleFlag()
    {
        if (GameManager.Instance.PauseGame) return;
        if (cellChecked) return;

        int f;
        isFlagged = !isFlagged;
        if (isFlagged)
        {
            statusImage.sprite = Resources.Load<Sprite>("Flag_A");
            statusImage.gameObject.SetActive(true);
            f = -1;
        }
        else
        {
            statusImage.gameObject.SetActive(false);
            f = 1;
        }
        GameManager.Instance.FlagsAvailable += f;
        MyEventSystem.RaiseSetFlags();
    }
    /// <summary>
    /// Handle pointer click events
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicou em: " + gameObject.name);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (IsFlagged) return;

            ActiveCell();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            ToggleFlag();
        }
    }

}
