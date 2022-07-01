using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private Game _game;

   public void ActivateMenu()
    {
        Time.timeScale = 0;
       // _menuSetting.SetActive(true);
    }

    public void RestartGame()
    {
        _game.BeginNewGame();
    }

    public void MouseGaming()
    {
        _game.GameControl = GameControl.Mouse;
    }

    public void KeyBoardGaming()
    {
        _game.GameControl = GameControl.KeyBoard;
    }
}
public enum GameControl
{
    Mouse,
    KeyBoard
}

