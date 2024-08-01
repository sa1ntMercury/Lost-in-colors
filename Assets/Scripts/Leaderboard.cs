using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _places;
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _scores;

    private static readonly string _publicLeaderboardKey = "505e1a73ccd4454c0e969dba116f7cf4406e9a2335eaaf88ba22c7997443eb18";

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(_publicLeaderboardKey, ((msg) => {

            SetLeaderboard(PlayerPrefs.GetString("PlayerName"), PlayerPrefs.GetInt("HighScore"));

            for (int i = 0; i < _places.Count; i++)
            {
                _places[i].text = msg[i].Rank.ToString();
                _names[i].text = msg[i].Username;
                _scores[i].text = msg[i].Score.ToString();
            }
        }));

        GetMyPlace();
    }

    public static void SetLeaderboard(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(_publicLeaderboardKey, username, score, ((msg) => {

        }));
    }

    private void GetMyPlace()
    {
        LeaderboardCreator.GetLeaderboard(_publicLeaderboardKey, ((msg) =>
        {
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i].Username == PlayerPrefs.GetString("PlayerName"))
                {
                    PlayerPrefs.SetInt("MyPlace", msg[i].Rank);
                }
            }
        }));
    }
}
