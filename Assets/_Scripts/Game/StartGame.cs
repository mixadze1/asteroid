using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _choiceMenu;

    private void Game()
    {
        _game.BeginNewGame();
        _choiceMenu.SetActive(false);
    }

    public void White()
    {     
        _game.Type = SpaceShipType.White;
        Game();
    }
    public void Black()
    {        
        _game.Type = SpaceShipType.Black;
        Game();
    }
    public void Fast()
    { 
        _game.Type = SpaceShipType.Fast;
        Game();
    }
}
