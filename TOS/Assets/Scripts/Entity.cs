﻿using UnityEngine;
using System.Collections;

public abstract class Entity: MonoBehaviour {


    private FloatingNumberManager _floatingNumberManager;
    private BloodManager _bloodManager;

    public string Name { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Height { get; set; }

    public virtual float RageMeter { get; set; }

    public virtual bool IsAlive { get; set; }

    public virtual void Hit(float damage)
    {
        Health -= damage;
        Debug.Log(Name + " Health: " + Health.ToString());

        _floatingNumberManager.DisplayDamage(damage, gameObject, Height + 1f);

        _bloodManager.EmitBlood(transform.position, Height);
    }

    public virtual void Heal(float heal)
    {
        Health += heal;
        Debug.Log(Name + " Health: " + Health.ToString());
    }

    public virtual void FullHeal()
    {
        Health = MaxHealth;
        Debug.Log(Name + " Health: " + Health.ToString());
    }

    public virtual void Death()
    {
        this.Recycle();
    }
    public virtual void IncreaseMaxHP(float newAmount)
    {
        MaxHealth = newAmount;
    }



    public virtual void Awake()
    {

        _floatingNumberManager = GameObject.FindGameObjectWithTag("Floating Numbers").GetComponent<FloatingNumberManager>();
        _bloodManager = GameObject.FindGameObjectWithTag("Blood").GetComponent<BloodManager>();
        Health = MaxHealth;
        IsAlive = true;
    }

    public virtual void Update()
    {
        if (Health <= 0)
            Death();
    }
}
