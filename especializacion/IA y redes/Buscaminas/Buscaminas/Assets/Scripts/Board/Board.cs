using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [Header("board")]
    [SerializeField] private int rows = 10;
    [SerializeField] private int columns = 10;
    [SerializeField] private int mines = 10;
    [SerializeField] private int nFlags = 0;
    [SerializeField] private bool perder=false;
    [Header("Cell")]
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private float cellsize = 1.0f;
    private Cell[,] cells;
    [Header("Menu")]
    [SerializeField] Menu menu;
    public int Rows { get => rows; set => rows = value; }
    public int Columns { get => columns; set => columns = value; }
    public int Mines { get => mines; set => mines = value; }
    public Cell[,] Cells { get => cells; set => cells = value; }
    public bool Perder { get => perder; set => perder = value; }

    private void Start()
    {

    }
    public void IniciarTablero()
    {
        GenerateBoard();
        PlaceMines();
        CountAdjacenteMinesAllCells();
        RevealFirstBlank();
    }
    private void GenerateBoard()
    {
        Cells = new Cell[Rows, Columns];
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                Vector3 position = new Vector3(col * cellsize, row * cellsize, 0);
                Cell cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                cell.Inicialice(row, col, this,menu);
                Cells[row, col] = cell;
            }
        }
    }
    /*
    private void PlaceMines(int mines) 
    {
        for (int i = 0; i <= (rows-1); i++)
        {

            for (int j = 0; j <= (columns-1); j++) {

                if ((mines >= 0) && Random.Range(0, 101)<=25) {
                    cells[i, j].IsMine = true;
                    mines--;
                }
                if ((mines<=0)&&(i==rows - 1&& i == columns - 1)) {
                    i = 0;
                    j = 0;
                }
            }

        }
    }
   */
    public bool TodasReveladas()
    {
        int casillas = (rows * columns) - mines;
        int count = 0;
        for (int i = 0; i <= Rows - 1; i++)
        {
            for (int j = 0; j <= Columns - 1; j++)
            {
                count += Cells[i, j].Isreveal ? 1 : 0;
            }
        }
        return casillas == count;
    }
    public void RevealFirstBlank()
    {
        bool first = false;
        for (int i = 0; i <= Rows - 1; i++)
        {
            for (int j = 0; j <= Columns - 1; j++)
            {
                if(Cells[i, j].Adjacent == 0 && !first && !Cells[i, j].IsMine)
                {
                    Cells[i, j].Interact();
                    first = true;
                }
            }
        }
    }
    public void PlaceMines()
    {
        List<Cell> abaliblecells = new List<Cell>();

        for (int row = 0; row < Rows; row++)
        {
            for(int col = 0;col < Columns; col++)
            {
                abaliblecells.Add(Cells[row,col]);
            }
        }
        for(int i = abaliblecells.Count - 1; i >= 0; i--)
        {
            int j = Random.Range(0,i+1);
            (abaliblecells[i], abaliblecells[j]) = (abaliblecells[j], abaliblecells[i]);
        }
        for (int i = 0; i < Mines;i++){
            abaliblecells[i].IsMine = true;
        }
    }
    public void RevealCell(int row, int column) {
        Cells[row, column].Isreveal = true;
        
        if (Cells[row,column].Adjacent == 0&& ! Cells[row, column].IsMine)
        {
            RevealAdjacentBlank(row, column);
        }
        if (TodasReveladas())
        {
            Time.timeScale=0;
            menu.Ganar();
        }
    }
    public void RevealAdjacentBlank(int row, int column)
    {


        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int rowI = row + i;
                int columnJ = column + j;
                if ((rowI >= 0) &&
                    (columnJ >= 0) &&
                    (rowI < this.Rows) &&
                    (columnJ < this.Columns) &&
                    (i != 0 || j != 0))
                {
                    if (!Cells[rowI, columnJ].Isreveal)
                    {
                        Cells[rowI, columnJ].Interact();
                    }

                }
            }
        }   
    }
    private void CountAdjacenteMinesAllCells()
    {

        for (int i = 0; i <= Rows - 1; i++)
        {

            for (int j = 0; j <= Columns - 1; j++)
            {
                Cells[i,j].Adjacent=CountAdjacenteMines(i,j);
            }
        }
    }
    private int CountAdjacenteMines(int row, int column)
    {
        int count = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int rowI = row + i;
                int columnJ=column + j;
                if ((rowI >= 0) &&
                    (columnJ >= 0) &&
                    (rowI < this.Rows) &&
                    (columnJ < this.Columns) &&
                    (i != 0 || j != 0))
                {
                    if (Cells[rowI, columnJ].IsMine)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }
    
    public void FlagCell(int row, int column)
    {
        Cell cell = Cells[row, column];
        if (!cell.IsFlaged)
        {
           
            if (nFlags < Mines)
            {
                cell.IsFlaged = true;
                cell.Number.GetComponent<TextMeshPro>().text = "F";
            }
        }
        else
        {
            cell.IsFlaged = !cell.IsFlaged;
            cell.Number.GetComponent<TextMeshPro>().text = "";
        }
    }
}
