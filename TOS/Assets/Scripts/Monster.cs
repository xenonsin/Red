using UnityEngine;
using System.Collections;

public class Monster : Entity{
    public string monsterName;
    public float health;
    public float height;

    private SpriteManager _spriteManager;
    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        _spriteManager = this.GetComponent<SpriteManager>();
        Name = monsterName;
        Health = health;
        Height = height;

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }


    public override void Hit(float damage)
    {
        base.Hit(damage);
        // _animator.SetTrigger("Hit");


        StartCoroutine(_spriteManager.FlashRed(0.2f));

        //instantiate floating numbers
        //flash red
    }


    public override void Heal(float heal)
    {
        base.Heal(heal);
    }

    public override void Death()
    {
        //play death animation
    }
}
