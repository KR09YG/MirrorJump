using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumppower = 3;
    Rigidbody2D _rb;
    float _index;
    int _jumpCount = 1;
    Animator Animator;
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _index = Input.GetAxisRaw("Horizontal");
        if (_index != 0 )
        {
            Animator.Play("PlayerRunAnimation");
        }
        
        if ( _index == 1 )
        {
            _spriteRenderer.flipX = false;
        }

        else if ( _index == -1 )
        {
            _spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount > 0)
        {
             _rb.velocity = new Vector2(0, _jumppower);
            _jumpCount = 0;
        }
    }
    private void FixedUpdate()
    {
        _rb.velocity = Vector2.right * _speed * _index;
    }
}
