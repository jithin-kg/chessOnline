using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    GameObject reference;

    int xBoardPosition;
    int yBoardPosition;

    //whether the piece the move plate is referencing can attack or not
    public bool isAttacking = false;

    private void Start()
    {
        if (isAttacking)
        {

            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
    private void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (isAttacking)
        {
            GameObject chessPiecce = controller.GetComponent<Game>().GetPieceAtPosition(xBoardPosition, yBoardPosition);
            if (chessPiecce.name == "white_king") controller.GetComponent<Game>().winner("black"); 
            if (chessPiecce.name == "black_king") controller.GetComponent<Game>().winner("white");
            Destroy(chessPiecce);
        }
        //we have to empty the current position before moving the chess piece to new location.
        controller.GetComponent<Game>()
            .setPositionEmpty(reference.GetComponent<ChessMan>().getXBoard(),
            reference.GetComponent<ChessMan>().getYBoard());
        //move the piece to the new location
        reference.GetComponent<ChessMan>().setXOnBoard(xBoardPosition);
        reference.GetComponent<ChessMan>().setYOnBoard(yBoardPosition);
        reference.GetComponent<ChessMan>().setCoords();

        controller.GetComponent<Game>().setPosition(reference);
        controller.GetComponent<Game>().SwitchPlayer();
        reference.GetComponent<ChessMan>().DestroyMovePlates();
    }
    public void Setcoords(int x, int y)
    {
        xBoardPosition = x;
        yBoardPosition = y;
    }
    public void SetReference(GameObject ob)
    {
        reference = ob;
    }
    public GameObject GetReference()
    {
        return reference;
    }
}
