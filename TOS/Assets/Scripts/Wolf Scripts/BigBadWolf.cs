using UnityEngine;
using System.Collections;

public class BigBadWolf : Entity
{
    public delegate void BBWolfAction();
    public static event BBWolfAction HpChange;

    public delegate void BBWolfAlive();
    public static event BBWolfAction Alive;


    public delegate void WolfDealth();
    public static event WolfDealth Dead;

    public string monsterName;
    public float health;
    public float height;

    private BigBadWolfSpriteManager _spriteManager;
    // Use this for initialization
    public override void Awake()
    {
        _spriteManager = this.GetComponent<BigBadWolfSpriteManager>();
        Name = monsterName;
        MaxHealth = health;
        Height = height;

        if (Alive != null)
            Alive();

        base.Awake();


    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }


    public override void Hit(float damage)
    {
        StartCoroutine(_spriteManager.FlashRed(0.2f));
        base.Hit(damage);

        if (HpChange != null)
            HpChange();


        // _animator.SetTrigger("Hit");

        //instantiate floating numbers
        //flash red
    }


    public override void Heal(float heal)
    {
        base.Heal(heal);

        if (HpChange != null)
            HpChange();
    }

    public override void Death()
    {
        if (Dead != null)
            Dead();
        base.Death();

    }
}
