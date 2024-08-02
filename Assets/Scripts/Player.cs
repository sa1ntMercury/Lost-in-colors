using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    [SerializeField] private Color _colorGreen;
    [SerializeField] private Color _colorMagenta;
    [SerializeField] private Color _colorBlue;
    [SerializeField] private Color _colorYellow;
    [SerializeField] private Color _colorRed;

    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private AudioSource _pointSound;
    [SerializeField] private AudioSource _loseSound;

    [SerializeField] private Manager _manager;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private string _currentColor;

    public int CountAvailableColors { get; set; }


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetRandomColor();
    }

    public void OnJump()
    {
        if (_manager.IsTimerRun)
        {
            ResumePlayer();
            _manager.HideTimerText();
        }

        _jumpSound.Play();
        _rigidbody2D.velocity = Vector2.up * _jumpForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColorChanger"))
        {
            Vector3 position = collision.GetComponentInParent<Transform>().position;

            _manager.AddSetup(position);

            SetRandomColor();

            Destroy(collision.gameObject);

            return;
        }

        if (collision.CompareTag("VisibleColorChanger"))
        {
            _currentColor = collision.GetComponent<VisibleColorChanger>().CurrentColor;
            SetVisibleChangerColor(_currentColor);
            Destroy(collision.gameObject);

            collision.GetComponentInParent<Rotator>().PlugInColliders();
            return;
        }

        if (collision.CompareTag(_currentColor))
        {
            _pointSound.Play();
            _manager.PointsCounter();
            collision.enabled = false;
        }

        if (!collision.CompareTag(_currentColor))
        {
            _loseSound.Play();
            _manager.LoseGame();
            Points.RecordResult(_manager.Points);
        }
    }

    private void SetRandomColor()
    {
        int index = Random.Range(0, CountAvailableColors);

        switch (index)
        {
            case 0:
                _currentColor = "Green";
                _spriteRenderer.color = _colorGreen;
                break;
            case 1:
                _currentColor = "Magenta";
                _spriteRenderer.color = _colorMagenta;
                break;
            case 2:
                _currentColor = "Blue";
                _spriteRenderer.color = _colorBlue;
                break;
            case 3:
                _currentColor = "Yellow";
                _spriteRenderer.color = _colorYellow;
                break;
            case 4:
                _currentColor = "Red";
                _spriteRenderer.color = _colorRed;
                break;
            default:
                break;
        }
    }

    private void SetVisibleChangerColor(string color)
    {
        switch (color)
        {
            case "Green":
                _spriteRenderer.color = _colorGreen;
                break;
            case "Magenta":
                _spriteRenderer.color = _colorMagenta;
                break;
            case "Blue":
                _spriteRenderer.color = _colorBlue;
                break;
            case "Yellow":
                _spriteRenderer.color = _colorYellow;
                break;
            case "Red":
                _spriteRenderer.color = _colorRed;
                break;
            default:
                break;
        }
    }

    public void PausePlayer()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }


    public void ResumePlayer()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

}