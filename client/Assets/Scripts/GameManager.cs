using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BoardView board;

    private BoardModel model;

    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        model = new BoardModel();
        board.Setup(model);
        model.Init();
    }
}
