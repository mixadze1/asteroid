using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common;
public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject _mouseButton;
    [SerializeField] private GameObject _keyBoardButton;


    private void Awake()
    {
        if (PlayerPrefs.GetInt(Const.GameController.MOUSE) == 0)
        {
            _keyBoardButton.SetActive(true);
            _mouseButton.SetActive(false);
        }
        else
        {
            _keyBoardButton.SetActive(false);
            _mouseButton.SetActive(true);
        }
       
    }

    

    public void StartGame()
    {
        SceneManager.LoadScene(Const.Scenes.GAME);
    }

    public void MouseGaming()
    {
        PlayerPrefs.SetInt(Const.GameController.MOUSE, 1);

        _keyBoardButton.SetActive(false);
        _mouseButton.SetActive(true);
    }

    public void KeyBoardGaming()
    {
        PlayerPrefs.SetInt(Const.GameController.MOUSE, 0);
            
        _keyBoardButton.SetActive(true);
        _mouseButton.SetActive(false);
    }

 

}

