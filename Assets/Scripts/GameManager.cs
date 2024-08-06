using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private LevelLoader _leverLoader;

    [Header("TEXT")]
    [SerializeField] private TMP_Text _points;
    [SerializeField] private TMP_Text _finalScore;
    [SerializeField] private TMP_Text _pauseScore;
    [SerializeField] private TMP_Text _timerText;

    [Header("PANELS")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _losePanel;

    [Header("NEW LEVELS DISTANCE")]
    [SerializeField] private int _yDistanceCircle;
    [SerializeField] private int _yDistanceDoubleCircle;

    [Header("LEVELS CONTROL")]
    [SerializeField] private int _maxActivePrefabs;
    [SerializeField] private int _levelsOnDifficultLevel;

    private DifficultArray[] _difficultArray;
    private Setup[] _difficultPrefabs;
    private List<Setup> _setups;

    private int _countUsedPrefabs;
    private int _prefabNumber;

    public int DifficultLevel { get; private set; }
    public int Points { get; private set; }
    public bool IsTimerRun { get; private set; }
    public bool IsNewRun { get; private set; }


    private void Awake()
    {
        _difficultArray = GetComponentsInChildren<DifficultArray>();
        AddSetup(new Vector3());
    }

    public void AddSetup(Vector3 position)
    {
        LevelManager();

        //Active prefab controller
        if (_setups == null)
        {
            _setups = new List<Setup>(_maxActivePrefabs + 1);
        }
        else if (_setups.Count >= _maxActivePrefabs)
        {
            for (int i = 0; i < _setups.Count / 2; i++)
            {
                Destroy(_setups[i].gameObject);
            }
            _setups.RemoveRange(0, _setups.Count / 2);
        }

        //Install Y-position of new mini-level
        if (DifficultLevel == 2 || DifficultLevel >= 5) //Levels with double circle [2, 5, 6, 7]
        {
            position.y += _yDistanceDoubleCircle;
        }
        else
        {
            position.y += _yDistanceCircle;
        }

        _setups.Add(Instantiate(_difficultPrefabs[_prefabNumber], position, new Quaternion()));

        _setups[0].PlugInLine();
    }

    private void LevelManager()
    {

        //Difficult level switch
        _countUsedPrefabs++;

        if (_countUsedPrefabs > _levelsOnDifficultLevel)
        {
            DifficultLevel++;
            _countUsedPrefabs = 0;
        }

        if (DifficultLevel >= _difficultArray.Length)
        {
            DifficultLevel = Random.Range(0, _difficultArray.Length);
        }

        _difficultPrefabs = _difficultArray[DifficultLevel]._difficultPrefabs;

        //Install count available colors to player
        if (_difficultArray[DifficultLevel].CompareTag("4 Colors"))
        {
            _player.CountAvailableColors = 4;
        }
        else if (_difficultArray[DifficultLevel].CompareTag("5 Colors"))
        {
            _player.CountAvailableColors = 5;
        }

        //Color level switch
        int nextPrefabNumber = Random.Range(0, _difficultPrefabs.Length);

        if (_prefabNumber != nextPrefabNumber)
        {
            _prefabNumber = nextPrefabNumber;
        }
        else if (_prefabNumber == nextPrefabNumber && _prefabNumber < _difficultPrefabs.Length - 1)
        {
            _prefabNumber = ++nextPrefabNumber;
        }
        else
        {
            _prefabNumber = --nextPrefabNumber;
        }

        _camera.backgroundColor = _difficultPrefabs[_prefabNumber].BackgroundColor;
    }

    public void PointsCounter()
    {
        ++Points;
        _points.text = $"{Points}";
    }

    public void LoseGame()
    {
        _player.PausePlayer();

        _mainPanel.SetActive(false);
        _losePanel.SetActive(true);


        if (Points == 0)
        {
            _finalScore.text = "0";
        }
        else
        {
            _finalScore.text = _points.text;
        }
    }

    public void Pause()
    {
        StopAllCoroutines();

        _pauseScore.text = _points.text;

        _mainPanel.SetActive(false);
        _pausePanel.SetActive(true);

        _player.PausePlayer();
    }

    public void Resume()
    {
        StartCoroutine(ResumeProcess());
    }


    public void TryAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void BackToMainMenu()
    {
        StartCoroutine(_leverLoader.LoadLevel("MainMenu"));
    }

    public IEnumerator ResumeProcess()
    {
        _pausePanel.SetActive(false);
        _mainPanel.SetActive(true);

        _timerText.gameObject.SetActive(true);
        IsTimerRun = true;

        for (int i = 3; i > 0; i--)
        {
            _timerText.text = $"{i}";
            yield return new WaitForSeconds(1f);
        }

        _timerText.gameObject.SetActive(false);
        _player.ResumePlayer();
        IsTimerRun = false;
        IsNewRun = false;
    }

    public void HideTimerText()
    {
        _timerText.gameObject.SetActive(false);
        IsTimerRun = false;
    }


}
