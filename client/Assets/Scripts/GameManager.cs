using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BoardView board = null;
    [SerializeField]
    private PlayerAI playerAIPrefab = null;
    [SerializeField]
    private PlayerInput playerInputPrefab = null;

    private BoardController controller = new BoardController();
    private BoardModel model = null;
    private List<Player> players = new List<Player>();

    private void Start()
    {
        ResetGame(false, false);
    }

    private void ResetGame(bool player1AI, bool player2AI)
    {
        model = new BoardModel();
        board.Setup(model);
        controller.Setup(model);

        model.ResetBoard();

        ClearPlayers();
        ColorState player1Color = EnumsUtils.RandomColor();
        AddPlayer(player1AI, player1Color);
        AddPlayer(player2AI, player1Color.Other());
    }

    private void ClearPlayers()
    {
        foreach(Player p in players)
        {
            Destroy(p.gameObject);
        }
        players.Clear();
    }

    private void AddPlayer(bool isAI, ColorState color)
    {
        Player p = Instantiate(GetPrefab(isAI), transform);
        p.Setup(color, controller);
        players.Add(p);
    }

    private Player GetPrefab(bool isAI)
    {
        return isAI ? (Player)playerAIPrefab : playerInputPrefab;
    }


}
