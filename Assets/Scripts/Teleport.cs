using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private float xDistance;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag(gameObject.tag))
        {
            return;
        }
        else
        {
            if (collision.CompareTag("RightDirection"))
            {
                xDistance = -19.2f;
            }
            else
            {
                xDistance = 19.2f;
            }
        }
        collision.transform.position = new Vector3(collision.transform.position.x + xDistance, collision.transform.position.y, collision.transform.position.z);
    }
}
