using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int health { get; set; }
    public override void InIt()
    {
        base.InIt();

        health = base.health;
    }
    public override void Movement()
    {
        base.Movement();
    }
    public void Damage()
    {
        if (isDead == true)
        {
            return;
        }
        Debug.Log("damage");
        health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);
        if (health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
}
