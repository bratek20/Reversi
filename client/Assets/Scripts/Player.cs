using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Color Color { get; private set; }

    public void Setup(Color color)
    {
        Color = color;
    }
}
