using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : Collidable
{
    public int[] damagePoint={1,2,3,4,5,6};
    public float[] pushForce ={6f,6.2f,6.5f,7f,7.2f,8.5f};

    public int weaponLevel=0;
    private SpriteRenderer spriteRenderer;

    private Animator anim;  

    private float cooldown=0.2f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        spriteRenderer=GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
    }
    protected override void Update() 
    {
        base.Update();
        if(Time.time-lastSwing>cooldown)
        {
            lastSwing =Time.time;
            Swing();
        }
        GameManager.instance.rightcolor=false;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag=="Fighter")
        {
            if(coll.name=="Player" || coll.name=="TNPC" || GameManager.instance.rightcolor==false)
                return;

            
            Damage dmg=new Damage
            {
                damageAmount=damagePoint[weaponLevel],
                origin=transform.position,
                pushForce=pushForce[weaponLevel]
            };
            coll.SendMessage("ReceiveDamage",dmg);
        }  
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite=GameManager.instance.weaponSprites[weaponLevel];

        //change stats
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel=level;
        spriteRenderer.sprite=GameManager.instance.weaponSprites[weaponLevel];
    }
}
