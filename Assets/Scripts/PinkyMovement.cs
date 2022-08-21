using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    private Rigidbody2D _pinkyRigidbody;
    void Start()
    {
        _pinkyRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _pinkyRigidbody.velocity = new Vector2(_moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _moveSpeed = -_moveSpeed;
        FlipPinky();
        //
    }

    private void FlipPinky()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_pinkyRigidbody.velocity.x)), 1f);
    }

}
