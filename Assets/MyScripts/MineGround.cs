using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
public class MineGround : MonoBehaviour
{
    [SerializeField] private Image statusImage;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private List<MineGround> sisterCellsList = new List<MineGround>();

    private Image cellImage;
    private Button button;

    private bool haveMine;
    public bool HaveMine {  get { return haveMine; } set { haveMine = value; } }

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
        button.onClick.AddListener(ActiveCell);
    }

    // Método principal chamado pelo clique
    public void ActiveCell()
    {
        // 1. Condição de Parada (Base Case)
        // Se já foi checada ou marcada com bandeira, não faz nada
        if (cellChecked || isFlagged) return;

        // 2. Marca como visitada e atualiza visual
        cellChecked = true;
        cellImage.enabled = false;
        button.enabled = false;

        if (haveMine)
        {
            // Game Over logic here
            Debug.Log("BOOM!");
        }
        else
        {
            // 3. Primeiro contamos as minas ao redor (SEM RECURSÃO AQUI)
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

    // Método auxiliar APENAS para contar (Matemática pura, sem recursão)
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

}
