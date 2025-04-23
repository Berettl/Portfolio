using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 2f;
    public int Damage = 1;

    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public float playerCheckRadius = 5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public float wallCheckDistance = 0.5f;

    private bool movingRight = true;
    private Rigidbody2D RB;

    //Ranged Enemy
    public bool Ranged;
    public Transform projectileSpawn;
    public GameObject Projectile;
    public float arcHeight = 2f;

    private Transform player;

    public float timeBtwShots = 1;  //Time between shots
    private float timeOfLastShot;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        RB.velocity = new Vector2((movingRight ? 1 : -1) * Speed, RB.velocity.y);

        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        bool isBlocked = IsWallInFront();

        bool isAttack = Physics2D.OverlapCircle(transform.position, playerCheckRadius, whatIsPlayer);

        if (!isGrounded || isBlocked)
        {
            Flip();
        }

        if (isAttack && Ranged)
        {
            Attack();

        }
        else 
        {
            Speed = 2f;
        }
    }

    //Attack Player
    private void Attack()
    {
        Speed = 0;
        
        if (player == null) return;

        
        //Calculate direction and launch velocity
        Vector3 targetPosition = player.position;
        Vector3 startPosition = projectileSpawn.position;

        if(targetPosition.x < gameObject.transform.position.x)
        {
            movingRight = false;
        }
        else if(targetPosition.x > gameObject.transform.position.x)
        {
            movingRight = true;
        }

        //Adjust for height difference
        float distance = Vector3.Distance(new Vector3(targetPosition.x, 0 , targetPosition.z), new Vector3(startPosition.x, 0, startPosition.z));
        float yDifference = targetPosition.y - startPosition.y;

        // Calculate velocity needed to reach the target with an arc
        float verticalVelocity = Mathf.Sqrt(2 * Physics.gravity.magnitude * arcHeight);
        float timeToApex = verticalVelocity / Physics.gravity.magnitude;
        float totalTime = 2 * timeToApex;
        float horizontalSpeed = distance / totalTime;

        Vector3 horizontalDirection = (targetPosition - startPosition).normalized;
        horizontalDirection.y = 0;

        Vector3 launchVelocity = horizontalDirection * horizontalSpeed + Vector3.up * verticalVelocity;

        //Spawn and launch projectile
        if (Time.time - timeOfLastShot >= timeBtwShots) //If the time elapsed is more than the fire rate, allow a shot
        {
            GameObject projectile = Instantiate(Projectile, projectileSpawn.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            StartCoroutine(DestroyProjectileAfterTime(projectile, 3f)); timeOfLastShot = Time.time;   //set new time of last shot

            if (rb != null)
            {
                rb.velocity = launchVelocity;
            }
        }
    }

    private IEnumerator DestroyProjectileAfterTime(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(projectile);
    }

    //Check for Wall in front of enemy
    private bool IsWallInFront()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wallCheckDistance, whatIsGround);

        Debug.DrawRay(transform.position, direction * wallCheckDistance, Color.blue);

        return hit.collider != null;
    }

    //Flip the X Rotation 
    private void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //Check for player Collision
    void OnTriggerEnter2D(Collider2D other)
    {
        HealthController Health = other.GetComponent<HealthController>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player");

            Health.Damage(Damage);
        }
    }

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }

 

    //Visualize Radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);
    }
}
