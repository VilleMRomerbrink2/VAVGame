using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpAmount;

    bool touchingGround;
    bool alive = true;
    bool isEnemy;
    bool isPlayer;

    Vector2 moveVector;
    LayerMask groundLayer;

    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody2D rB;
    SpriteRenderer spriteRenderer;
    GameObject player;

    void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
        rB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isPlayer = true;
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            isEnemy = true;
        }

        if (isPlayer)
        {
            moveAction = InputSystem.actions.FindAction("Move");
            jumpAction = InputSystem.actions.FindAction("Jump");
        }
        else if (isEnemy)
        {
            player = GameObject.Find("Player");
        }
    }

    void FixedUpdate()
    {
        if (alive)
        {
            GroundCheck();

            if (isPlayer)
            {
                PlayerMovement();
            }
            else if (isEnemy)
            {
                EnemyMovement();
            }
        }
    }

    void Update()
    {
        if (alive)
        {
            AnimationParameterCheck();
        }
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

    void EnemyMovement()
    {
        if (transform.position.x - player.transform.position.x > 0)
        {
            spriteRenderer.flipX = true;
            rB.linearVelocityX = -moveSpeed;
        }
        else if (transform.position.x - player.transform.position.x < 0)
        {
            spriteRenderer.flipX = false;
            rB.linearVelocityX = moveSpeed;
        }
        else
        {
            rB.linearVelocityX = 0;
        }
    }

    void AnimationParameterCheck()
    {
        if (isPlayer)
        {
            if (!touchingGround)
            {
                gameObject.tag = ("Jumping");
            }
            else if (touchingGround && moveVector.x == 0)
            {
                gameObject.tag = ("Idle");
            }
            else if (touchingGround && moveVector.x != 0)
            {
                gameObject.tag = ("Running");
            }
            else
            {
                gameObject.tag = gameObject.name;
            }
        }
        else if (isEnemy)
        {
            if (!touchingGround)
            {
                gameObject.tag = ("Jumping");
            }
            else if (touchingGround && rB.linearVelocityX == 0)
            {
                gameObject.tag = ("Idle");
            }
            else if (touchingGround && rB.linearVelocityX != 0)
            {
                gameObject.tag = ("Running");
            }
            else
            {
                gameObject.tag = gameObject.name;
            }
        }
    }

    void GroundCheck()
    {
        touchingGround = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayer);
    }
}
