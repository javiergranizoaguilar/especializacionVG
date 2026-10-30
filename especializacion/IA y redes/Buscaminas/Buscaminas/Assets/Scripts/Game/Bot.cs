using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] Board board;
    private int minas;
    private int row;
    private int col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IniciarBot(int dificultad)
    {
        minas = board.Mines;
        row = board.Rows;
        col = board.Columns;
        switch (dificultad) {
            case 1:
                BotFacil();
                break;
            case 2:
                BotMedio();
                break;
            case 3:
                BotDificil();
                break;
        }

    }
    private void BotFacil()
    {
        int x;
        int y;
        while (!board.TodasReveladas()|| !board.Perder) 
        { 
            x=Random.Range(0, row);
            y=Random.Range(0, col);
            if(!board.Cells[x, y].Isreveal)
            {
                board.Cells[x, y].Interact();
            }
        }
    }
    private void BotMedio()
    {

    }
    private void BotDificil()
    {

    }
}
