using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BoardView board;

    private BoardController controller = new BoardController();
    private BoardModel model;

    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        model = new BoardModel();
        board.Setup(model);
        controller.Setup(model);

        model.ResetBoard();
    }
}
