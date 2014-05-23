using UnityEngine;
using System.Collections;

public class Player : Entity  {

    public delegate void PlayerAction();
    public static event PlayerAction HpChange;


    private SpriteManager _spriteManager;

	// Use this for initialization
	public override void Awake() {
        _spriteManager = this.GetComponent<SpriteManager>();
        Name = "Red";
        MaxHealth = 1000f;
        Height = 2f;
	
        base.Awake();

	}
	
	// Update is called once per frame
	 public override void Update () {
        base.Update();
	}


    public override void Hit(float damage)
    {
        StartCoroutine(_spriteManager.FlashRed(0.2f));

        if (HpChange != null)
            HpChange();
        base.Hit(damage);


    }


    public override void Heal(float heal)
    {
        if (HpChange != null)
            HpChange();
        base.Heal(heal);
    }

    public override void FullHeal(float heal)
    {
        if (HpChange != null)
            HpChange();
        base.FullHeal(heal);
    }

    public override void IncreaseMaxHP(float newAmount)
    {
        if (HpChange != null)
            HpChange();
        base.IncreaseMaxHP(newAmount);
    }

    public override void Death()
    {
        //play death animation
    }
}
