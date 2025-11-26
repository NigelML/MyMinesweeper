using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MineGround : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image statusImage;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private List<MineGround> sisterCellsList = new List<MineGround>();
    
    private Image cellImage;
    private Button button;

    private bool haveMine;
    public bool HaveMine { get { return haveMine; } set { haveMine = value; } }

    private bool cellChecked;
    public bool CellChecked => cellChecked;

    private bool isFlagged;
    public bool IsFlagged => isFlagged;

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
        //button.onClick.AddListener(ActiveCell);
    }

    // Método principal chamado pelo clique
    public void ActiveCell()
    {
        if (GameManager.Instance.PauseGame) return;
        // 1. Condição de Parada (Base Case)
        // Se já foi checada ou marcada com bandeira, não faz nada
        if (cellChecked || isFlagged) return;

        // 2. Marca como visitada e atualiza visual
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
                // Se tiver minas perto, mostra o número e PARA.                
                amountText.text = minesAround.ToString();
                amountText.gameObject.SetActive(true);
            }
            else
            {
                // 4. FLOOD FILL (Recursão)
                // Só entramos aqui se NÃO houver minas ao redor (buraco vazio)
                foreach (MineGround sister in sisterCellsList)
                {
                    sister.ActiveCell();
                }
            }
        }
    }

    // Método auxiliar APENAS para contar minas ao redor
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

    public void RevealMine()
    {
        if (haveMine)
        {
            statusImage.sprite = Resources.Load<Sprite>("mine");
            statusImage.gameObject.SetActive(true);
        }
    }

    public void ToggleFlag()
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
