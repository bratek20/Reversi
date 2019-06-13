using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private Piece piece;
    [SerializeField]
    private Selection selection;

    public float PieceHeight
    {
        get { return piece.Height; }
    }

    public bool Selectable
    {
        get { return selection.Active; }
        set { selection.Active = value; }
    }

    public int I { get; private set; }
    public int J { get; private set; }

    public void SetupCoords(int i, int j)
    {
        I = i;
        J = j;
    }

    public void SetupState(FieldState state)
    {
        piece.SetColor(state);
        Selectable = state == FieldState.SELECTABLE;
    }

    private void OnMouseEnter()
    {
        selection.SetHighlight(true);
    }

    private void OnMouseExit()
    {
        selection.SetHighlight(false);
    }
}
