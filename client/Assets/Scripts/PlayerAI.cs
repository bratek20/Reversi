using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : Player
{
    [SerializeField]
    private int recurenceDeep = 7;

    protected override void OnBoardModelUpdated()
    {
        base.OnBoardModelUpdated();
        if(IsMyTurn())
        {
            Communicator.MoveData move = Communicator.CalcOptimalMove(Controller.GetBoardSnapshot(), Color, recurenceDeep);
            if(move.IsPass())
            {
                return;
            }

            StartCoroutine(ApplyMove(move.I, move.J));
        }
    }

    private IEnumerator ApplyMove(int i, int j)
    {
        yield return new WaitForSeconds(0.5f);
        bool moveResult = Controller.TryMakeMove(Color, i, j);
        if (!moveResult)
        {
            Debug.LogError("AI move failed");
        }
    }
 }

