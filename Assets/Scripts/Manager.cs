using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;

    [SerializeField] private TMP_Text _points;
    [SerializeField] private TMP_Text _finalScore;
    [SerializeField] private TMP_Text _pauseScore;
    [SerializeField] private TMP_Text _timerText;

    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _losePanel;

    [SerializeField] private int _yDistanceCircle;
    [SerializeField] private int _yDistanceDoubleCircle;
    

    public int Points { get; private set; }
    public bool IsTimerRun { get; private set; }
    public bool IsNewRun { get; private set; }

    public int DifficultLevel { get; private set; } 
    private int _countUsedPrefabs;
    private DifficultArray[] _difficultArray;
    private Setup[] _difficultPrefabs;
    private List<Setup> _setups;
    private int _prefabNumber;


    private void Awake()
    {
        _difficultArray = GetComponentsInChildren<DifficultArray>();
        AddSetup(new Vector3());
    }

    public void AddSetup(Vector3 position)
    {
        _countUsedPrefabs++;

        if (_countUsedPrefabs > 15)
        {
            DifficultLevel++;
            _countUsedPrefabs = 0;
        }

        if (DifficultLevel >= _difficultArray.Length)
        {
            DifficultLevel = Random.Range(0, _difficultArray.Length);
        }


        _difficultPrefabs = _difficultArray[DifficultLevel]._difficultPrefabs;


        if(_difficultArray[DifficultLevel].CompareTag("4 Colors"))
        {
            _player.CountAvailableColors = 4;
        }
        else if (_difficultArray[DifficultLevel].CompareTag("5 Colors"))
        {
            _player.CountAvailableColors = 5;
        }


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


        if (_setups == null)
        {
            _setups = new List<Setup>(8);
        }
        else if (_setups.Count >= 6)
        {
            for (int i = 0; i < _setups.Count / 3; i++)
            {
                Destroy(_setups[i].gameObject);
            }
            _setups.RemoveRange(0, _setups.Count / 3);
        }


        if(DifficultLevel == 2 || DifficultLevel >= 5)
        {
            position.y += _yDistanceDoubleCircle;
        }
        else
        {
            position.y += _yDistanceCircle;
        }

        _setups.Add(Instantiate(_difficultPrefabs[_prefabNumber], position, new Quaternion()));

        _camera.backgroundColor = _difficultPrefabs[_prefabNumber].BackgroundColor;

        _setups[0].PlugInLine();
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
        SceneManager.LoadScene("MainMenu");
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















































    //[SerializeField] private Player _player;
    //[SerializeField] private Camera _camera;

    //[SerializeField] private List<Setup> _prefab;

    //[SerializeField] private TMP_Text _points;
    //[SerializeField] private TMP_Text _finalScore;
    //[SerializeField] private TMP_Text _pauseScore;
    //[SerializeField] private TMP_Text _timerText;

    //[SerializeField] private GameObject _mainPanel;
    //[SerializeField] private GameObject _pausePanel;
    //[SerializeField] private GameObject _losePanel;

    //private int _countUsedPrefabs;
    //private int _prefabNumber;
    //private List<Setup> _setups;
    //public int Points { get; private set; }
    //public bool IsTimerRun { get; private set; }
    //public bool IsNewRun { get; private set; }



    //private void Awake()
    //{
    //    AddSetup(new Vector3());
    //}

    //public void AddSetup(Vector3 position)
    //{
    //    if (_countUsedPrefabs >= 1)
    //    {
    //        int nextPrefabNumber = Random.Range(0, _prefab.Count);

    //        if(_prefabNumber != nextPrefabNumber)
    //        {
    //            _prefabNumber = nextPrefabNumber;
    //        }
    //        else if(_prefabNumber == nextPrefabNumber && _prefabNumber < _prefab.Count- 1)
    //        {
    //            _prefabNumber = ++nextPrefabNumber;
    //        }
    //        else
    //        {
    //            _prefabNumber = --nextPrefabNumber;
    //        }
    //    }

    //    if (_setups == null)
    //    {
    //        _setups = new List<Setup>(8);
    //    }

    //    if (_setups.Count >= 2)
    //    {
    //        for (int i = 0; i < _setups.Count / 2; i++)
    //        {
    //            Destroy(_setups[i].gameObject);
    //        }

    //        _setups.RemoveRange(0, _setups.Count / 2);
    //    }

    //    _setups.Add(Instantiate(_prefab[_prefabNumber], position, new Quaternion()));
    //    _camera.backgroundColor = _prefab[_prefabNumber].BackgroundColor;
    //    _countUsedPrefabs++;
    //    _setups[0].PlugInLine();
    //}

    //public void PointsCounter()
    //{
    //    ++Points;
    //    _points.text = $"{Points}";
    //}

    //public void LoseGame()
    //{
    //    _player.PausePlayer();

    //    _mainPanel.SetActive(false);
    //    _losePanel.SetActive(true);


    //    if (Points == 0)
    //    {
    //        _finalScore.text = "0";
    //    }
    //    else
    //    {
    //        _finalScore.text = _points.text;
    //    }
    //}

    //public void Pause()
    //{
    //    StopAllCoroutines();

    //    _pauseScore.text = _points.text;

    //    _mainPanel.SetActive(false);
    //    _pausePanel.SetActive(true);

    //    _player.PausePlayer();
    //}

    //public void Resume()
    //{
    //    StartCoroutine(ResumeProcess());
    //}


    //public void TryAgain()
    //{
    //    SceneManager.LoadScene("Game");
    //}

    //public void BackToMainMenu()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //}

    //public IEnumerator ResumeProcess()
    //{
    //    _pausePanel.SetActive(false);
    //    _mainPanel.SetActive(true);

    //    _timerText.gameObject.SetActive(true);
    //    IsTimerRun = true;

    //    for (int i = 3; i > 0; i--)
    //    {
    //        _timerText.text = $"{i}";
    //        yield return new WaitForSeconds(1f);
    //    }

    //    _timerText.gameObject.SetActive(false);
    //    _player.ResumePlayer();
    //    IsTimerRun = false;
    //    IsNewRun = false;

    //}

    //public void HideTimerText()
    //{
    //    _timerText.gameObject.SetActive(false);
    //    IsTimerRun = false;
    //}


}
