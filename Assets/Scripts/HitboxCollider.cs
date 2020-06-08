using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxCollider : MonoBehaviour
{
    Attacable attacable;


    private void Awake()
    {
        attacable = this.gameObject.GetComponent<Attacable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "CollisionForCamera" && other.gameObject.layer != this.gameObject.layer && other.gameObject.tag == "WeaponHitbox" && other.transform.root.GetComponent<Attacable>().getIsAttack() && other.transform.root.GetComponent<Attacable>().getAttackCounter() == 0)
        {
            //Debug.Log(this.gameObject.tag + "z" + other.gameObject.tag);
            //Debug.Log("TRUE KOLIZJA");
            Attacable enemy = other.transform.root.GetComponent<Attacable>();
            enemy.incAttackCounter(1);
            attacable.TakeDamge(other.transform.root.GetComponent<Attacable>().getStats().baseAttack);
        }
        else if(other.gameObject.tag=="LavaHitbox" && this.gameObject.tag=="Player")
        {
            Debug.Log("Lava KOLIZJA");
            attacable.TakeDamge(attacable.getStats().health);
        }
        else if (other.gameObject.tag != "CollisionForCamera" && other.gameObject.layer != this.gameObject.layer && other.gameObject.tag == "SpecialHitbox" && other.transform.root.GetComponent<EnemyController>().getSpecialAttack() && other.transform.root.GetComponent<Attacable>().getAttackCounter() == 0)
        {
            //Debug.Log(this.gameObject.tag + "z" + other.gameObject.tag);
            Debug.Log("SPECIAL KOLIZJA");
            Attacable enemy = other.transform.root.GetComponent<Attacable>();
            enemy.incAttackCounter(1);
            attacable.TakeDamge(other.transform.root.GetComponent<Attacable>().getStats().baseAttack+10);
        }
    }

}
