using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] float _horizontalVelocity = 5f;

    [Header("Jump settings")]
    [SerializeField] float _jumpVelocity = 5f;
    [SerializeField] float _jumpDuration = 0.5f;
    [SerializeField] float _jumpEndTime = 0.5f;

    [Header("Sprites")]
    [SerializeField] Sprite _jumpSprite;

    [SerializeField] LayerMask _layerMask;

    Sprite _defaultSprite;
    SpriteRenderer _spriteRenderer;
    Animator _animator;

    public bool IsGrounded;
    float _horizontal;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _defaultSprite = _spriteRenderer.sprite;    
    }

    void Update()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y);

        var hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);
        if (hit.collider)
            IsGrounded = true;
        else
            IsGrounded = false;

        _horizontal = Input.GetAxis("Horizontal");
        var rigidbody = GetComponent<Rigidbody2D>();
        var vertical = rigidbody.velocity.y;

        if (Input.GetButtonDown("Fire1") && IsGrounded)
            _jumpEndTime = Time.time + _jumpDuration;
        if (Input.GetButtonDown("Fire1") && _jumpEndTime > Time.time)
            vertical = _jumpVelocity;
        
        _horizontal *= _horizontalVelocity;
        rigidbody.velocity = new Vector2(_horizontal, vertical);
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        _animator.SetBool("IsGrounded", IsGrounded);
        _animator.SetFloat("HorizontalSpeed", Math.Abs(_horizontal));
        if (_horizontal > 0)
            _spriteRenderer.flipX = false;
        else if (_horizontal < 0)
            _spriteRenderer.flipX = true;
    }

    void OnDrawGizmos()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);
    }
}
