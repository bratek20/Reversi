using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel : MonoBehaviour
{
    public event Action update;

    private FieldState[,] fields = new FieldState[8, 8];

    public void Init()
    {
        Utils.ForEachCoord((i, j) =>
        {
            fields[i, j] = FieldState.EMPTY;
        });
        fields[3, 3] = FieldState.BLACK;
        fields[3, 4] = FieldState.WHITE;
        fields[4, 3] = FieldState.WHITE;
        fields[4, 4] = FieldState.BLACK;

        update?.Invoke();
    }

    public FieldState GetFieldState(int i, int j)
    {
        if (CheckRange(i) && CheckRange(j))
        {
            return fields[i, j];
        }
        throw new Exception("Bad field coords");
    }

    public void SetColor(int i, int j, FieldState state)
    {

    }

    private bool CheckRange(int i)
    {
        return 0 <= i && i < 8; 
    }
}
