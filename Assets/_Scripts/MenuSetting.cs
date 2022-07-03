using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSetting : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _menuSetting;
    [SerializeField] private GameObject _continueButton;

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        _menuSetting.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        _menuSetting.SetActive(false);

    }
    
    public void RestartGame()
    {
        Time.timeScale = 1;
        _game.BeginNewGame();
        _menuSetting.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(Const.Scenes.MAIN_MENU);
    }
}
