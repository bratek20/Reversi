using System;

public class BoardModel
{
    private static int[] di = { -1, 0, 1, 0, -1, -1, 1, 1 };
    private static int[] dj = { 0, -1, 0, 1, -1, 1, -1, 1 };

    public event Action update;

    private FieldState[,] fields = new FieldState[8, 8];

    public ColorState CurrentColor { get; private set; } = ColorState.BLACK;

    internal void ResetBoard()
    {
        Utils.ForEachCoord((i, j) =>
        {
            fields[i, j] = FieldState.EMPTY;
        });
        fields[3, 3] = FieldState.BLACK;
        fields[3, 4] = FieldState.WHITE;
        fields[4, 3] = FieldState.WHITE;
        fields[4, 4] = FieldState.BLACK;

        CurrentColor = ColorState.BLACK;
        CalcSelectableFields();
        SignalUpdate();
    }

    internal FieldState GetFieldState(int i, int j)
    {
        return fields[i, j];
    }

    internal void MakeMove(int i, int j)
    {
        fields[i, j] = CurrentColor.ToFieldState();
        for (int k = 0; k < 8; k++)
        {
            if (CheckSelectionDir(CurrentColor, i, j, di[k], dj[k]))
            {
                ChangeColor(CurrentColor, i, j, di[k], dj[k]);
            }
        }
        CurrentColor = CurrentColor.Other();
        CalcSelectableFields();

        SignalUpdate();
    }

    private void ChangeColor(ColorState color, int i, int j, int di, int dj)
    {
        i += di;
        j += dj;
        while (fields[i, j].ToColor() != color)
        {
            fields[i, j] = color.ToFieldState();
            i += di;
            j += dj;
        }
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
        if (fields[i, j] != FieldState.EMPTY)
        {
            return false;
        }
        bool isSelectable = false;
        for (int k = 0; k < 8; k++)
        {
            isSelectable |= CheckSelectionDir(CurrentColor, i, j, di[k], dj[k]);
        }
        return isSelectable;
    }

    private bool CheckSelectionDir(ColorState color, int i, int j, int di, int dj)
    {
        bool otherColorFound = false;
        i += di;
        j += dj;
        while (CheckRange(i) && CheckRange(j))
        {
            if (!fields[i, j].IsColor())
            {
                return false;
            }
            if (fields[i, j].ToColor() == color)
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
