using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _healthText;

    private int _score;
    private int _health;
    public const string SCORE = "Score";

    public int LargeAsteroidScore;
    public int MediumAteroidScore;
    public int SmallAteroidScore;
    public int NloScore;

    public static GUIManager _instance;

    void Awake()
    {
        _instance = GetComponent<GUIManager>();
        _scoreText.text = _score.ToString();
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            _scoreText.text = _score.ToString();
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            _healthText.text = _health.ToString();
        }
    }
}

