using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public Canvas canvas;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] GameObject specialHitbox;
    [SerializeField] bool specialAttack=false;

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
        isAttack = false;
        canvas.gameObject.SetActive(false);
        specialAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying(anim, "Attack1") || isPlaying(anim, "Attack2"))
        {
            specialAttack = false;
            isAttack = true;
            if (weaponHitbox != null) weaponHitbox.SetActive(true);
        }
        else if(isPlaying(anim, "Attack_Special") && (!isPlaying(anim, "Attack1") || !isPlaying(anim, "Attack2")))
        {
            specialAttack = true;
            isAttack = false;
            if (specialHitbox != null) specialHitbox.SetActive(true);
        }
        else
        {
            specialAttack = false;
            isAttack = false;
            attackCounter = 0;
            if(weaponHitbox!=null) weaponHitbox.SetActive(false);
            if (specialHitbox != null) specialHitbox.SetActive(false);
        }
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (!dead && distance <= seeingRadious)
        {
            seePlayer = true;
            canvas.gameObject.SetActive(true);
            agent.isStopped = false; //zeby sie nie poruszal niepotrzebnie
            if (agent.velocity == Vector3.zero) setAnimToIdle(); 
            else setAnimToRun();

            agent.SetDestination(target.transform.position);
            if (distance <= agent.stoppingDistance && Time.time > nextAttack && !isPlaying(anim, "Attack1") && !isPlaying(anim,"Attack2") && !isPlaying(anim,"Attack_Special"))
            {
                //Atack Target
                agent.isStopped = true;
                nextAttack = Time.time + attackOffset;
                var num = Random.Range(0, 100); //random attack
                if (num >= 0 && num <= 50) //choose normal attack
                {
                    setAnimToAttack();
                    // this.Attack(target.GetComponent<Attacable>());

                }
                else //choose special attack
                {
                    setAnimToSpecialAttack();
                    //pewnie trzeba zmienic funkcje
                    //tymczasowo----------------------------------------------------------
                    //this.Attack(target.GetComponent<Attacable>()); //--------------------------------------------------------
                }
             //   target.GetComponent<Attacable>().showStats(); //test
            }
            FaceTarget();
        }
        else
        {
            seePlayer = false;
            canvas.gameObject.SetActive(false);
            agent.isStopped = true; //nie biegaj mi tu
            setAnimToIdle();
        }


        //dead
        if (myStats.health <= 0 && !dead)
        {
            mainCanvas.GetComponent<ScoreText>().counter--;
            dead = true;
            transform.Find("EnemyParticles").gameObject.SetActive(false);
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

    public bool getSpecialAttack()
    {
        return specialAttack;
    }

    void Die()
    {
        Destroy(weaponHitbox);
        Destroy(specialHitbox);
        Destroy(GetComponent<HitboxCollider>());
        this.GetComponent<HitboxCollider>().enabled = false; 
        //Debug.Log("Enemy health: " + myStats.health);
        Debug.Log("Enemy Died");
        Animator anim = this.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Die");
        Destroy(this.gameObject, 5f);
    }

}
