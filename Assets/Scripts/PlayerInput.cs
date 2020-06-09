using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] float dodgeRate = 0.9f;
    private float nextDodge;
    [SerializeField] float attackRate = 0.9f;
    private float nextAttack;

    public IState CheckInputNotMove()
    {
        if ((Input.GetButton("Attack1") || Input.GetMouseButtonDown(1)) && Time.time > nextAttack)
        {
            //attack
            nextAttack = Time.time + attackRate;
            Debug.Log("attackButton");
            return new StateAttack(this.gameObject);
        }
        /*else if (Input.GetButton("Dodge") && Time.time > nextDodge)
        {
            //dodge
            nextDodge = Time.time + dodgeRate;
            Debug.Log("dodgeButton");
            return new StateDodge();
        }*/
        else return new StateMove(this.gameObject);
    }
}
