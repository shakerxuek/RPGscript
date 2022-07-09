using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : Collidable
{   
    public string[] sceneNames;
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name=="Player" || coll.name=="weapon")
        {   
            
            GameManager.instance.SaveState();
            //Teleport the Player
            string sceneName = sceneNames[0];
            SceneManager.LoadScene(sceneName);
            GameManager.instance.Player.isAlive=false;
        }
    }

    public void setisalive()
    {
        GameManager.instance.Player.isAlive=true;
        Debug.Log("called");
    }
}
