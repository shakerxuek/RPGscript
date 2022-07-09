using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterMenu : MonoBehaviour
{
    //text
    public Text levelText, hitpoiontText, pesosText, upgradeCostText, xpText;
    private int currentCharacterSelection=0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //character
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            if(currentCharacterSelection==GameManager.instance.playerSprites.Count)
                currentCharacterSelection=0;
            
            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;
            if(currentCharacterSelection<0)
                currentCharacterSelection=GameManager.instance.playerSprites.Count-1;

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite=GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.Player.swapSprite(currentCharacterSelection);
    }

    //weapon
    public void OnUpgradeClick()
    {
        if(GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    //character information
    public void UpdateMenu()
    {   
        weaponSprite.sprite=GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        hitpoiontText.text=GameManager.instance.Player.hitpoint.ToString()+" / "+GameManager.instance.Player.maxHitpoint;
        pesosText.text=GameManager.instance.pesos.ToString();
        if(GameManager.instance.weapon.weaponLevel==GameManager.instance.weaponPrices.Count)
            upgradeCostText.text="MAX";
        else
            upgradeCostText.text=GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        levelText.text=GameManager.instance.GetCurrentLevel().ToString();
        
        int currentlevel=GameManager.instance.GetCurrentLevel();
        //xpbar
        if(currentlevel==GameManager.instance.xpTable.Count)
        {
            xpText.text=GameManager.instance.experience.ToString()+"MAX LEVEL";
            xpBar.localScale=Vector3.one;
        }
        else
        {
            int prelevelxp=GameManager.instance.GetxpToLevel(currentlevel-1);
            int currentlevelxp=GameManager.instance.GetxpToLevel(currentlevel);

            int difference=currentlevelxp-prelevelxp;
            int currXP=GameManager.instance.experience-prelevelxp;
            float completionRatio=(float)currXP/(float)difference;
            xpBar.localScale=new Vector3(completionRatio,1,1);
            xpText.text=currXP.ToString()+" / "+difference;
        }
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            OnArrowClick(true);
        }   
        if(Input.GetKeyDown(KeyCode.K))
        {
            OnArrowClick(false);
        }
    }
}
