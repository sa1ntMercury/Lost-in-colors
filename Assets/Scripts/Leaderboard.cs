 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.Networking;

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
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }


        LeaderboardCreator.GetLeaderboard(_publicLeaderboardKey, ((msg) =>
        {

            SetLeaderboard(PlayerPrefs.GetString(Settings.PlayerName), PlayerPrefs.GetInt(Settings.HighScore));

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
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }

        LeaderboardCreator.UploadNewEntry(_publicLeaderboardKey, username, score, ((msg) => {

        }));
    }

    private void GetMyPlace()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return;
        }

        LeaderboardCreator.GetLeaderboard(_publicLeaderboardKey, ((msg) =>
        {
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i].Username == PlayerPrefs.GetString(Settings.PlayerName))
                {
                    PlayerPrefs.SetInt(Settings.MyPlace, msg[i].Rank);
                }
            }
        }));
    }
}
