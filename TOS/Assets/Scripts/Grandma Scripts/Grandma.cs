using UnityEngine;
using System.Collections;

public class Grandma : Entity
{

    public delegate void GrandmaAction();
    public static event GrandmaAction HpChange;

    public delegate void GrandmaDeath();
    public static event GrandmaDeath Dead;

    private GrandmaSpriteManager _spriteManager;

    // Use this for initialization
    public override void Awake()
    {
        _spriteManager = this.GetComponent<GrandmaSpriteManager>();
        Name = "Grandma";
        MaxHealth = 2000f;
        Height = 2f;

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

    }


    public override void Heal(float heal)
    {
        base.Heal(heal);
        if (HpChange != null)
            HpChange();
    }

    public override void FullHeal()
    {
        base.FullHeal();
        if (HpChange != null)
            HpChange();
    }

    public override void IncreaseMaxHP(float newAmount)
    {

        base.IncreaseMaxHP(newAmount);
        if (HpChange != null)
            HpChange();
    }

    public override void Death()
    {
        if (Dead != null)
            Dead();
        base.Death();

    }

    public override void Kill()
    {
        base.Kill();
    }

    public void EndPanelClicked(dfControl ignore, dfMouseEventArgs args)
    {
        FullHeal();
    }
}
