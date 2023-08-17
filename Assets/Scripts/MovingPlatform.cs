using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 _position1;
    [SerializeField] Vector3 _position2;
    [Range(0f, 1f)]
    [SerializeField] float _percentAcross;
    [SerializeField] float _speed = 1f;

    [ContextMenu(nameof(SetPosition1))] public void SetPosition1() => _position1 = transform.position;
    [ContextMenu(nameof(SetPosition2))] public void SetPosition2() => _position2 = transform.position;

    void Update()
    {
        _percentAcross = Mathf.PingPong(Time.time * _speed, 1f);
        transform.position = Vector3.Lerp(_position1, _position2, _percentAcross);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var collider = GetComponent<BoxCollider2D>();
        Gizmos.DrawWireCube(_position1, collider.bounds.size);
        Gizmos.DrawWireCube(_position2, collider.bounds.size);

        Gizmos.color = Color.yellow;
        var currentPosition = Vector3.Lerp(_position1, _position2, _percentAcross);
        Gizmos.DrawWireCube(currentPosition, collider.bounds.size);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.transform.SetParent(null);
        }
    }
}
