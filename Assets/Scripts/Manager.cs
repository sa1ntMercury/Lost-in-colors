using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{ 
    [SerializeField] private Setup[] _prefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private TMP_Text _points;
    [SerializeField] private TMP_Text _finalScore;

    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _losePanel;

    private int _countUsedPrefabs;
    private int _prefabNumber;
    private List<Setup> _setups;
    public int Points { get; private set; }

    private void Awake()
    {
        AddSetup(new Vector3());
    }

    public void AddSetup(Vector3 position)
    {
        if (_countUsedPrefabs >= 1)
        {
            int nextPrefabNumber = Random.Range(0, _prefab.Length);

            if(_prefabNumber != nextPrefabNumber)
            {
                _prefabNumber = nextPrefabNumber;
            }
            else if(_prefabNumber == nextPrefabNumber && _prefabNumber < _prefab.Length-1)
            {
                _prefabNumber = ++nextPrefabNumber;
            }
            else
            {
                _prefabNumber = --nextPrefabNumber;
            }
        }

        if (_setups == null)
        {
            _setups = new List<Setup>(8);
        }

        if (_setups.Count >= 2)
        {
            for (int i = 0; i < _setups.Count / 2; i++)
            {
                Destroy(_setups[i].gameObject);
            }

            _setups.RemoveRange(0, _setups.Count / 2);
        }

        _setups.Add(Instantiate(_prefab[_prefabNumber], position, new Quaternion()));
        _camera.backgroundColor = _prefab[_prefabNumber].BackgroundColor;
        _countUsedPrefabs++;
        _setups[0].PlugInLine();
    }

    public void PointsCounter()
    {
        ++Points;
        _points.text = $"{Points}";
    }

    public void LoseGame()
    {
        Time.timeScale = 0.0f;

        if (Points == 0)
        {
            _finalScore.text = "0";
        }
        else
        {
            _finalScore.text = _points.text;
        }

        _mainPanel.SetActive(false);
        _losePanel.SetActive(true);
    }

    public void TryAgain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

}
