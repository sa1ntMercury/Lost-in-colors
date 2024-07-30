using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsFlying : MonoBehaviour
{
    [SerializeField] private GameObject[] _cloudsSetups;
    [SerializeField] private float xStep;

    private void Start()
    {
        int direction = Random.Range(1, 3);

        switch (direction)
        {
            case 1:
                xStep = -xStep;
                for (int i = 0; i < _cloudsSetups.Length; i++)
                {
                    _cloudsSetups[i].tag = "LeftDirection";
                }
                break;
            case 2:
                for (int i = 0; i < _cloudsSetups.Length; i++)
                {
                    _cloudsSetups[i].tag = "RightDirection";
                }
                break;
            default:
                break;
        }
    }
    
    void Update()
    {
        transform.position = new Vector3 (transform.position.x + xStep, transform.position.y, transform.position.z);
    }
}
