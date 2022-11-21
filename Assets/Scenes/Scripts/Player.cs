using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    protected override void RecieveDamage(Damage dmg)
    {
        base.RecieveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    public void ChangeSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp() 
    {
        maxHitPoint++;
        hitpoint = maxHitPoint;
    }
    public void SetLevel(int level)
    {
        for(int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
    public void Heal(int healingAmount)
    {
        if(hitpoint == maxHitPoint)
        {
            return;
        }

        hitpoint += healingAmount;
        if(hitpoint > maxHitPoint)
        {
            hitpoint = maxHitPoint;
        }
  
        GameManager.instance.ShowText("+" + healingAmount.ToString() + " HP ", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }
}
