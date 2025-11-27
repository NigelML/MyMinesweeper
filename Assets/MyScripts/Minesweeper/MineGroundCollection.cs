using System.Collections.Generic;
using UnityEngine;

public class MineGroundCollection : MonoBehaviour
{
    [Tooltip("Parent object that holds the grid cells")]
    [SerializeField] private GameObject gridParent;
    [Tooltip("Prefab for the individual cell")]
    [SerializeField] private GameObject cellPrefab;
    [Tooltip("Number of rows in the grid")]
    [SerializeField] private int rows;
    [Tooltip("Number of columns in the grid")]
    [SerializeField] private int columns;

    [Space, Tooltip("Dificuldade (0.1 = 10%, 0.2 = 20%). OBS: Apesar de ser visualizado, atualmente é ajustado pelo GameManager.")]
    [Range(0.1f, 0.25f)] // Limita o slider no Inspector para não quebrar o jogo
    [SerializeField] private float difficultyRatio = 0.15f; // Atualmente é definido pelo GameManager, porém pode ser ajustado aqui para testes rápidos
    private int mineCount;
    private int safeMineCount;

    private MineGround[,] matrixMineGrounds;
    void OnEnable()
    {
        MyEventSystem.OnGameOver += HandleGameOver;
        MyEventSystem.OnCellChecked += SafeMineChecked;
    }
    void OnDisable()
    {
        MyEventSystem.OnGameOver -= HandleGameOver;
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
    void OnDestroy()
    {
        foreach (Transform child in gridParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    /// <summary>
    /// Sets up the grid of MineGround cells
    /// </summary>
    private void SetGrid()
    {
        matrixMineGrounds = new MineGround[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Instance already defining gridParent as parent.
                // This automatically places the object in the Layout Group list.
                GameObject cellObj = Instantiate(cellPrefab, gridParent.transform);

                // Organization (Optional but recommended for debugging)
                cellObj.name = $"Cell_{i}_{j}";

                // Retrieves the script component to store in the logic.
                MineGround cellScript = cellObj.GetComponent<MineGround>();

                // Saved in your logical array [,]
                matrixMineGrounds[i, j] = cellScript;

            }
        }
    }
    /// <summary>
    /// Sets the sister cells for each MineGround cell
    /// </summary>
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
    /// <summary>
    /// Inserts mines randomly into the grid
    /// </summary>
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

        // Mathf.FloorToInt rounds down
        mineCount = Mathf.FloorToInt(totalCells * difficultyRatio);

        // Minimum safety: At least 1 mine
        if (mineCount < 1) mineCount = 1;

        // Maximum security: Leaves at least 1 empty hole (for the first click)
        if (mineCount >= totalCells) mineCount = totalCells - 1;

        // Update the GameManager with the mine count
        GameManager.Instance.FlagsAvailable = mineCount;
    }
    /// <summary>
    /// Get the MineGround at specific row and column
    /// </summary>
    /// <param name="r"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public MineGround GetMineGroundAt(int r, int c)
    {
        if (r >= 0 && r < rows && c >= 0 && c < columns)
        {
            return matrixMineGrounds[r, c];
        }
        return null;
    }
    /// <summary>
    /// Handles game over by revealing all mines
    /// </summary>
    private void HandleGameOver()
    {        
        foreach (MineGround mineCell in mineCells)
        {
            mineCell.RevealMine();
        }
    }
    /// <summary>
    /// Handles safe mine checked event
    /// </summary>
    public void SafeMineChecked()
    {
        safeMineCount--;
        if (safeMineCount <= 0)
        {
            MyEventSystem.RaiseTryGameWin();
        }
    }
}