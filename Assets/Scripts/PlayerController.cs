using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float jumpAmount = 20f;

    Vector2 moveVector;
    bool touchingGround = false;

    InputAction move;
    InputAction jump;
    Rigidbody2D rb2D;

    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        moveVector = move.ReadValue<Vector2>();

        MovePlayer();
        Jump();
        CheckForGround();
    }



    void MovePlayer()
    {
        if (moveVector.x < 0)
        {
            rb2D.AddForceX(-moveSpeed); 
        }
        else if (moveVector.x > 0)
        {
            rb2D.AddForceX(moveSpeed);
        }
    }

    void Jump()
    {
        if (touchingGround && jump.IsPressed())
        {
            rb2D.linearVelocityY = jumpAmount;
        }
    }

    void CheckForGround()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        touchingGround = Physics2D.Raycast(transform.position, Vector2.down, 1, groundLayer);
    }
}
