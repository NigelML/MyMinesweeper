using System.Collections.Generic;
using UnityEngine;

public class MineGroundCollection : MonoBehaviour
{
    [SerializeField] private GameObject gridParent;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    [Header("Dificuldade (0.1 = 10%, 0.2 = 20%)")]
    [Range(0.1f, 0.25f)] // Limita o slider no Inspector para não quebrar o jogo
    private float difficultyRatio = 0.15f;
    private int mineCount;
    private int safeMineCount;

    private MineGround[,] matrixMineGrounds;
    void OnEnable()
    {
        MyEventSystem.OnGameOver += OnGameOver;
        MyEventSystem.OnCellChecked += SafeMineChecked;
    }
    void OnDisable()
    {
        MyEventSystem.OnGameOver -= OnGameOver;
        MyEventSystem.OnCellChecked -= SafeMineChecked;
    }
    void Start()
    {
        difficultyRatio = GameManager.Instance.DificultyLevel;
        SetGrid();
        CalculateMineCount();
        SetSisterCells();
        InsertMines();
    }

    private void SetGrid()
    {
        matrixMineGrounds = new MineGround[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // 1. Instancia já definindo o gridParent como pai
                // Isso coloca o objeto automaticamente na lista do Layout Group
                GameObject cellObj = Instantiate(cellPrefab, gridParent.transform);

                // 2. Organização (Opcional mas recomendado para Debug)
                cellObj.name = $"Cell_{i}_{j}";

                // 3. Pega o componente do script para guardar na lógica
                MineGround cellScript = cellObj.GetComponent<MineGround>();

                // 4. Salva na sua matriz lógica [,]
                matrixMineGrounds[i, j] = cellScript;

            }
        }
    }

    private void SetSisterCells()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                MineGround currentCell = matrixMineGrounds[i, j];
                List<MineGround> sisterCellsList = new List<MineGround>();

                for (int r = i - 1; r <= i + 1; r++)
                {
                    for (int c = j - 1; c <= j + 1; c++)
                    {
                        if (r == i && c == j) continue; // Pular a própria célula

                        if (r >= 0 && r < rows && c >= 0 && c < columns)
                        {
                            sisterCellsList.Add(matrixMineGrounds[r, c]);
                        }
                    }
                }
                currentCell.ConfigureSisterCells(sisterCellsList);
            }
        }
    }

    List<int> mineIndices;
    List<MineGround> mineCells = new List<MineGround>();
    private void InsertMines()
    {
        int totalCells = rows * columns;
        mineIndices = RandomIndicesUtils.GetUniqueRandomIndices(mineCount, totalCells);

        foreach (int index in mineIndices)
        {
            int r = index / columns;
            int c = index % columns;
            matrixMineGrounds[r, c].HaveMine = true;
            mineCells.Add(matrixMineGrounds[r, c]);
        }
        safeMineCount = totalCells - mineCount;
    }
    private void CalculateMineCount()
    {
        int totalCells = rows * columns;

        // Mathf.FloorToInt arredonda para baixo 
        mineCount = Mathf.FloorToInt(totalCells * difficultyRatio);

        // Segurança mínima: Pelo menos 1 mina
        if (mineCount < 1) mineCount = 1;

        // Segurança máxima: Deixa pelo menos 1 buraco vazio (para o primeiro clique)
        if (mineCount >= totalCells) mineCount = totalCells - 1;
        
        GameManager.Instance.FlagsAvailable = mineCount;

    }
    public MineGround GetMineGroundAt(int r, int c)
    {
        if (r >= 0 && r < rows && c >= 0 && c < columns)
        {
            return matrixMineGrounds[r, c];
        }
        return null;
    }
    void OnDestroy()
    {
        foreach (Transform child in gridParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    private void OnGameOver()
    {
        // Revela todas as minas
        foreach (MineGround mineCell in mineCells)
        {
            mineCell.RevealMine();
        }
    }

    public void SafeMineChecked()
    {
        safeMineCount--;
        if (safeMineCount <= 0)
        {
            MyEventSystem.RaiseTryGameWin();
        }
    }
}