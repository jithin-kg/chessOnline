using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRules
{
    public static readonly CheckRules obj = new CheckRules();
    private GameObject piece;

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
    public void SetGameObject(GameObject obj)
    {
        piece = obj;
    }
}
