using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStay : IState
{
    //temp script
    GameObject owner;

    private Animator anim;

    public StateStay(GameObject owner)
    {
        this.owner = owner;
    }

    public void Enter()
    {
        anim = owner.GetComponent<Animator>();
        anim.SetBool("IsDeath", false);
        anim.SetBool("IsAttack", false);
    }

    public void Execute()
    {
       // Debug.Log("updating stay state");
    }

    public void Exit()
    {
       // Debug.Log("exiting stay state");
    }
}
