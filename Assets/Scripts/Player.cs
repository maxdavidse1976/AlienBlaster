using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpVelocity = 5f;
    [SerializeField] private float _jumpDuration = 0.5f;
    [SerializeField] private float _jumpEndTime = 0.5f;

    public bool IsGrounded;

    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - spriteRenderer.bounds.extents.y);

        var hit = Physics2D.Raycast(origin, Vector2.down, 0.1f);
        if (hit.collider)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }

        var horizontal = Input.GetAxis("Horizontal");
        var rigidbody = GetComponent<Rigidbody2D>();
        var vertical = rigidbody.velocity.y;

        if (Input.GetButtonDown("Fire1") && IsGrounded)
        {
            _jumpEndTime = Time.time + _jumpDuration;
        }
        if (Input.GetButtonDown("Fire1") && _jumpEndTime > Time.time)
        {
            vertical = _jumpVelocity;
        }

        rigidbody.velocity = new Vector2(horizontal, vertical);
    }

    private void OnDrawGizmos()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - spriteRenderer.bounds.extents.y);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);
    }
}
