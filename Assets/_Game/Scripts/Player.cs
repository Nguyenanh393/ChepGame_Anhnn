using UnityEngine;
using UnityEngine.Serialization;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    
    private static bool isGrounded;
    private static bool isJumping = false;
    private static bool isAttacking = false;
    //private static bool isDead = false;
    private int coin = 0;
    
    private float horizontal;
    private string currentAnimaName;

    private Vector3 savePoint;

    public override void OnInit()
    {
        base.OnInit();
        
        isAttacking = false;
        
        transform.position = savePoint;
        Invoke("ResetToIdle", 1f);
        
        DeActiveAtack();
        SavePoint();
    }
    // Update is called once per frame

    protected override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    
    protected override void OnDeath()
    {
        base.OnDeath();
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }
        isGrounded = CheckGrounded();
        Attack();
        Run();
        Throw();
        Jump();
        Fall();
    }
    
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }
    
    private void Attack()
    {
        if (isAttacking)
        {
            isAttacking = false;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.J) && isGrounded)
        {
            ChangeAnim("attack");
            isAttacking = true;
            Invoke(nameof(ResetToIdle), 0.5f);
            ActiveAtack();
            Invoke(nameof(DeActiveAtack), 0.5f);
        }
    }
    
    private void Throw()
    {
        if (Input.GetKeyDown(KeyCode.K) && isGrounded)
        {
            ChangeAnim("throw");
            isAttacking = true;
            Invoke("ResetToIdle", 0.5f);
            Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
        }
    }
    
    private void Jump()
    {
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.H) && isGrounded)
            {
                isJumping = true;
                ChangeAnim("jump");
                rb.AddForce(Vector2.up * jumpForce);
            }
 
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }
        }
    }

    private void Fall()
    {
        if (!isGrounded && rb.velocity.y < 0)
        {
            isJumping = false;
            ChangeAnim("fall");
        }
    }

    private void Run()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeAnim("run");
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));

        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }

    }

    private void ResetToIdle()
    {
        ChangeAnim("idle");
    } //????????

    private void ActiveAtack()
    {
        attackArea.SetActive(true);
    }
    
    private void DeActiveAtack()
    {
        attackArea.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coin++;
            Destroy(collision.gameObject);
        }
        
        if (collision.tag == "Deathzone")
        {
            ChangeAnim("die");
            Invoke("OnInit", 1f);
        }

    }

    public void SavePoint()
    {
        savePoint = transform.position;
    }
}
