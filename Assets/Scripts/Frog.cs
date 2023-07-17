using UnityEngine;

public class Frog : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    
    [SerializeField] float _jumpDelay = 3f;
    [SerializeField] Vector2 _jumpForce; 

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("Jump", _jumpDelay, _jumpDelay);
    }

    void Jump()
    {
        _rigidbody.AddForce(_jumpForce);
    }
}
