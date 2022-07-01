using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common;
public class Menu : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _menuSetting;
    [SerializeField] private GameObject _mouseButton;
    [SerializeField] private GameObject _keyBoardButton;
    [SerializeField] private GameObject _continueButton;

   public void ActivateMenu()
    {
        Time.timeScale = 0;
        _menuSetting.SetActive(true);
    }

    public void RestartGame()
    {
        _game.BeginNewGame();
    }

    public void MouseGaming()
    {
        _game.GameControl = GameControl.Mouse;
        _keyBoardButton.SetActive(true);
        _mouseButton.SetActive(false);
    }

    public void KeyBoardGaming()
    {
        _game.GameControl = GameControl.KeyBoard;
        _keyBoardButton.SetActive(false);
        _mouseButton.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        _menuSetting?.SetActive(false);

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(Const.Scenes.MAIN_MENU);
    }
}
public enum GameControl
{
    Mouse,
    KeyBoard
}

