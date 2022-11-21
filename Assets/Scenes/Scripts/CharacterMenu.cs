using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text fields
    public Text levelText, hitpointText, coinText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite, weaponSprite;
    public RectTransform xpBar;

    //Character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            //If at the end of characters
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            //If at the end of characters
            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.ChangeSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        //Upgrade the character info
        if(GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    //Update the character info
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "Max Lvl";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        //MEta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        coinText.text = GameManager.instance.coins.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        //levelBar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if(currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " Total experience points"; //Display Total XP
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int pastLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentLevelXp = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currentLevelXp - pastLevelXp;
            int xpProgress = GameManager.instance.experience - pastLevelXp;

            float completionRatio = (float)xpProgress / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = xpProgress.ToString() + " / " + diff;
        }

    }

}
