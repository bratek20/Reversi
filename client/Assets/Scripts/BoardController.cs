using System;
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

    public void OnModelUpdate(Action a)
    {
        model.update += a;
    }

    public void ResetBoard()
    {
        model.ResetBoard();
    }

    public bool IsGameOver()
    {
        return model.GameOver;
    }

    public ColorState GetWinner()
    {
        int blackPieces = CalcPieces(ColorState.BLACK);
        int whitePieces = CalcPieces(ColorState.WHITE);
        return blackPieces > whitePieces ? ColorState.BLACK : ColorState.WHITE; 
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

    public int CalcPieces(ColorState color)
    {
        return model.CalcPieces(color);
    }

    public string GetBoardSnapshot()
    {
        string snapshot = "";
        Utils.ForEachCoord((i, j) =>
        {
            FieldState state = model.GetFieldState(i, j);
            if(!state.IsColor())
            {
                snapshot += ".";
            }
            else
            {
                snapshot += state.ToColor().ToAIString();
            }
        });
        return snapshot;
    }
}