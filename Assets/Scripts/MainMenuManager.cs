using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private LevelLoader _leverLoader;

    [Header("TEXT")]
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private TMP_Text _changeName;
    [SerializeField] private TMP_Text _yourPlaceGlobal;
    [SerializeField] private TMP_Text _yourNameGlobal;
    [SerializeField] private TMP_Text _yourScoreGlobal;
    [SerializeField] private TMP_Text _greetingsName;

    [Header("PANELS")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _namePanel;
    [SerializeField] private GameObject _scoreTable;
    [SerializeField] private GameObject _noInternetConnection;


    private Leaderboard _leaderboard;

    public void Awake()
    { 
        if(PlayerPrefs.GetString(Settings.PlayerName).Length < 5)
        {
            PlayerPrefs.SetFloat(Settings.Volume, 0.5f);
            _mainPanel.SetActive(false);
            _namePanel.SetActive(true);
        }

        _leaderboard = GetComponent<Leaderboard>();
        _greetingsName.text = $"Hi, {PlayerPrefs.GetString(Settings.PlayerName)}!";
    }
    public void Play()
    {
        StartCoroutine(_leverLoader.LoadLevel("Game"));
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Score()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _scoreTable.SetActive(false);
            _noInternetConnection.SetActive(true);
            return;
        }

        _noInternetConnection.SetActive(false);
        _leaderboard.GetLeaderboard();
        _scoreTable.SetActive(true);

        _highScore.text = PlayerPrefs.GetInt(Settings.HighScore).ToString();

        _yourPlaceGlobal.text = PlayerPrefs.GetInt(Settings.MyPlace).ToString();
        _yourNameGlobal.text = PlayerPrefs.GetString(Settings.PlayerName);
        _yourScoreGlobal.text = PlayerPrefs.GetInt(Settings.HighScore).ToString();
    }

    public void Next()
    {
        if(_playerName.text.Length >= 5)
        {
            PlayerPrefs.SetString(Settings.PlayerName, _playerName.text);
            _namePanel.SetActive(false);
            _mainPanel.SetActive(true);

            _greetingsName.text = $"Hi, {PlayerPrefs.GetString(Settings.PlayerName)}!";
        }
    }

    public void ChangeName()
    {
        if (_changeName.text.Length >= 5)
        {
            
            PlayerPrefs.SetString(Settings.PlayerName, _changeName.text);
            _greetingsName.text = $"Hi, {PlayerPrefs.GetString(Settings.PlayerName)}!";
        }
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }


}
