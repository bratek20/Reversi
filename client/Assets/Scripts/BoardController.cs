using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController
{
    private BoardModel model;

    public ColorState CurrentColor { get => model.CurrentColor; }

    public void Setup(BoardModel model)
    {
        this.model = model;
    }

    public void ResetBoard()
    {
        model.ResetBoard();
    }

    public bool TryMakeMove(ColorState color, int i, int j)
    {
        if (CurrentColor != color)
        {
            return false;
        }
        if (model.GetFieldState(i, j) != FieldState.SELECTABLE)
        {
            return false;
        }
        model.MakeMove(i, j);
        return true;
    }
}