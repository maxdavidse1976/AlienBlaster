using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] float _maxHorizontalSpeed = 5f;

    [Header("Jump settings")]
    [SerializeField] float _jumpVelocity = 5f;
    [SerializeField] float _jumpDuration = 0.5f;

    [Header("Sprites")]
    [SerializeField] Sprite _jumpSprite;

    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _footOffset = 0.5f;
    [SerializeField] float _acceleration = 10;


    public bool IsGrounded;
    
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    AudioSource _audioSource;
    Rigidbody2D _rigidbody;
    
    float _horizontal;
    int _jumpsRemaining;
    float _jumpEndTime;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();

        Vector2 origin = new Vector2(transform.position.x, transform.position.y - capsuleCollider.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        // Draw Left Foot
        origin = new Vector2(transform.position.x - _footOffset, transform.position.y - capsuleCollider.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);

        // Draw Right Foot
        origin = new Vector2(transform.position.x + _footOffset, transform.position.y - capsuleCollider.bounds.extents.y);
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);
    }

    void Update()
    {
        UpdateGrounding();

        var horizontalInput = Input.GetAxis("Horizontal");
        var vertical = _rigidbody.velocity.y;

        if (Input.GetButtonDown("Fire1") && _jumpsRemaining > 0)
        {
            _jumpEndTime = Time.time + _jumpDuration;
            _jumpsRemaining--;

            _audioSource.pitch = _jumpsRemaining > 0 ? 1 : 1.2f;
            _audioSource.Play();
        }

        if (Input.GetButtonDown("Fire1") && _jumpEndTime > Time.time)
            vertical = _jumpVelocity;

        var desiredHorizontal = horizontalInput * _maxHorizontalSpeed;
        _horizontal = Mathf.Lerp(_horizontal, desiredHorizontal, Time.deltaTime * _acceleration);
        _rigidbody.velocity = new Vector2(_horizontal, vertical);
        UpdateSprite();
    }

    void UpdateGrounding()
    {
        IsGrounded = false;
        // Check center
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y);

        var hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);
        if (hit.collider)
            IsGrounded = true;

        // Check left
        origin = new Vector2(transform.position.x - _footOffset, transform.position.y - _spriteRenderer.bounds.extents.y);

        hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);
        if (hit.collider)
            IsGrounded = true;

        // Check right
        origin = new Vector2(transform.position.x + _footOffset, transform.position.y - _spriteRenderer.bounds.extents.y);

        hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, _layerMask);
        if (hit.collider)
            IsGrounded = true;

        if (IsGrounded && GetComponent<Rigidbody2D>().velocity.y  <= 0)
            _jumpsRemaining = 2;
    }

    void UpdateSprite()
    {
        _animator.SetBool("IsGrounded", IsGrounded);
        _animator.SetFloat("HorizontalSpeed", Math.Abs(_horizontal));

        if (_horizontal > 0)
            _spriteRenderer.flipX = false;
        else if (_horizontal < 0)
            _spriteRenderer.flipX = true;
    }
}
