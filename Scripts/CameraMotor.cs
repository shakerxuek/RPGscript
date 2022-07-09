using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform Lookat;
    public float BoundX = 0.15f;
    public float BoundY =0.05f;

    private void Start() 
    {
        Lookat=GameObject.Find("Player").transform;
    }
    private void LateUpdate() 
    {
        Vector3 delta=Vector3.zero;

        //Check if we are inside the bounds on the X axis
        float deltaX=Lookat.position.x-transform.position.x;
        if(deltaX>BoundX || deltaX<-BoundX)
        {
            if(transform.position.x<Lookat.position.x)
            {
                delta.x=deltaX-BoundX;
            }
            else
            {
                delta.x=deltaX+BoundX;
            }
        }

        //Check if we are inside the bounds on the Y axis
        float deltaY=Lookat.position.y-transform.position.y;
        if(deltaY>BoundY || deltaY<-BoundY)
        {
            if(transform.position.y<Lookat.position.y)
            {
                delta.y=deltaY-BoundY;
            }
            else
            {
                delta.y=deltaY+BoundY;
            }
        }
        transform.position += new Vector3(delta.x,delta.y,0);

    }
}
