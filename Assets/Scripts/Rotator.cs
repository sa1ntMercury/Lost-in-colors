using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float _speed;

    private readonly float MinScale = 0.5f;
    private readonly float MaxScale = 1.05f;

    private readonly float MinSpeed = 90;
    private readonly float MaxSpeed = 160;


    private void Start()
    {
        float circleScale = Random.Range(MinScale, MaxScale);
        transform.localScale = new Vector3(circleScale, circleScale, circleScale);

        switch (Random.Range(1, 3))
        {
            case 1:
                _speed = Random.Range(MinSpeed, MaxSpeed);
                break;
            case 2:
                _speed = -Random.Range(MinSpeed, MaxSpeed);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, _speed * Time.deltaTime);
    }

    public void PlugInColliders()
    {
        PolygonCollider2D[] _colliders = GetComponents<PolygonCollider2D>();

        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = true;
        }
    }


}
