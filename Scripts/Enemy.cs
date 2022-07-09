using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue=1;
    public float triggerLength=1;
    public float chaseLength=5;
    private bool chasing;
    private bool collidingwithPlayer;
    private Transform playerTransform;
    private Vector3 startingPostion;
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits=new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        // playerTransform = GameObject.Find("Player").transform;
        playerTransform = GameManager.instance.Player.transform;
        startingPostion=transform.position;
        hitbox=transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate() 
    {   
        if(Vector3.Distance(playerTransform.position, startingPostion)<chaseLength)
        {
            if(Vector3.Distance(playerTransform.position,startingPostion)<triggerLength)
                chasing =true;
            
            if(chasing)
            {
                if(!collidingwithPlayer)
                {   
                    UpdateMotor((playerTransform.position-transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor((startingPostion-transform.position)*3);
            }

        }
        else
        {
            UpdateMotor((startingPostion-transform.position)*3);
            chasing=false;
        }
        
        collidingwithPlayer=false;
        boxCollider.OverlapCollider(filter,hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i]==null)
                continue;
            if(hits[i].tag=="Fighter"&&hits[i].name=="Player")
            {
                collidingwithPlayer=true;
            }

            hits[i]=null;
        }  
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+"+xpValue+"xp",30,Color.magenta,transform.position,Vector3.up*40,1.0f);
    }
}
