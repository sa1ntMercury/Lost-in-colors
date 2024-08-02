using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] private Color _backgroundColor;
    [SerializeField] private GameObject _clouds;
    [SerializeField] private LimitLine _limitLine;


    public Color BackgroundColor { get => _backgroundColor; }


    // Start is called before the first frame update
    void Start()
    {
        float cloudsXPosition = Random.Range(-4f, 4f);
        Vector3 _oldCloudsPosition = _clouds.transform.position;
        _clouds.transform.position = new Vector3(cloudsXPosition, _oldCloudsPosition.y, _oldCloudsPosition.z);
    }


    public void PlugInLine()
    {
        _limitLine.TurnOnLine();
    }


}
