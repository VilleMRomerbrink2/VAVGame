using Unity.VisualScripting;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileHeight;
    [SerializeField] float reloadTime;
    public bool isProjectile;

    Rigidbody2D rB;
    SpriteRenderer spriteRenderer;
    public GameObject projectile;
    GameObject player;
    GameObject enemy;
    MovementController mController;

    void Awake()
    {
        rB = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        mController = enemy.GetComponent<MovementController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (isProjectile && player.transform.position.x > transform.position.x)
        {
            rB.linearVelocityX = projectileSpeed;
            rB.linearVelocityY = projectileHeight;
        }
        else if (isProjectile && player.transform.position.x < transform.position.x)
        {
            rB.linearVelocityX = -projectileSpeed;
            rB.linearVelocityY = projectileHeight;
            spriteRenderer.flipX = true;

        }
        InvokeRepeating("ThrowProjectile", reloadTime, reloadTime);
    }

    void FixedUpdate()
    {
        if (isProjectile && transform.position.y < player.transform.position.y - 15)
        {
            Destroy(gameObject);
        }
    }

    void ThrowProjectile()
    {
        if (!isProjectile)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
    }
}
