using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMan : MonoBehaviour
{
    //references game objects
    public GameObject controller;
    public  GameObject movePlate; // indicates valid moves in chess board while playing

    //positions
    private int xOnBoard = -1;
    private int yOnBoard = -1;

    private string player;

    public Sprite black_rook, black_knight, black_bishop, black_king, black_queen, black_pawn;
    public Sprite white_rook, white_knight, white_bishop, white_king, white_queen, white_pawn;
    // Start is called before the first frame update

    public void activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        setCoords(); //adjust the transform, setting our own co-ordinates
        switch (this.name)
        {
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;

            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;

        }
    }

    public void setCoords()
    {
        float x = xOnBoard;
        float y = yOnBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);

    }



    public int getXBoard()
    {
        return xOnBoard;
    }

    public int getYBoard()
    {
        return yOnBoard;
    }

    public void setXOnBoard(int x)
    {
        xOnBoard = x;
    }
    public void setYOnBoard(int y)
    {
        yOnBoard = y;
    }

    private void OnMouseUp()
    {
        if(!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();
            InitiateMovePlates();
        }
        
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i =0; i < movePlates.Length; i ++)
        {
            Destroy(movePlates[i]);
        }
    }
    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                MakeMovePlatesLine(1, 0);
                MakeMovePlatesLine(-1, 0);
                MakeMovePlatesLine(0, 1);
                MakeMovePlatesLine(0, -1);
                MakeMovePlatesLine(1, 1);
                MakeMovePlatesLine(-1, -1);
                MakeMovePlatesLine(-1, 1);
                MakeMovePlatesLine(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                    LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                MakeMovePlatesLine(1, 1);
                MakeMovePlatesLine(-1, 1);
                MakeMovePlatesLine(1, -1);
                MakeMovePlatesLine(-1, -1);
                break;
            case "black_rook":
            case "white_rook":
                MakeMovePlatesLine(0, 1);
                MakeMovePlatesLine(0, -1);
                MakeMovePlatesLine(-1, 0);
                MakeMovePlatesLine(1, 0);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_pawn":
                PawnMovePlate(xOnBoard, yOnBoard -1);
                break;
            case "white_pawn":
                PawnMovePlate(xOnBoard, yOnBoard +  1);
                break;





        }
    }
    public void MakeMovePlatesLine(int xInccrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();
        
        int x = xOnBoard + xInccrement;
        int y = yOnBoard + yIncrement;

        while (sc.IsValidPositionOnBoard(x, y) && sc.GetPieceAtPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);

            x += xInccrement;
            y += yIncrement;
            
        }
        if (sc.IsValidPositionOnBoard(x, y) &&
                sc.GetPieceAtPosition(x, y).GetComponent<ChessMan>().player != player)
        {
            // that is an enemy piece
            MovePlateAttackSpawn(x, y);
        }

    }

    public void LMovePlate()
    {
        PointMovePlate(xOnBoard + 1, yOnBoard + 2);
        PointMovePlate(xOnBoard - 1, yOnBoard + 2);
        PointMovePlate(xOnBoard + 1, yOnBoard - 2);
        PointMovePlate(xOnBoard - 1, yOnBoard - 2);
        PointMovePlate(xOnBoard + 2, yOnBoard + 1);
        PointMovePlate(xOnBoard - 2, yOnBoard + 1);
        PointMovePlate(xOnBoard - 2, yOnBoard - 1);
        PointMovePlate(xOnBoard + 2, yOnBoard - 1);
    }
    public void SurroundMovePlate()
    {
        PointMovePlate(xOnBoard, yOnBoard + 1);
        PointMovePlate(xOnBoard, yOnBoard - 1);
        PointMovePlate(xOnBoard + 1 , yOnBoard );
        PointMovePlate(xOnBoard - 1, yOnBoard );
        PointMovePlate(xOnBoard +1 , yOnBoard + 1);
        PointMovePlate(xOnBoard -1 , yOnBoard + 1);
        PointMovePlate(xOnBoard -1, yOnBoard - 1);
        PointMovePlate(xOnBoard + 1, yOnBoard - 1);
    }

    public void PointMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        if(sc.IsValidPositionOnBoard(xIncrement, yIncrement))
        {
            GameObject cp = sc.GetPieceAtPosition(xIncrement, yIncrement);
            if(cp == null)
            {
                MovePlateSpawn(xIncrement, yIncrement);
            }else if(cp.GetComponent<ChessMan>().player  != player)
            {
                MovePlateAttackSpawn(xIncrement, yIncrement);
            }
        }
    }
    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if(sc.IsValidPositionOnBoard(x, y))
        {
            if(sc.GetPieceAtPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            } 
            if(sc.IsValidPositionOnBoard(x+1, y) &&  sc.GetPieceAtPosition(x + 1, y) != null &&
               sc.GetPieceAtPosition(x + 1, y).GetComponent<ChessMan>().player!= player ) {
                MovePlateAttackSpawn(x + 1 , y);
            }

            if (sc.IsValidPositionOnBoard(x - 1, y) && sc.GetPieceAtPosition(x - 1, y) != null &&
              sc.GetPieceAtPosition(x - 1, y).GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f; 
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate movePlateScript = mp.GetComponent<MovePlate>();
        movePlateScript.SetReference(gameObject);
        movePlateScript.Setcoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate movePlateScript = mp.GetComponent<MovePlate>();
        movePlateScript.isAttacking = true;
        movePlateScript.SetReference(gameObject);
        movePlateScript.Setcoords(matrixX, matrixY);
    }
}

