using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxB : Collidable
{
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag=="Fighter" && coll.name=="Player")
        {   
            if(GameManager.instance.Player.spriteRenderer.sprite != GameManager.instance.playerSprites[2])
            {
                Damage dmg=new Damage
                {
                    damageAmount=damage,
                    origin=transform.position,
                    pushForce=pushForce
                };
                coll.SendMessage("ReceiveDamage",dmg);
                Debug.Log("bhit");
            }
            else
            {
                GameManager.instance.rightcolor=true;
            }
        }
       
    }
}
