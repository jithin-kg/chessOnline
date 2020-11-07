using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject ChessPiece;

    //positions for chess pieces
    public GameObject[,] positions = new GameObject[8, 8];
    public GameObject[] blackPieces = new GameObject[16];
    public GameObject[] whitePieces = new GameObject[16];

    private string CurrentPlayer = "white";
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //generated = Instantiate(olakka, new Vector3(0, 0, -1), Quaternion.identity);
        //s generated.transform.Translate()
        initBoard();

    }
    private void initBoard()
    {
        whitePieces = new GameObject[]
       {
            create("white_rook", 0, 0), create("white_knight", 1, 0),
            create("white_bishop", 2, 0), create("white_king", 3, 0),
            create("white_queen", 4, 0), create("white_bishop", 5, 0),
            create("white_knight", 6, 0), create("white_rook", 7, 0),

            create("white_pawn", 0, 1), create("white_pawn", 1, 1),
            create("white_pawn",2, 1), create("white_pawn", 3, 1),
            create("white_pawn", 4, 1), create("white_pawn", 5, 1),
            create("white_pawn", 6, 1), create("white_pawn", 7, 1),

       };

        blackPieces = new GameObject[]
        {
            create("black_rook", 0, 7), create("black_knight", 1, 7),
            create("black_bishop", 2, 7), create("black_king", 3, 7),
            create("black_queen", 4, 7), create("black_bishop", 5, 7),
            create("black_knight", 6, 7), create("black_rook", 7, 7),

            create("black_pawn", 0, 6), create("black_pawn", 1, 6),
            create("black_pawn", 2, 6), create("black_pawn", 3, 6),
            create("black_pawn", 4, 6), create("black_pawn", 5, 6),
            create("black_pawn", 6, 6), create("black_pawn", 7, 6),

        };

        //intial chess board position setup
        for (int i = 0; i < blackPieces.Length; i++)
        {
            setPosition(blackPieces[i]);
            setPosition(whitePieces[i]);
        }
    }
    private GameObject create(string name, int x, int y)
    {
        GameObject ob = Instantiate(ChessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChessMan cm = ob.GetComponent<ChessMan>();

        //setting name and X and Y position of a piece in board, initially
        cm.name = name;
        cm.setXOnBoard(x);
        cm.setYOnBoard(y);
        cm.activate();
        return ob;
    }
    private void setPosition(GameObject obj)
    {
        ChessMan cm = obj.GetComponent<ChessMan>();
        positions[cm.getXBoard(), cm.getYBoard()] = obj;
    }

    public void setPositionEmpty(int x, int y)
    {
        positions[x, y] = null;

    }
    public GameObject getObjectAtPosition(int x, int y)
    {
        return positions[x, y];
    }
    public bool positionOnBoard(int x, int y)
    {
        if(x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
        {
            return false;
        }
        return true;
    }
}
