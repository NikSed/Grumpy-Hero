using System;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float JumpForce = 3f;
    public bool IsControllsInverse = true;
    public bool IsControllsBlock = false;
    public bool IsGrounded = false;

    public float GroundRadius = 0.3f;
    public LayerMask GroundLayer;
    public Transform GroundCheck;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, GroundLayer);

        if (Input.GetKeyDown(KeyCode.W) && !IsControllsInverse)
            Jump();

        if (Input.GetKeyDown(KeyCode.S) && IsControllsInverse)
            Jump();

        if (_rigidbody2D.velocity.y < 0)
            _rigidbody2D.gravityScale = 3f;
        else
            if (_rigidbody2D.gravityScale != 1f)
            _rigidbody2D.gravityScale = 1f;

        Move(inputX);
    }

    private void Jump()
    {
        if (IsGrounded)
            _rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void Move(float x)
    {
        if (IsControllsInverse)
            x = -x;

        transform.position += (MoveSpeed * x * Time.deltaTime * Vector3.right);
    }
}
