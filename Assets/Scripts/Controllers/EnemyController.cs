using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyController : Attacable
{
    [SerializeField] private float seeingRadious=6f;
    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool seePlayer=false;
    private float nextAttack = 0;
    [SerializeField] private float attackOffset = 0.3f;
    public float distance;
    bool dead = false;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!agent)
            Debug.LogError("Agent not Found");
        target = PlayerManger.instance.player;
        if (!target)
            Debug.LogError("Target not Found.");
        else
            Debug.Log("Found Player");

        anim = GetComponent<Animator>();
        setAnimToIdle();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (!dead && distance <= seeingRadious)
        {
            seePlayer = true;
            agent.isStopped = false; //zeby sie nie poruszal niepotrzebnie
            if (agent.velocity == Vector3.zero) setAnimToIdle(); 
            else setAnimToRun();
            agent.SetDestination(target.transform.position);
            if (distance <= agent.stoppingDistance && Time.time > nextAttack)
            {
                //Atack Target
                agent.isStopped = true;
                nextAttack = Time.time + attackOffset;
                var num = Random.Range(0, 100); //random attack
                if (num >= 0 && num <= 50) //choose normal attack
                {
                    setAnimToAttack();
                    //zmienic na hitboxy - if kolizja to trafienie
                    this.Attack(target.GetComponent<Attacable>()); 
                }
                else //choose special attack
                {
                    setAnimToSpecialAttack();
                    //pewnie trzeba zmienic funkcje
                    this.Attack(target.GetComponent<Attacable>());
                }
            }
            FaceTarget();
        }
        else
        {
            seePlayer = false;
            agent.isStopped = true; //nie biegaj mi tu
            setAnimToIdle();
        }


        //dead
        if (myStats.health <= 0 && !dead)
        {
            dead = true;
            //tutaj wylaczyc particle - tag particle
            Die();//jezeli zrobimy atak z dlugiego dystansu to bedzie trzeba to przeniesc poza tego ifa. 
        }

    }

    void setAnimToRun()
    {
        anim.SetBool("Run", true);
        anim.SetBool("Idle", false);
    }

    void setAnimToIdle()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Idle", true);
    }

    void setAnimToAttack()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Idle", false);

        var num=Random.Range(0, 100);
        if (num >= 0 && num <= 50) anim.SetTrigger("Attack1");
        else anim.SetTrigger("Attack2");
        
    }

    void setAnimToSpecialAttack()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Idle", false);
        anim.SetTrigger("AttackSpecial");
    }

    void FaceTarget()
    {
        Vector3 directrion = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directrion.x, 0, directrion.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seeingRadious);
    }

    public override void Die()
    {
        base.Die();
        //Debug.Log("Enemy health: " + myStats.health);
        Debug.Log("Enemy Died");
        Animator anim = this.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Die");
        Destroy(this.gameObject, 5f);
    }

}
