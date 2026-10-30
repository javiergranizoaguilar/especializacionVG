using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Board board;
    private int row;
    private int column;
    [SerializeField] private bool isMine;
    private int adjacent;
    private bool isreveal;
    private bool isFlaged;
    [SerializeField] GameObject number;
    private Menu menu;

    public bool IsMine { get => isMine; set => isMine = value; }
    public int Adjacent { get => adjacent; set => adjacent = value; }
    public bool Isreveal { get => isreveal; set => isreveal = value; }
    public bool IsFlaged { get => isFlaged; set => isFlaged = value; }
    public Board Board { get => board; set => board = value; }
    public int Row { get => row; set => row = value; }
    public int Column { get => column; set => column = value; }
    public GameObject Number { get => number; set => number = value; }
    public Menu Menu { get => menu; set => menu = value; }

    public void Start()
    {
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Inicialice(int row,int column,Board board,Menu menu) {
        this.Row = row;
        this.Column = column;
        this.Board = board;
        this.IsMine = false;
        this.Adjacent = 0;
        this.Isreveal = false;
        this.IsFlaged = false;
        this.Menu= menu;
    }
    public void Interact()
    {
        if (!this.isFlaged)
        {
            Board.RevealCell(Row, Column);
            if (isMine)
            {
                GetComponent<Renderer>().material.color = Color.red;
                Time.timeScale = 0;
                Menu.Perder();
                board.Perder = true;

            }
            else
            {
                GetComponent<Renderer>().material.color = Color.green;
                number.GetComponent<TextMeshPro>().text = Adjacent.ToString();
                
            }
            
        }
    }
    public void Flag() {
        board.FlagCell(Row,Column);

    }

}
