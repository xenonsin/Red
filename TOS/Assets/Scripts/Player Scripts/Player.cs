using UnityEngine;
using System.Collections;

public class Player : Entity  {

    private SpriteManager _spriteManager;
    private FloatingNumberManager _floatingNumberManager;
	// Use this for initialization
	public override void Awake() {
        base.Awake();
        _spriteManager = this.GetComponent<SpriteManager>();
        _floatingNumberManager = GameObject.FindGameObjectWithTag("Floating Numbers").GetComponent<FloatingNumberManager>();
        Name = "Red";
        Health = 1000f;
        Height = 3f;
	
	}
	
	// Update is called once per frame
	 public override void Update () {
        base.Update();
	
	}


    public override void Hit(float damage)
    {
        base.Hit(damage);
       // _animator.SetTrigger("Hit");
       

        StartCoroutine( _spriteManager.FlashRed(0.2f));

        _floatingNumberManager.DisplayDamage(damage, gameObject, Height);

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
