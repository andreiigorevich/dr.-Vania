using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float _arrowSpeed = 5f;

    private Rigidbody2D _arrowRigidbody;
    private PlayerMovement _player;
    private float _xSpeed;
    private SpriteRenderer _spriteArrow;
    
    void Start()
    {
        _arrowRigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();
        _xSpeed = _player.transform.localScale.x * _arrowSpeed;
    }

    void Update()
    {
        _arrowRigidbody.velocity = new Vector2(_xSpeed, 0f);
        FlipSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(_player.transform.localScale.x), 1f);
    }
}
