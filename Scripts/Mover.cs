using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{   
    public Vector3 originsize;
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float yspeed=0.75f;
    public float xspeed=1.0f;

    protected virtual void Start() 
    {   
        originsize=transform.localScale;
        boxCollider=GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() 
    {
        
    
    }
    protected virtual void UpdateMotor(Vector3 input)
    {
        //Reset moveDelta
        moveDelta=new Vector3(input.x*xspeed, input.y*yspeed,0);
        
        //Swap direction 
        if(moveDelta.x>0)
            transform.localScale=originsize;
        else if(moveDelta.x<0)
            transform.localScale=new Vector3(originsize.x*-1,originsize.y,originsize.z);
        
        moveDelta+=pushDirection;

        pushDirection=Vector3.Lerp(pushDirection,Vector3.zero,pushRecoverySpeed);

        hit=Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y*Time.deltaTime), LayerMask.GetMask("Actor","Blocking"));
        
        if(hit.collider==null)
        {   
            //move
            transform.Translate(0,moveDelta.y*Time.deltaTime,0);
        }
        
        hit=Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x*Time.deltaTime), LayerMask.GetMask("Actor","Blocking"));
        
        if(hit.collider==null)
        {   
            //move
            transform.Translate(moveDelta.x*Time.deltaTime,0,0);
        }
    }
}
