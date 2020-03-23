using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum States
    {
        Staying,
        Moving,
        Attacking,
        Dodging,
        Dying
    }

    public States state;
    
    public float dodgeRate = 0.9f;
    private float nextDodge;
    public float attackRate = 0.9f;
    private float nextAttack;
   

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;
    private float verticalVel;
    private Vector3 moveVector;
    public float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        state = States.Staying;
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
        //poniezsze na wszelki wypadek, zaczecia nowej gry nie po raz pierwszy
        anim.SetBool("IsDeath", false);
        anim.SetBool("IsAttack", false);
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude(); //ogolnie sila nacisku na drazku i klawiaturze(klawiatura slabo dziala na tym)
//sprawdzenie czy jestesmy na ziemi(character controller bez tego sie buguje)
        isGrounded = controller.isGrounded;
        if (isGrounded) verticalVel -= 0;
        else verticalVel -= 2;

        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);
//maszyna stanow
        switch (state)
        {
            case States.Dying:
                //tu trzeba ogolnie wylaczyc sterowanie i pokazac jakies menu czy cos
                anim.SetBool("IsDeath", true);
                break;
            case States.Attacking:
                anim.SetBool("IsAttack", true);
                break;
            case States.Dodging:
                break;
            case States.Staying:
               // anim.SetBool("IsAttack", false);
                CheckInputNotMove();
                break;
            case States.Moving:
               // anim.SetBool("IsAttack", false);
                CheckInputNotMove();
                break;
        }
//------------------    
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        anim.SetFloat("Vertical", InputZ, 0.0f, Time.deltaTime * 2f);
        anim.SetFloat("Horizontal", InputX, 0.0f, Time.deltaTime * 2f);

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
            state = States.Moving;
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            state = States.Staying;
            anim.SetFloat("InputMagnitude", Speed, 0.0f, Time.deltaTime);
        }
        else state = States.Staying;
    }

    void CheckInputNotMove() 
    {
        if (Input.GetButton("Attack1") && Time.time>nextAttack)
        {
            //attack
            nextAttack = Time.time + attackRate;
            state = States.Attacking;
            anim.SetBool("IsAttack", true);
            Debug.Log(state);
            Debug.Log("attackButton");
        }

        if (Input.GetButton("Dodge") && Time.time>nextDodge)
        {
            //dodge
            nextDodge = Time.time + dodgeRate;
            state = States.Dodging;
            Debug.Log(state);
            Debug.Log("dodgeButton");
        }

        if (Input.GetButtonDown("Options"))
        {
            //opcje
            state = States.Staying;
            Debug.Log("optionsButton");
        }
    }
    
}
