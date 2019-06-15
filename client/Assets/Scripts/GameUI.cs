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

    [SerializeField]
    private GameObject resultPanel = null;
    [SerializeField]
    private Image resultColor = null;
    [SerializeField]
    private Button resultConfirmButton = null;

    private void Awake()
    {
        mainMenu.Show();
        resultPanel.SetActive(false);
        resetButton.onClick.AddListener(OnResetClicked);
        resultConfirmButton.onClick.AddListener(OnResetClicked);
    }

    public void Setup(Player p1, Player p2)
    {
        p1Panel.Setup(p1);
        p2Panel.Setup(p2);
    }

    public void ShowResult(ColorState winnerColor)
    {
        resultColor.color = winnerColor.ToUnityColor();
        resultPanel.SetActive(true);
    }

    private void OnResetClicked()
    {
        resultPanel.SetActive(false);
        mainMenu.Show();
    }
}
