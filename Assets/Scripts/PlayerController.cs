using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;

    Vector2 moveVector;

    InputAction move;
    Rigidbody2D rb2D;

    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        moveVector = move.ReadValue<Vector2>();
    } 



    void MovePlayer()
    {
        if ( moveVector.x < 0)
        {
            rb2D.linearVelocityX = moveSpeed * Time.deltaTime;
        }
    }
}
