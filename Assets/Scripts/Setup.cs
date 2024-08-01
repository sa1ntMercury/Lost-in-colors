using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] private Color _backgroundColor;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _clouds;
    [SerializeField] private LimitLine _limitLine;


    public Color BackgroundColor { get => _backgroundColor; }

    // Start is called before the first frame update
    void Start()
    {
        float circleScale = Random.Range(0.7f, 1.55f);
        _circle.transform.localScale = new Vector3(circleScale, circleScale, circleScale);

        float cloudsXPosition = Random.Range(-4f, 4f);
        Vector3 _oldCloudsPosition = _clouds.transform.position;
        _clouds.transform.position = new Vector3(cloudsXPosition, _oldCloudsPosition.y, _oldCloudsPosition.z);

        int _switcher = Random.Range(1, 3);
        switch (_switcher)
        {
            case 1:
                _circle.GetComponent<Rotator>().Speed = Random.Range(90, 160);
                break;
            case 2:
                _circle.GetComponent<Rotator>().Speed = Random.Range(-160, -90);
                break;
            default:
                break;
        }
      
    }

    public void PlugInLine()
    {
        _limitLine.TurnOnLine();
    }


}
