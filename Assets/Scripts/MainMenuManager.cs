using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private TMP_Text _changeName;
    [SerializeField] private TMP_Text _placeHolderText;
    [SerializeField] private TMP_Text _yourPlaceGlobal;
    [SerializeField] private TMP_Text _yourNameGlobal;
    [SerializeField] private TMP_Text _yourScoreGlobal;

    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _namePanel;

    private Leaderboard _leaderboard;

    public void Awake()
    { 
        if(PlayerPrefs.GetString("PlayerName").Length < 5)
        {
            _mainPanel.SetActive(false);
            _namePanel.SetActive(true);
        }
        _leaderboard = GetComponent<Leaderboard>();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Score()
    {
        _leaderboard.GetLeaderboard();

        _placeHolderText.text = PlayerPrefs.GetString("PlayerName");

        _highScore.text = PlayerPrefs.GetInt("HighScore").ToString();

        _yourPlaceGlobal.text = PlayerPrefs.GetInt("MyPlace").ToString();
        _yourNameGlobal.text = PlayerPrefs.GetString("PlayerName");
        _yourScoreGlobal.text = PlayerPrefs.GetInt("HighScore").ToString();


    }

    public void Next()
    {
        if(_playerName.text.Length >= 5)
        {
            PlayerPrefs.SetString("PlayerName", _playerName.text);
            _namePanel.SetActive(false);
            _mainPanel.SetActive(true);

        }
    }

    public void ChangeName()
    {
        if (_changeName.text.Length >= 5)
        {
            PlayerPrefs.SetString("PlayerName", _changeName.text);
        }
    }


}
