using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] Transform foot;

    PlayerInput input;
    InputAction moveInput;
    InputAction jumpInput;

    Rigidbody rb;
    Vector3 moveDirection;
    float distanceToGround;
    bool isGrounded;
    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        moveInput = input.actions["Move"];
        jumpInput = input.actions["Jump"];
        cam = Camera.main;

        distanceToGround = transform.position.y - foot.position.y + 0.1f;
    }

    void Update()
    {
        isGrounded = CheckGrounded();
        Vector2 moveInputValue = moveInput.ReadValue<Vector2>();
        Vector3 camForward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        Vector3 verticalValue = moveInputValue.y * camForward;
        Vector3 holizontalValue = moveInputValue.x * cam.transform.right;
        moveDirection = verticalValue + holizontalValue;

        if (moveDirection != Vector3.zero)
        {
            Rotate();
        }

        if (isGrounded && jumpInput.WasPressedThisFrame())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    // 移動
    void Move()
    {
        float currentSpeed = rb.velocity.magnitude;
        if (currentSpeed > maxSpeed) return;

        rb.AddForce(moveDirection * moveSpeed, ForceMode.VelocityChange);
    }

    // 回転
    void Rotate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // ジャンプ
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
    }

    // 接地判定
    bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }
}