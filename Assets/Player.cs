using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private float _jumpForce;

   private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void OnJump()
    {
        _rigidbody2D.velocity = Vector2.up * _jumpForce;
    }

    
}
