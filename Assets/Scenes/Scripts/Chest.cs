using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Chest : Collectable
{
    //public Sprite openedChest;
    public Sprite emptyChest;
    public int coinAmount = 5;
    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            //GetComponent<SpriteRenderer>().sprite = openedChest;
            
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.coins += coinAmount;
            GameManager.instance.ShowText("+ " + coinAmount + " coins!", 25, Color.yellow, transform.position, Vector3.up * 50, 1.5f);
        }
    }
}
