using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() 
    {   
        if(GameManager.instance!=null)
        {
            Destroy(gameObject);
            Destroy(Player.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        
        instance=this;
        SceneManager.sceneLoaded+=LoadState;
        SceneManager.sceneLoaded+=OnSceneLoaded;
    }
    private void Start() 
    {
        Player.transform.position=GameObject.Find("sp").transform.position;
    }
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public Player Player;
    public weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointbar;
    public GameObject hud;
    public GameObject menu;
    public Animator deahthmenu;
    public bool rightcolor=false;

    public int pesos;
    public int experience;

    public void ShowText(string msg, int fontsize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg,fontsize,color,position,motion,duration);
    }
    //upgrade weapon
    public bool TryUpgradeWeapon()
    {
        if(weaponPrices.Count<=weapon.weaponLevel)
            return false;
        if(pesos>=weaponPrices[weapon.weaponLevel])
        {
            pesos -=weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    //xp system
    public int GetCurrentLevel()
    {
        int r=0;
        int add=0;

        while(experience>=add)
        {
            add+=xpTable[r];
            r++;

            if(r==xpTable.Count)
                return r;
        }
        return r;
    }

    public int GetxpToLevel(int level)
    {
        int r=0;
        int xp=0;

        while(r<level)
        {
            xp+=xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currentlevel = GetCurrentLevel();
        experience+=xp;
        if(currentlevel<GetCurrentLevel())
            OnlevelUp();
    }

    public void OnlevelUp()
    {
        Player.LevelUP();
        OnhitPointChange();
    }
    public void SaveState()
    {
        string s="";
        s += "0" + "|";
        s += pesos.ToString()+"|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState",s);
    }

    public void LoadState(Scene s,LoadSceneMode mode)
    {   
        SceneManager.sceneLoaded-=LoadState;
        if(!PlayerPrefs.HasKey("Savestate"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //skin
        pesos=int.Parse(data[1]);
        experience=int.Parse(data[2]);
        Player.setLevel(GetCurrentLevel());
        //weapon
        weapon.SetWeaponLevel(int.Parse(data[3]));
        
    }

    //Hitpoint bar
    public void OnhitPointChange()
    {
        float ratio=(float)Player.hitpoint/(float)Player.maxHitpoint;
        hitpointbar.localScale=new Vector3(1,ratio,1);
    }

    public void OnSceneLoaded(Scene s,LoadSceneMode mode)
    {
        Player.transform.position=GameObject.Find("sp").transform.position;
    }
    public void restart()
    {
        deahthmenu.SetTrigger("hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        Player.respawn();
    }

    
}
