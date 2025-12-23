using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float jumpAmount = 6;

    bool touchingGround = false;
    public bool running;
    public bool idle;
    public bool jumping;

    Vector2 moveVector;
    LayerMask groundLayer;

    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody2D rB;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        PlayerMovement();
        GroundCheck();
    }

    void Update()
    {
        AnimationParameterCheck();
    }

    void PlayerMovement()
    {
        moveVector = moveAction.ReadValue<Vector2>();

        if (moveVector.x > 0)
        {
            rB.linearVelocityX = moveSpeed;
            spriteRenderer.flipX = false;
        }
        else if (moveVector.x < 0)
        {
            rB.linearVelocityX = -moveSpeed;
            spriteRenderer.flipX = true;
        }
        else
        {
            rB.linearVelocityX = 0;
        }

        if (touchingGround && jumpAction.IsPressed())
        {
            rB.linearVelocityY = jumpAmount;
        }
    }

    void AnimationParameterCheck()
    {
        if (!touchingGround)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
        
        if (touchingGround && moveVector.x == 0)
        {
            idle = true;
        }
        else
        {
            idle = false;
        }
        
        if (touchingGround && moveVector.x != 0)
        {
            running = true;
        }
        else
        {
            running = false;
        }
    }

    void GroundCheck()
    {
        touchingGround = Physics2D.Raycast(transform.position, Vector2.down, 0.65f, groundLayer);
    }
}
