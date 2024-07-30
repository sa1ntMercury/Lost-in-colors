using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    [SerializeField] private Color _colorBlue;
    [SerializeField] private Color _colorMagenta;
    [SerializeField] private Color _colorRed;
    [SerializeField] private Color _colorGreen;

    [SerializeField] private Manager _manager;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private string _currentColor;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetRandomColor();
    }

    public void OnJump()
    {
        _rigidbody2D.velocity = Vector2.up * _jumpForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColorChanger"))
        {
            SetRandomColor();
            Destroy(collision.gameObject);

            Vector3 position = collision.GetComponentInParent<Transform>().position;

            position.y += 4;

            _manager.AddSetup(position);

            return;
        }

        if (collision.CompareTag(_currentColor))
        {
            _manager.PointsCounter();
            collision.enabled = false;
        }

        if (!collision.CompareTag(_currentColor))
        {
            _manager.LoseGame();
            Points.RecordResult(_manager.Points);
        }
    }

    private void SetRandomColor()
    {
        int index = Random.Range(0, 4);

        switch (index)
        {
            case 0:
                _currentColor = "Blue";
                _spriteRenderer.color = _colorBlue;
                break;
            case 1:
                _currentColor = "Magenta";
                _spriteRenderer.color = _colorMagenta;
                break;
            case 2:
                _currentColor = "Red";
                _spriteRenderer.color = _colorRed;
                break;
            case 3:
                _currentColor = "Green";
                _spriteRenderer.color = _colorGreen;
                break;
            default:
                break;
        }
    }

    
}
