using System.Collections.Generic;
using UnityEngine;

public class King 
{
    GameObject controller;
    ChessMan cman;
    // checkedplate list holds all plates which are checked by oponent piece
    List<List<int>> checkedPlate =  new List<List<int>>();
    public static readonly King obj = new King();
    private King() { }
    
    int x, y;

    public void SetCheckerXPosition(int value)
    {
        x = value;
    }
    public void SetChekerYPosition(int value)
    {
        y = value;
    }
    public int  GetCheckerX()
    {
        return x;
    }
    public int GetCheckerY()
    {
        return y;
    }

    public void AdditemValidMoveslist(List<int> item)
    {
        checkedPlate.Add(item);
    }
    public List<List<int>> GetValidMovesList()
    {
        
        return checkedPlate;
    }
    public void EmptyvalidMovesList()
    {
        checkedPlate.Clear();
    }

    public void PointMovePlateKing(string player, int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();
        //ChessMan cman = controller.GetComponent<ChessMan>();
        if (sc.IsValidPositionOnBoard(xIncrement, yIncrement) && IsValidMove(xIncrement, yIncrement))
        {
            GameObject cp = sc.GetPieceAtPosition(xIncrement, yIncrement);
            if (cp == null)
            {
                cman.MovePlateSpawn(xIncrement, yIncrement);
            }
            else if (cp.GetComponent<ChessMan>().player != player)
            {
                cman.MovePlateAttackSpawn(xIncrement, yIncrement);
            }
        }
    }
    public void SetController(GameObject ct, ChessMan cm)
    {
        controller = ct;
        cman = cm;
    }
    public bool IsValidMove(int x, int y)
    {
      
        if (checkedPlate.Count <= 0) return true;
        foreach(List<int> item  in checkedPlate)
        {
            int attackedX = item[0];
            int attackedY = item[1];
            if(attackedX == x && attackedY == y)
            {
                ////empty the list
                //King.obj.EmptyvalidMovesList();
                //return true;
                return false;
            }
        }
        return true;
    }
}
