using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private static readonly Vector3 WHITE_ROTATION = Vector3.zero;
    private static readonly Vector3 BLACK_ROTATION = new Vector3(180, 0, 0);

    public float Height
    {
        get; private set;
    }

    private void Awake()
    {
        Bounds bounds = new Bounds();
        var meshes = GetComponents<MeshFilter>();
        foreach (var mesh in meshes)
        {
            bounds.Encapsulate(mesh.mesh.bounds);
        }
        Height = bounds.extents.y * 2;
    }

    public void SetColor(FieldState state)
    {
        if(!state.IsColor())
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.localEulerAngles = state == FieldState.BLACK ? BLACK_ROTATION : WHITE_ROTATION;
            gameObject.SetActive(true);
        }
    }
}
