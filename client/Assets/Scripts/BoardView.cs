using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField]
    private Transform leftTop;
    [SerializeField]
    private Transform leftBottom;
    [SerializeField]
    private Transform rightTop;
    [SerializeField]
    private Field fieldPrefab;

    private BoardModel model;
    private Field[,] fields = new Field[8, 8];

    private void Awake()
    {
        Vector3 leftTopPos = leftTop.position;
        Vector3 dx = (rightTop.position - leftTopPos) / 7;
        Vector3 dy = (leftBottom.position - leftTopPos) / 7;
        leftTopPos.y = fieldPrefab.PieceHeight / 2;

        Utils.ForEachCoord((i, j) =>
        { 
            Field f = Instantiate(fieldPrefab, transform);
            f.transform.position = leftTopPos + dx * j + dy * i;
            fields[i, j] = f;
            f.SetupCoords(i, j);
            f.SetupState(FieldState.EMPTY);
        });
    }
    
    public void Setup(BoardModel model)
    {
        this.model = model;
        model.update += OnModelUpdate;
    }

    private void OnModelUpdate()
    {
        Utils.ForEachCoord((i, j) =>
        {
           fields[i, j].SetupState(model.GetFieldState(i, j));
        });
    }
}
