using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button playButton = null;
    [SerializeField]
    private Toggle p1AIToggle = null;
    [SerializeField]
    private Toggle p2AIToggle = null;
    [SerializeField]
    private GameManager game = null;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayClicked);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnPlayClicked()
    {
        game.StartGame(p1AIToggle.isOn, p2AIToggle.isOn);
        Hide();
    }
}
