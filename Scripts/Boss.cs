using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] fireballspeed;
    public Transform[] fireballs;
    public float distance = 0.25f;
    public GameObject GG;
    private void Update()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position=transform.position+new Vector3(-Mathf.Cos(Time.time*fireballspeed[i])*distance,Mathf.Sin(Time.time*fireballspeed[i])*distance,0);
        }
    }

    protected override void Death()
    {
        base.Death();
        GG.SetActive(true);
        
    }
}
