﻿using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action update;

    public ColorState Color { get; private set; }
    public BoardController Controller { get; private set; }

    public void Setup(ColorState color, BoardController controller)
    {
        Color = color;
        Controller = controller;
        controller.OnModelUpdate(OnBoardModelUpdated);
    }

    public bool IsMyTurn()
    {
        return Controller.CurrentColor == Color;
    }

    public int CalcMyPieces()
    {
        return Controller.CalcPieces(Color);
    }

    protected virtual void OnBoardModelUpdated()
    {
        update?.Invoke();
    }

}
