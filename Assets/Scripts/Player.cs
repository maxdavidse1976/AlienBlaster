using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpVelocity = 5f;
    [SerializeField] private float _jumpDuration = 0.5f;
    [SerializeField] private float _jumpEndTime = 0.5f;


    void Start()
    {
        
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var rigidbody = GetComponent<Rigidbody2D>();
        var vertical = rigidbody.velocity.y;

        if (Input.GetButtonDown("Fire1"))
        {
            _jumpEndTime = Time.time + _jumpDuration;
        }
        if (Input.GetButtonDown("Fire1") && _jumpEndTime > Time.time)
        {
            vertical = _jumpVelocity;
        }

        rigidbody.velocity = new Vector2(horizontal, vertical);
    }
}
