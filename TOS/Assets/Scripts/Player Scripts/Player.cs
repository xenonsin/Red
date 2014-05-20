using UnityEngine;
using System.Collections;

public class Player : Entity  {


	// Use this for initialization
	public override void Awake() {
        base.Awake();
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
