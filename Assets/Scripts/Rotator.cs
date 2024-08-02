using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float Speed { get; set; }

    private void Start()
    {
        float circleScale = Random.Range(0.5f, 1.05f);
        transform.localScale = new Vector3(circleScale, circleScale, circleScale);

        switch (Random.Range(1, 3))
        {
            case 1:
                Speed = Random.Range(90, 160);
                break;
            case 2:
                Speed = Random.Range(-160, -90);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, Speed * Time.deltaTime);
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
