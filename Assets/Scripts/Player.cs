using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AttackingSystemPlayer
{
    StateMachine stateMachine = new StateMachine();

    private PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.ChangeState(new StateMove(this.gameObject));
        input = this.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.ChangeState(input.CheckInputNotMove());
        stateMachine.Update();
    }
}
