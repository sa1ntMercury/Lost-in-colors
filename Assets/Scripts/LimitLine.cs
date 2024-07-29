using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLine : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TurnOnLine()
    {
        _spriteRenderer.enabled = true;
        _boxCollider2D.enabled = true;
    }

}
