using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRules
{
    public static readonly CheckRules obj = new CheckRules();
    private GameObject controller;
    private GameObject reference;

    //These variables are used to keep track of pieces moved offscreen to check whether that movement is allowed
    //ie, the resulting move gives a back check
    int prevX;
    int prevY;
    int updatedX;
    int updatedY;

    private CheckRules() { }
    // This function checks whether the other non king piece can move into block kings check
    public bool IsValidMove(int x, int y)
    {
       List<List<int>> validMovesPositions =  King.obj.GetValidMovesList();
        if (validMovesPositions.Count <= 0) return true;
        foreach(List<int> item  in validMovesPositions)
        {
            int attackedX = item[0];
            int attackedY = item[1];
            if(attackedX == x && attackedY == y)
            {
                //empty the list only after making the move, not by tapping that
                //King.obj.EmptyvalidMovesList();
                return true;
            }
        }
        return false;
    }

    //Function check if the move of the current piece same team piece result in a check by a oponent
    public bool IsMoveAllowed(int xBoardPosition, int yBoardPosition, int currentX, int currentY)
    {
        prevX = currentX;
        prevY = currentY;
        updatedX = xBoardPosition;
        updatedY = yBoardPosition;
        GameObject[,] posns = controller.GetComponent<Game>().positions;
        Game sc = controller.GetComponent<Game>();

        // move the current pawn to new location and check that result in a back check
        

        controller.GetComponent<Game>()
           .setPositionEmpty(currentX, currentY);


        //move the piece to the new location
        reference.GetComponent<ChessMan>().setXOnBoard(xBoardPosition);
        reference.GetComponent<ChessMan>().setYOnBoard(yBoardPosition);
        controller.GetComponent<Game>().setPosition(reference); // update chess pice in board | matrx
        GameObject[,] posns2 = controller.GetComponent<Game>().positions;
        
        //TODO reset position after moving

        //controller.GetComponent<ChessMan>().setCoords();

        //check if the current move  resultans in a back check


        return !IsKingInAttack();
    }
    public bool IsKingInAttack()
    {
        ChessMan c = reference.GetComponent<ChessMan>();
        GameObject k;
        k = GameObject.Find("black_king");
        int a = k.GetComponent<ChessMan>().getXBoard();
        int b = k.GetComponent<ChessMan>().getYBoard();
       
        if (k.GetComponent<ChessMan>().IsKingUnderAttack())
        {
            Debug.Log("king under attack from check rules");
            //blackkingUnderAttck = true;
            //find valid moves for other pieces to block the current attack
            //validMovesWhenKingInCheck = new List<List<int>>();


            //item.Add(King.obj.GetCheckerX());
            //item.Add(King.obj.GetCheckerY());
            //for black king
            //int yValue = b - 1;
            ////adding all the plcaed in board that are under attack, which can be coverd by other pieces of black
            //for (int i = a - 1; i > King.obj.GetCheckerX(); i--)
            //{
            //    int xValue = i;
            //    List<int> item = new List<int>();
            //    item.Add(xValue);
            //    item.Add(yValue);
            //    yValue--;
            //    King.obj.AdditemValidMoveslist(item);
            //}
            return true;

        }
        else
        {
            Debug.Log("king not in attack from check rules");
            return false;
        }



        // check whether ther is a bishop eying at this position, so asign the features of bishop to king and draw
        //move plates



    }
    public void ResetOffScreenMovedPiece()
    {
        controller.GetComponent<Game>()
          .setPositionEmpty(updatedX, updatedY);


        //move the piece to the new location
        reference.GetComponent<ChessMan>().setXOnBoard(prevX);
        reference.GetComponent<ChessMan>().setYOnBoard(prevY);
        controller.GetComponent<Game>().setPosition(reference);
    }
    public void SetGameObject(GameObject obj)
    {
        controller = obj;
    }

    internal void SetReference(GameObject re)
    {
        reference = re;
    }
}
