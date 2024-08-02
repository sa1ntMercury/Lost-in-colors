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

    public string CurrentColor { get; private set; }

    private SpriteRenderer _spriteRenderer;
    private bool _isSwitch;
    private int _countAvailableColors;
    private int _colorIndex;


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
        if(index == _colorIndex && index == 0)
        {
            _colorIndex = ++index;
        }
        else if (index == _colorIndex && index == 4)
        {
            _colorIndex = --index;
        }

            switch (_colorIndex)
        {
            case 0:
                CurrentColor = "Green";
                _spriteRenderer.color = _colorGreen;
                break;
            case 1:
                CurrentColor = "Magenta";
                _spriteRenderer.color = _colorMagenta;
                break;
            case 2:
                CurrentColor = "Blue";
                _spriteRenderer.color = _colorBlue;
                break;
            case 3:
                CurrentColor = "Yellow";
                _spriteRenderer.color = _colorYellow;
                break;
            case 4:
                CurrentColor = "Red";
                _spriteRenderer.color = _colorRed;
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(1f);

        _isSwitch = false;
    }

}
