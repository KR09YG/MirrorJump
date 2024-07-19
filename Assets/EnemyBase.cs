using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    /// <summary>壁を検出するための line のオフセット</summary>
    [SerializeField] Vector2 _lineForWall = Vector2.right;

    /// <summary>壁のレイヤー（レイヤーはオブジェクトに設定されている）</summary>
    [SerializeField] LayerMask _wallLayer = 0;
    /// <summary>床を検出するための line のオフセット</summary>
    [SerializeField] Vector2 _lineForGround = new Vector2(1f, -1f);

    /// <summary>床のレイヤー</summary>
    [SerializeField] LayerMask _groundLayer = 0;
    /// <summary>移動方向</summary>
    Vector2 _moveDirection = Vector2.right;
    Rigidbody2D _rb = default;
    SpriteRenderer _spriteRenderer;
    int _flip =1;

    public abstract void Activate();
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoveOnFloor();
        if ( _flip == 1 )
        {
            _spriteRenderer.flipX = true;
        }
        else if ( _flip == -1 )
        {
            _spriteRenderer.flipX = false;
        }
    }

    void MoveOnFloor()
    {
        Vector2 start = this.transform.position;
        Debug.DrawLine(start, start + _lineForWall);
        Debug.DrawLine(start, start + _lineForGround);
        RaycastHit2D hit = Physics2D.Linecast(start, start + _lineForGround, _groundLayer);
        RaycastHit2D hit1 = Physics2D.Linecast(start, start + _lineForWall, _wallLayer);
        Vector2 velo = Vector2.zero;
        if (!hit.collider)
        {
            _moveDirection = _moveDirection * -1;
            _lineForGround.x *= -1;
            _lineForWall.x *= -1;
            _flip *= -1;
        }
        else if (hit1.collider)
        {
            _lineForWall = _lineForWall * -1;
            _moveDirection = _moveDirection * -1;
            _lineForGround.x *= -1;
            _flip *= -1;
        }
        velo = _moveDirection.normalized * _moveSpeed;
        velo.y = _rb.velocity.y;    // 落下については現在の値を保持する
        _rb.velocity = velo;
    }
}
