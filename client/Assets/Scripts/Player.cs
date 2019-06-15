using UnityEngine;

public class Player : MonoBehaviour
{
    public ColorState Color { get; private set; }
    public BoardController Controller { get; private set; }

    public void Setup(ColorState color, BoardController controller)
    {
        Color = color;
        Controller = controller;
    }

    public bool MyTurn()
    {
        return Controller.CurrentColor == Color;
    }
}
