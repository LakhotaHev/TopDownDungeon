using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            //Destroy(player.gameObject);
            //Destroy(floatingTextManager.gameObject);

            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List <int> weaponPrices;
    public List <int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform healthBar;

    //Logic
    public int coins;
    public int experience;


    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        //Is the Weapon Maxxed out
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if(coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    //Healthbar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitPoint;
        healthBar.localScale = new Vector3(1, ratio, 1);

    }
    //FunctionTest
    /*private void Update()
    {
        Debug.Log(GetCurrentLevel());
    }*/

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while(experience >= add)
        {
            add += xpTable[r];
            r++;

            if(r == xpTable.Count) 
            {
                return r;
            }

        }
        return r;
    }

    public int GetXpToLevel(int level) 
    {
        int r = 0;
        int xp = 0;

        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currentLevel = GetCurrentLevel();
        experience += xp;
        if(currentLevel < GetCurrentLevel()) 
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        player.OnLevelUp();
    }

    //Saves state
    /*
     * INT characterSkin 
     * INT coins
     * INT experience
     * INT weaponLevel
     * 
     */

    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }
  
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change player skin
        coins = int.Parse(data[1]);

        //XP
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        {
            player.SetLevel(GetCurrentLevel());
        }
        //Change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
        
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
}
