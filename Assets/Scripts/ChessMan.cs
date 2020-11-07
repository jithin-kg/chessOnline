using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMan : MonoBehaviour
{
    //references game objects
    GameObject controller;
    GameObject validMovesIdication; // indicates valid moves in chess board while playing

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
        setCords(); //adjust the transform, setting our own co-ordinates
        switch (this.name)
        {
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook; break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knight; break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop; break;
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = black_king; break;
            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black_queen; break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn; break;

            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook; break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knight; break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop; break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king; break;
            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = white_queen; break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn; break;

        }
    }

    public void setCords()
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
}

