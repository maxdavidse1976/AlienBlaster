using TMPro;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] Sprite _sprung;
    SpriteRenderer _spriteRenderer;
    Sprite _defaultSprite;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            _spriteRenderer.sprite = _sprung;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _spriteRenderer.sprite = _defaultSprite;
    }
}
