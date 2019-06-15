using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private MainMenu mainMenu = null;
    [SerializeField]
    private Button resetButton = null;
    [SerializeField]
    private PlayerInfoPanel p1Panel = null;
    [SerializeField]
    private PlayerInfoPanel p2Panel = null;

    private void Awake()
    {
        resetButton.onClick.AddListener(OnResetClicked);
    }

    public void Setup(Player p1, Player p2)
    {
        p1Panel.Setup(p1);
        p2Panel.Setup(p2);
    }

    private void OnResetClicked()
    {
        mainMenu.Show();
    }
}
