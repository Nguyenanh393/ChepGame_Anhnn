
using System;
using UnityEngine;

public class Enemy : Character
{

    [SerializeField] private float AttackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    
    private IState currentState;
    private bool isRight = true;
    
    private Character target;
    public Character Target => target;
    
    private void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }


    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeActiveAtack();
    }

    protected override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }
    
    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }
    
    public void ChangeState(IState newState) 
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    
    public void Moving()
    {
        ChangeAnim("run"); // tại sao không đổi được anim 
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAtack();
        Invoke(nameof(DeActiveAtack), 0.5f);
    }
    public void SetTarget(Character character)
    {
        this.target = character;
        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else
        {
            if (target != null)
            {
                ChangeState(new PatrolState());
            }
            else
            {
                ChangeState(new IdleState());
            }
        }
    }
    
    public bool IsTargetInRange()
    {
        return target != null && Vector2.Distance(transform.position, target.transform.position) <= AttackRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight); // enemy chay nhung khi gap Player thi van tiep tuc duoi theo thay vi dung lai
        }
    }


    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    
    private void ActiveAtack()
    {
        attackArea.SetActive(true);
    }
    
    private void DeActiveAtack()
    {
        attackArea.SetActive(false);
    }

}
