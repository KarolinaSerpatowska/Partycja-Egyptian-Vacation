using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Attacable
{
    StateMachine stateMachine = new StateMachine();
    IState newState;

    private PlayerInput input;
    Animator anim;
    HitboxCollider hitboxCollider;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.ChangeState(new StateMove(this.gameObject));
        input = this.GetComponent<PlayerInput>();
        hitboxCollider = this.GetComponent<HitboxCollider>();
        anim = this.gameObject.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isPlaying(anim, "Attack1"))
        {
            isAttack = true;
            if (weaponHitbox != null) weaponHitbox.SetActive(true);
        }
        else
        {
            isAttack = false;
            attackCounter = 0;
            if (weaponHitbox != null) weaponHitbox.SetActive(false);
        }
        //if (isDead) //stateMachine.ChangeState(new StateMove(this.gameObject));
        //else
        if (!isDead) stateMachine.ChangeState(input.CheckInputNotMove());
        if(!isDead) stateMachine.Update();

        
        if (myStats.health <= 0 && !isDead)
        {
            isDead = true;
            Die();          
        }
    }
/*
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        transform.position = spawner.transform.position;
        isDead = false;
        stateMachine.ChangeState(new StateMove(this.gameObject));
        myStats.health = myStats.maxHealth;
    }
    */
    void Die()
    {
        Destroy(GetComponent<HitboxCollider>());
        Destroy(weaponHitbox);
        anim.SetTrigger("Die");
    }

    public void setIsAttack(bool val)
    {
        isAttack = val;
    }

}
