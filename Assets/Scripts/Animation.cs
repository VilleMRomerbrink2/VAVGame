using UnityEngine;
using UnityEngine.Rendering;

public class Animation : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] float animationFrameDelay = 0.2f;

    public Sprite[] runAnimation;
    int currentRunSprite;

    public Sprite[] idleAnimation;
    int currentIdleSprite;

    public Sprite[] jumpAnimation;
    int currentJumpSprite;

    PlayerController playerMovement;
    SpriteRenderer spriteRenderer;


    void Awake()
    {
        playerMovement = GetComponent<PlayerController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        InvokeRepeating(nameof(Animate), animationFrameDelay, animationFrameDelay);
    }

    void Animate()
    {
        if (playerMovement.running)
        {
            currentRunSprite++;
            if (currentRunSprite >= runAnimation.Length)
            {
                currentRunSprite = 0;
            }

            spriteRenderer.sprite = runAnimation[currentRunSprite];
        }
        else
        {
            currentRunSprite = 0;
        }

        if (playerMovement.jumping)
        {
            currentJumpSprite++;
            if (currentJumpSprite >= jumpAnimation.Length)
            {
                currentJumpSprite--;
            }

            spriteRenderer.sprite = jumpAnimation[currentJumpSprite];
        }
        else
        {
            currentJumpSprite = 0;
        }

        if (playerMovement.idle)
        {
            currentIdleSprite++;
            if (currentIdleSprite >= idleAnimation.Length)
            {
                currentIdleSprite = 0;
            }

            spriteRenderer.sprite = idleAnimation[currentIdleSprite];
        }
        else
        {
            currentIdleSprite = 0;
        }
    }
}
