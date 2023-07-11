using UnityEngine;

public class Player : MonoBehaviour
{

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
            vertical = 5;
        }

        rigidbody.velocity = new Vector2(horizontal, vertical);
    }
}
