using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Movement:")]
    [SerializeField]
    private Rigidbody2D m_characterRigidBody;

    [SerializeField]
    private string m_groundTag = "Ground";
    [SerializeField]
    private Collider2D m_characterCollider;
    [SerializeField]
    private float m_verticalCastDistance = 0.025f;
    [SerializeField]
    private float m_horizontalCastDistance = 0.025f;

    [SerializeField]
    private float m_horizontalSpeed = 2f;
    [SerializeField]
    private float m_verticalSpeed = 2f;

    [Header("Combat:")]
    [SerializeField]
    private WandContainer m_wandContainer;

    [Header("Visuals:")]
    [SerializeField]
    private SpriteRenderer m_characterSpriteRenderer;
    [SerializeField]
    private SpriteRenderer m_characterAttackRenderer;
    [SerializeField]
    private Animator m_characterAnimator;

    [SerializeField]
    private bool m_spriteFlipped = false;

    [SerializeField]
    private string m_isRunningParamName = "IsRunning";
    [SerializeField]
    private string m_isGroundedParamName = "IsGrounded";
    [SerializeField]
    private string m_triggerJumpParamName = "Jump";
    [SerializeField]
    private string m_idOfWandParamName = "WeaponId";
    [SerializeField]
    private string m_triggerAttackParamName = "Attack";

    // Animator Paramater Hash IDs
    private int m_animIsRunningHash;
    private int m_animIsGroundedHash;
    private int m_animTriggerJumpHash;
    private int m_animIdOfWandHash;
    private int m_animTriggerAttackHash;

    // Start is called before the first frame update
    void Start()
    {
        if (m_characterRigidBody == null)
        {
            m_characterRigidBody = GetComponent<Rigidbody2D>();
        }

        if (m_characterCollider == null)
        {
            m_characterCollider = GetComponent<Collider2D>();
        }

        if (m_characterSpriteRenderer == null)
        {
            m_characterSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (m_characterAttackRenderer == null)
        {
            m_characterAttackRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (m_characterAnimator == null)
        {
            m_characterAnimator = GetComponent<Animator>();
        }

        if (m_characterRigidBody == null)
        {
            throw new MissingComponentException("Missing: RigidBody2D");
        }

        if (m_characterCollider == null)
        {
            throw new MissingComponentException("Missing: Collider2D");
        }

        if (m_characterSpriteRenderer == null)
        {
            throw new MissingComponentException("Missing: SpriteRenderer");
        }

        if (m_characterAttackRenderer == null)
        {
            throw new MissingComponentException("Missing: SpriteRenderer");
        }

        if (m_characterAnimator == null)
        {
            throw new MissingComponentException("Missing: Animator");
        }

        m_animIsRunningHash     = Animator.StringToHash(m_isRunningParamName);
        m_animIsGroundedHash    = Animator.StringToHash(m_isGroundedParamName);
        m_animTriggerJumpHash   = Animator.StringToHash(m_triggerJumpParamName);
        m_animIdOfWandHash      = Animator.StringToHash(m_idOfWandParamName);
        m_animTriggerAttackHash = Animator.StringToHash(m_triggerAttackParamName);

        m_wandContainer.SetWandID(CheckPointRegistry.GetWandId());

        m_characterAnimator.SetInteger(m_animIdOfWandHash, m_wandContainer.GetCurrentWandID());

        CheckPointRegistry.SpawnAtCheckPoint(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        UpdateDirection();

        float horizontalVelocity = UpdateHorizontal();
        float verticalVelocity = 0;

        bool isGrounded = IsGrounded();

        m_characterAnimator.SetBool(m_animIsGroundedHash, isGrounded);

        m_characterAnimator.ResetTrigger(m_animTriggerJumpHash);

        if (Input.GetButtonDown("Jump"))
        {
            verticalVelocity = Jump(isGrounded);
        }

        m_characterAnimator.ResetTrigger(m_animTriggerAttackHash);

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        m_characterRigidBody.velocity = new Vector2(horizontalVelocity, m_characterRigidBody.velocity.y + verticalVelocity);
    }

    private float UpdateHorizontal()
    {
        float horizontalVelocity = Input.GetAxis("Horizontal") * m_horizontalSpeed;

        if (horizontalVelocity == 0)
        {
            m_characterAnimator.SetBool(m_animIsRunningHash, false);
        }
        else
        {
            m_characterAnimator.SetBool(m_animIsRunningHash, true);
        }

        if (horizontalVelocity < 0)
        {
            SetSpriteFlip(!m_spriteFlipped);
        }
        else if (horizontalVelocity > 0)
        {
            SetSpriteFlip(m_spriteFlipped);
        }

        if (IsWalled(horizontalVelocity))
        {
            horizontalVelocity = 0;
        }

        return horizontalVelocity;
    }

    private void UpdateDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 direction = m_wandContainer.GetDirection();

        if (horizontalInput != 0)
        {
            direction = new Vector2(Math.Sign(horizontalInput), 0);
        }

        direction = new Vector2(direction.x, verticalInput);

        m_wandContainer.SetDirection(direction);
    }

    private bool IsGrounded()
    {
        RaycastHit2D[] hits = new RaycastHit2D[64];

        int hitCount = m_characterCollider.Cast(Vector2.down, hits, m_verticalCastDistance);

        for (int hit = 0; hit < hitCount; hit++)
        {
            if (hits[hit].collider != null && hits[hit].collider.CompareTag(m_groundTag))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsWalled(float xDirection)
    {
        RaycastHit2D[] hits = new RaycastHit2D[64];

        int hitCount = m_characterCollider.Cast(new Vector2(xDirection, 0), hits, m_horizontalCastDistance);

        for (int hit = 0; hit < hitCount; hit++)
        {
            if (hits[hit].collider != null && hits[hit].collider.CompareTag(m_groundTag))
            {
                return true;
            }
        }

        return false;
    }

    private void SetSpriteFlip(bool flip)
    {
        m_characterSpriteRenderer.flipX = flip;
        m_characterAttackRenderer.flipX = flip;
    }


    private void Attack()
    {
        m_wandContainer.WandCast();
        //m_characterAnimator.SetTrigger(m_animTriggerAttackHash);
    }

    private float Jump(bool isGrounded)
    {
        if (isGrounded)
        {
            m_characterAnimator.SetTrigger(m_animTriggerJumpHash);
            return m_verticalSpeed;
        }

        return 0;
    }

    public void SetWand(int id)
    {
        m_wandContainer.SetWandID(id);
        m_characterAnimator.SetInteger(m_animIdOfWandHash, m_wandContainer.GetCurrentWandID());
    }

    public void UpgradeWand()
    {
        m_wandContainer.NextWand();
        m_characterAnimator.SetInteger(m_animIdOfWandHash, m_wandContainer.GetCurrentWandID());
    }

    public void Die()
    {
        CheckPointRegistry.Respawn();
    }
}
