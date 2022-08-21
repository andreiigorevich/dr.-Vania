using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private Color _targetColor;
    [SerializeField] private Transform _gun;
    [SerializeField] private GameObject _bullet;

    private Vector2 _moveInput;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private CapsuleCollider2D _playerCollider;
    private float _normalGravity;
    private BoxCollider2D _feetCollider;
    private bool _isAlive = true;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] GameObject _bow;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<CapsuleCollider2D>();
        _normalGravity = _rigidBody.gravityScale;
        _feetCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (_isAlive)
        {
            Run();
            FlipSprite();
            ClimbLadde();
            Die();
        }
        //
    }

    private void OnJump(InputValue value)
    {
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;
        if (value.isPressed && _isAlive)
        {
            _rigidBody.velocity += new Vector2(0f, _jumpForce);
        }
    }
    void OnMove(InputValue value)
    {
        if (!_isAlive) { return; }
        _moveInput = value.Get<Vector2>();
        Debug.Log(_moveInput);
    }
    private void Run()
    {
        Vector2 playerVlocity = new Vector2(_moveInput.x * _speed, _rigidBody.velocity.y);
        _rigidBody.velocity = playerVlocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidBody.velocity.x) > Mathf.Epsilon;

        _animator.SetBool("IsRuning", playerHasHorizontalSpeed);
    }

    private void ClimbLadde()
    {
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            _rigidBody.gravityScale = _normalGravity;
            _animator.SetBool("OnLadder", false);
            return;
            
        }   
        
        Vector2 _climbVlocity = new Vector2(_rigidBody.velocity.x, (_moveInput.y * _speed) / 2f);
        _rigidBody.velocity = _climbVlocity;
        _rigidBody.gravityScale = 0;

        bool playerHasVerticalSpeed = Mathf.Abs(_rigidBody.velocity.y) > Mathf.Epsilon;
        _animator.SetBool("OnLadder", playerHasVerticalSpeed);

    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidBody.velocity.x), 1f);
        }
    }

    private IEnumerator OnFire(InputValue value)
    {
        //_bow.GetComponent<AudioSource>().Play();
        _animator.SetTrigger("Shoot");
        
        //if (!_isAlive) { return; }
        yield return new WaitForSecondsRealtime(0.4f);
        
        Instantiate(_bullet, _gun.position, transform.rotation);
        
        
    }
    private void Die()
    {
        if (_playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemys", "Hazards", "Water")))
        {
            _isAlive = false;
            GetComponent<AudioSource>().Play();
            _animator.SetTrigger("Death");
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _targetColor, 0.6f);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
   /* private void ReloadScene()
    {
        SceneManager.LoadScene(_currentSceneIndex);
    }*/

}
