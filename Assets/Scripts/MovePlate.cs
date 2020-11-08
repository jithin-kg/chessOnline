
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    GameObject reference;

    int xBoardPosition;
    int yBoardPosition;
    public bool blackkingUnderAttck = false;
    
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
        //empty the check plates list
        King.obj.EmptyvalidMovesList();
        //check whether the king is in the enemy piece attacking position
        // check if ther is a bishop in line
        IsKingInAttack();








        //string name = reference.name;
        //if(name!="white_king" || name != "black_king")
        //{
        //    if(name == "white_bishop")
        //    {
        //        makeInvisibleMovePlate(1, 1);
        //        makeInvisibleMovePlate(-1, 1);
        //        makeInvisibleMovePlate(1, -1);
        //        makeInvisibleMovePlate(-1, -1);
               
        //    }
            
        //}
    }

   public void IsKingInAttack()
    {
        ChessMan c = reference.GetComponent<ChessMan>();
        GameObject k;
        k = GameObject.Find("black_king");
        int a = k.GetComponent<ChessMan>().getXBoard();
       int b =  k.GetComponent<ChessMan>().getYBoard();
       if(k.GetComponent<ChessMan>().IsKingUnderAttack())
        {
            Debug.Log("king under attack");
            blackkingUnderAttck = true;
            //find valid moves for other pieces to block the current attack
            //validMovesWhenKingInCheck = new List<List<int>>();
            

            //item.Add(King.obj.GetCheckerX());
            //item.Add(King.obj.GetCheckerY());
            //for black king
            int yValue = b - 1;
            //adding all the plcaed in board that are under attack, which can be coverd by other pieces of black
            for(int i =a-1; i >King.obj.GetCheckerX(); i--)
            {
                int xValue = i;
                List<int> item = new List<int>();
                item.Add(xValue);
                item.Add(yValue);
                yValue--;
                King.obj.AdditemValidMoveslist(item);
            }
            

        }
        else
        {
            Debug.Log("king not in attack");
        }
       
        

        // check whether ther is a bishop eying at this position, so asign the features of bishop to king and draw
        //move plates



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
