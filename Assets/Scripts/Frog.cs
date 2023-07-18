using UnityEngine;

public class Frog : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;

    Sprite _defaultSprite;
    int _jumpsRemaining;

    [SerializeField] int _jumps = 2;
    [SerializeField] float _jumpDelay = 3f;
    [SerializeField] Vector2 _jumpForce;
    [SerializeField] Sprite _jumpSprite;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        _jumpsRemaining = _jumps;
        InvokeRepeating("Jump", _jumpDelay, _jumpDelay);
    }

    void Jump()
    {
        if (_jumpsRemaining <= 0)
        {
            _jumpForce *= new Vector2(-1, 1);
            _jumpsRemaining = _jumps;
        }
        _jumpsRemaining--;

        _rigidbody.AddForce(_jumpForce);
        _spriteRenderer.flipX = _jumpForce.x > 0;
        _spriteRenderer.sprite = _jumpSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _spriteRenderer.sprite = _defaultSprite;
    }
}
