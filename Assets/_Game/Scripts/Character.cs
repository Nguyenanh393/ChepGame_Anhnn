using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPref;
    
    private float hp;
    private string currentAnimName;
    
    public bool isDead => hp <= 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit() //goi bat ky luc nao cung dc
    {
        hp = 100;
        healthBar.OnInit(100, transform);
    }

    protected virtual void OnDespawn() // khong dung nua
    {
        
    }
   
    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        
        Invoke(nameof(OnDespawn), 2f);
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
    public void OnHit(float damage)
    {
        if (!isDead)
        {
            hp -= damage;
            if (isDead)
            {
                OnDeath();
                hp = 0;
            }
            healthBar.setNewHp(hp);
            Instantiate(combatTextPref, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }

    }
    
}
