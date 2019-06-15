using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel : MonoBehaviour
{
    private static int[] di = { -1, 0, 1, 0, -1, -1, 1, 1 };
    private static int[] dj = { 0, -1, 0, 1, -1, 1, -1, 1 };

    public event Action update;

    private FieldState[,] fields = new FieldState[8, 8];
    private Color currentColor = Color.BLACK;

    public void ResetBoard()
    {
        Utils.ForEachCoord((i, j) =>
        {
            fields[i, j] = FieldState.EMPTY;
        });
        fields[3, 3] = FieldState.BLACK;
        fields[3, 4] = FieldState.WHITE;
        fields[4, 3] = FieldState.WHITE;
        fields[4, 4] = FieldState.BLACK;

        currentColor = Color.BLACK;
        CalcSelectableFields();
        SignalUpdate();
    }

    public FieldState GetFieldState(int i, int j)
    {
        if (CheckRange(i) && CheckRange(j))
        {
            return fields[i, j];
        }
        throw new Exception("Bad field coords");
    }

    public void SetColor(int i, int j, Color color)
    {

    }

    private bool CheckRange(int i)
    {
        return 0 <= i && i < 8; 
    }

    private void CalcSelectableFields()
    {
        Utils.ForEachCoord((i, j) =>
        {
            fields[i, j].ResetSelect();
            fields[i, j] = IsFieldSelectable(i, j) ? FieldState.SELECTABLE : fields[i, j];
        });
    }

    private bool IsFieldSelectable(int i, int j)
    {
        if(fields[i, j] != FieldState.EMPTY)
        {
            return false;
        }
        bool isSelectable = false;
        for (int k = 0; k < 8; k++)
        {
            isSelectable |= CheckSelectionDir(i, j, di[k], dj[k]);
        }
        return isSelectable;
    }

    private bool CheckSelectionDir(int i, int j, int di, int dj)
    {
        bool otherColorFound = false;
        i += di;
        j += dj;
        while (CheckRange(i) && CheckRange(j))
        {
            if(!fields[i, j].IsColor())
            {
                return false;
            }
            if(fields[i, j].ToColor() == currentColor)
            {
                return otherColorFound;
            }
            otherColorFound = true;
            i += di;
            j += dj;
        }
        return false;
    }

    private void SignalUpdate()
    {
        update?.Invoke();
    }
}
