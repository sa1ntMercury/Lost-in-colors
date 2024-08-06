using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private float _xDistanceTeleport = 19.2f;

    private void Start()
    {
        if(CompareTag("RightDirection"))
        {
            _xDistanceTeleport = -_xDistanceTeleport;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag(gameObject.tag))
        {
            return;
        }
       
        collision.transform.position = new Vector3(collision.transform.position.x + _xDistanceTeleport, collision.transform.position.y, collision.transform.position.z);
    }
}
