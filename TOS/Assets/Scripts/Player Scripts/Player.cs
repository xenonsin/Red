using UnityEngine;
using System.Collections;

public class Player : Entity  {

    //private SpriteManager _spriteManager;
   // private FloatingNumberManager _floatingNumberManager;
    //private BloodManager _bloodManager;

	// Use this for initialization
	public override void Awake() {
        base.Awake();
       // _spriteManager = this.GetComponent<SpriteManager>();
       // _floatingNumberManager = GameObject.FindGameObjectWithTag("Floating Numbers").GetComponent<FloatingNumberManager>();
       // _bloodManager = GameObject.FindGameObjectWithTag("Blood").GetComponent<BloodManager>();
        Name = "Red";
        Health = 1000f;
        Height = 2f;
	
	}
	
	// Update is called once per frame
	 public override void Update () {
        base.Update();
	
	}


    public override void Hit(float damage)
    {
        base.Hit(damage);
       // _animator.SetTrigger("Hit");
       

        //StartCoroutine( _spriteManager.FlashRed(0.2f));

        //_floatingNumberManager.DisplayDamage(damage, gameObject, Height + 1f);

       // _bloodManager.EmitBlood(transform.position, Height);
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
