using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{   
    public SpriteRenderer spriteRenderer;
    public bool isAlive=false;

    protected override void Start()
    {   
        base.Start();
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    protected override void ReceiveDamage(Damage dmg)
    {   
        if(!isAlive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnhitPointChange();
        Debug.Log("hit");
    }
    private void FixedUpdate() 
    {
        float x= Input.GetAxisRaw("Horizontal");
        float y=Input.GetAxisRaw("Vertical");
        
        
        //Reset moveDelta
        if(isAlive)
            UpdateMotor(new Vector3(x,y,0));
    }
    public void swapSprite(int skinid)
    {
        spriteRenderer.sprite=GameManager.instance.playerSprites[skinid];
    }

    public void LevelUP()
    {
        maxHitpoint+=2;
        hitpoint=maxHitpoint;
    }
    public void setLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            LevelUP();
        }
    }

    public void Heal(int healingAmout)
    {   
        if(hitpoint==maxHitpoint)
            return;
        
        hitpoint += healingAmout;
        if(hitpoint>maxHitpoint)
            hitpoint=maxHitpoint;
        GameManager.instance.ShowText("+ "+healingAmout.ToString()+" hp",25,Color.green,transform.position,Vector3.up*30,1.0f);
        GameManager.instance.OnhitPointChange();
    }
    protected override void Death()
    {
        GameManager.instance.deahthmenu.SetTrigger("show");
        isAlive=false;
    }
    public void respawn()
    {
        Heal(maxHitpoint);
        isAlive=true;
        lastImmune=Time.time;
        pushDirection=Vector3.zero;
    }

    public void setact()
    {
        isAlive=true;
    }
}
