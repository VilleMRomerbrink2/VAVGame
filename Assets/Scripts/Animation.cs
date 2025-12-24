using UnityEngine;

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

    SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        InvokeRepeating(nameof(Animate), animationFrameDelay, animationFrameDelay);
    }

    void Animate()
    {
        if (gameObject.tag == ("Running") && runAnimation.Length != 0)
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

        if (gameObject.tag == ("Jumping") && jumpAnimation.Length != 0)
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

        if (gameObject.tag == ("Idle") && idleAnimation.Length != 0)
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
