using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : Player
{
    protected override void OnModelUpdate()
    {
        base.OnModelUpdate();
        if(IsMyTurn())
        {
            Communicator.MoveData move = Communicator.CalcOptimalMove(Controller.GetBoardSnapshot(), Color, 5);
            Debug.Log(move.I + " " + move.J);
            bool moveResult = Controller.TryMakeMove(Color, move.I, move.J);
            if(!moveResult)
            {
                Debug.LogError("AI move failed");
            }
        }
    }
}
