using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{   
    public string message;
    private float cooldown=4.0f;
    private float lastshout;
    protected override void Start() 
    {   
        base.Start();
        lastshout=-cooldown;    
    }
        
    
    protected override void OnCollide(Collider2D coll)
    {   
        if(Time.time-lastshout>cooldown)
        {
            lastshout=Time.time;
            GameManager.instance.ShowText(message,25,Color.white,gameObject.transform.position+new Vector3(0.16f,0.16f,0),Vector3.zero,cooldown);
        }
        
    }
}
