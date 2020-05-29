using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Attacable : MonoBehaviour
{
    [SerializeField] protected Stats myStats;
    [SerializeField] protected bool isDead = false;
    public GameObject weaponHitbox;
    [SerializeField] protected bool isAttack;
    [SerializeField] protected int attackCounter;
    public GameObject bar;
    
    private void Awake()
    {
        myStats = GetComponent<Stats>();
        if (!myStats)
            Debug.Log("Problem with myStats");
        else
        {
            Debug.Log("Stats Found");
        }
        isAttack = false;
        attackCounter = 0;
        bar.GetComponent<HealthBar>().setMax(myStats.maxHealth * 1.0f);
    }

    void Start()
    {
        
    }
   
    public virtual void TakeDamge(float damage)
    {
        myStats.health -= damage;
        bar.GetComponent<HealthBar>().setBar(myStats.health*1.0f);
    }
    public virtual void Attack(Attacable enemy)
    {
        if(enemy!=null) enemy.TakeDamge(myStats.baseAttack);
    }
    public virtual void Attack()
    {
        
    }
    public virtual void Dodge()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1.5f);//Enemy attack Range
        
    }
    
    public bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public void showStats()
    {
        Debug.Log(myStats.health);
    }

    public Stats getStats()
    {
        return myStats;
    }

    public bool getIsAttack()
    {
        return isAttack;
    }

    public void incAttackCounter(int val) { attackCounter += val; }

    public int getAttackCounter() { return attackCounter; }

}
