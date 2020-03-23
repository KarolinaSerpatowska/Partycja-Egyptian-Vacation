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
    /*
        public float dodgeRate = 0.9f;
        private float nextDodge;
        public float attackRate = 0.9f;
        private float nextAttack;
        public float speed = 10.0f;
        public float rotationSpeed = 100.0f;
        */

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
    }

    // Update is called once per frame
    void Update()
    {
        /* do maszyny stanow
        switch (state)
        {
            case States.Staying:
               //staying
                break;
            case States.Running:
                //run
                break;
            case States.Attacking:
                //attack
                break;
            case States.Dodging:
                //dodge
                break;
            case States.Dying:
                //dying
                break;
        }*/

        // CheckInput();

        InputMagnitude();
        
        isGrounded = controller.isGrounded;
        if (isGrounded) verticalVel -= 0;
        else verticalVel -= 2;

        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector);

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
        {
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

/*
    void CheckInput() 
    {
        
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);

        transform.Rotate(0, rotation, 0);
        //movement end

        if (Input.GetButton("Attack1") && Time.time>nextAttack)
        {
            //attack
            nextAttack = Time.time + attackRate;
            Debug.Log("attackButton");
        }

        if (Input.GetButton("Dodge") && Time.time>nextDodge)
        {
            //dodge
            nextDodge = Time.time + dodgeRate;
            Debug.Log("dodgeButton");
        }

        if (Input.GetButtonDown("Options"))
        {
            //opcje
            Debug.Log("optionsButton");
        }
    }
    */
}
