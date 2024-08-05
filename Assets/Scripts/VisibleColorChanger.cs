using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleColorChanger : MonoBehaviour
{
    [SerializeField] private Color _colorGreen;
    [SerializeField] private Color _colorMagenta;
    [SerializeField] private Color _colorBlue;
    [SerializeField] private Color _colorYellow;
    [SerializeField] private Color _colorRed;

    private SpriteRenderer _spriteRenderer;
    private bool _isSwitch;
    private int _countAvailableColors;


    public int ColorIndex { get; private set; }


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(GetComponentInParent<Setup>().CompareTag("4 Colors"))
        {
            _countAvailableColors = 4;
        }
        else if (GetComponentInParent<Setup>().CompareTag("5 Colors"))
        {
            _countAvailableColors = 5;
        }
    }

    void Update()
    {
        if(!_isSwitch)
        {
            StartCoroutine(ColorSwitcher());
        }
    }

    private IEnumerator ColorSwitcher()
    {
        _isSwitch = true;

        int index = Random.Range(0, _countAvailableColors);

        if(index == ColorIndex && index == 0)
        {
            ColorIndex = ++index;
        }
        else if (index == ColorIndex && index == _countAvailableColors-1)
        {
            ColorIndex = --index;
        }
        else
        {
            ColorIndex = index;
        }

        switch (ColorIndex)
        {
            case 0:
                _spriteRenderer.color = _colorGreen;
                break;
            case 1:
                _spriteRenderer.color = _colorMagenta;
                break;
            case 2:
                _spriteRenderer.color = _colorBlue;
                break;
            case 3:
                _spriteRenderer.color = _colorYellow;
                break;
            case 4:
                _spriteRenderer.color = _colorRed;
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(1f);

        _isSwitch = false;
    }

}
