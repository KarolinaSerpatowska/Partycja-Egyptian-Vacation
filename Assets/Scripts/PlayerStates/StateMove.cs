using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : IState
{
    private GameObject owner;

    [SerializeField] float InputX;
    [SerializeField] float InputZ;
    [SerializeField] Vector3 desiredMoveDirection;
    [SerializeField] bool blockRotationPlayer;
    [SerializeField] float desiredRotationSpeed=0.1f;
    private Animator anim;
    [SerializeField] float Speed;
    [SerializeField] float allowPlayerRotation;
    [SerializeField] Camera cam;
    [SerializeField] CharacterController controller;
    [SerializeField] bool isGrounded;
    private float verticalVel;
    private Vector3 moveVector;
    [SerializeField] float moveSpeed = 3f;


    [SerializeField] Transform playerTransform;


    public StateMove(GameObject owner) { this.owner = owner; }


    public void Enter()
    {
      //  Debug.Log("entering move state");
        anim = owner.GetComponent<Animator>();
        cam = Camera.main;
        controller = owner.GetComponent<CharacterController>();
        playerTransform = PlayerManger.instance.player.transform;
    }

    public void Execute()
    {
      //  Debug.Log("updating move state");
        InputMagnitude(); //ogolnie sila nacisku na drazku i klawiaturze(klawiatura slabo dziala na tym)
                              //sprawdzenie czy jestesmy na ziemi(character controller bez tego sie buguje)
        isGrounded = controller.isGrounded;
        if (isGrounded) verticalVel -= 0;
        else verticalVel -= 2;

        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);
    }

    public void Exit()
    {
       // Debug.Log("exiting move state");
    }


    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (!blockRotationPlayer)
        {//poruszenie
            controller.Move(desiredMoveDirection * Time.deltaTime * moveSpeed);
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }
    }

    public void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        anim.SetFloat("Vertical", InputZ, 0.0f, Time.deltaTime * 2f);
        anim.SetFloat("Horizontal", InputX, 0.0f, Time.deltaTime * 2f);

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
        }
    }

}
