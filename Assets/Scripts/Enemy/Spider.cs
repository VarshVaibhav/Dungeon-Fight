using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject _acidEffectPrefab;
    public int health { get; set; }
    public override void InIt()
    {
        base.InIt();
        health = base.health;
    }
    public override void Update()
    { 

    }
    public override void Movement()
    {
    }
    public void Damage()
    {
        if(isDead == true)
        {
            return;
        }
        health--;
        if(health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
    public void Attack()
    {
        Instantiate(_acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
