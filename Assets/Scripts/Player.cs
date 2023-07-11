using UnityEngine;

public class Player : MonoBehaviour
{
    private float _jumpEndTime;

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
            _jumpEndTime = Time.time + 0.5f;
        }
        if (Input.GetButtonDown("Fire1") && _jumpEndTime > Time.time)
        {
            vertical = 5;
        }

        rigidbody.velocity = new Vector2(horizontal, vertical);
    }
}
