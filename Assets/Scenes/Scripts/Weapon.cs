using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage Struct
    public float[] damagePoint = { 1, 2, 2.5f, 3, 4.5f, 5, 5.5f, 6, 6.5f, 7, 10};
    public float[] pushForce = { 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3f, 3.2f, 3.4f, 3.6f, 4f};

    //Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //Swing
    private Animator swingAnim;
    private float cooldown = 5.0f;
    private float lastSwing;

    
    protected override void Start()
    {
        base.Start();
        swingAnim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if(Time.time - lastSwing > cooldown) 
            {
                lastSwing = Time.time;
                Swing();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {


            lastSwing = Time.time;
            Swing();
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if(coll.name == "Player")
                return;

            
            Damage dmg = new Damage()
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel],
            };

            coll.SendMessage("RecieveDamage", dmg);
        }
    }

    private void Swing()
    {
        swingAnim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
