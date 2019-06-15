using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanel : MonoBehaviour
{
    [SerializeField]
    private Image playerColor = null;
    [SerializeField]
    private Text aiText = null;
    [SerializeField]
    private Text piecesNumberText = null;
    [SerializeField]
    private GameObject highlight = null;

    private Player player;

    public void Setup(Player p)
    {
        player = p;
        p.update += OnPlayerUpdate;
        aiText.text = ((p as PlayerAI) != null).ToString();
        playerColor.color = player.Color.ToUnityColor();
    }

    private void OnPlayerUpdate()
    {
        highlight.SetActive(player.IsMyTurn());
        piecesNumberText.text = player.CalcMyPieces().ToString();
    }
}
