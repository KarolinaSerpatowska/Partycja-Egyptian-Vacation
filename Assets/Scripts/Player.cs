using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum States
    {
        Staying,
        Running,
        Attacking,
        Dodging,
        Dying
    }

    public States state;

    public float dodgeRate = 0.9f;
    private float nextDodge;
    public float attackRate = 0.9f;
    private float nextAttack;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        state = States.Staying;
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

        CheckInput();
    }

    void CheckInput() //prawdopodobnie dodge i attack do zmiany lub modyfikacji wg animacji
    {
        //movement (do zmiany)
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
}
