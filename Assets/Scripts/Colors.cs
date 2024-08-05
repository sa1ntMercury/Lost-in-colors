using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    [SerializeField] private Color _colorGreen;
    [SerializeField] private Color _colorMagenta;
    [SerializeField] private Color _colorBlue;
    [SerializeField] private Color _colorYellow;
    [SerializeField] private Color _colorRed;

    public Color ColorGreen => _colorGreen;
    public Color ColorMagenta => _colorMagenta;
    public Color ColorBlue => _colorBlue;
    public Color ColorYellow => _colorYellow;
    public Color ColorRed => _colorRed;
}
