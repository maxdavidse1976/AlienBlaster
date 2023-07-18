using UnityEngine;

public class Frog : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;

    Sprite _defaultSprite;

    [SerializeField] float _jumpDelay = 3f;
    [SerializeField] Vector2 _jumpForce;
    [SerializeField] Sprite _jumpSprite;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        InvokeRepeating("Jump", _jumpDelay, _jumpDelay);
    }

    void Jump()
    {
        _rigidbody.AddForce(_jumpForce);
        _jumpForce *= new Vector2(-1, 1);
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
        _spriteRenderer.sprite = _jumpSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _spriteRenderer.sprite = _defaultSprite;
    }
}
