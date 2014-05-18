using UnityEngine;
using System.Collections;

public abstract class Entity: MonoBehaviour {

    public string Name { get; set; }
    public float Health { get; set; }
    public float Height { get; set; }

    public virtual float RageMeter { get; set; }

    public virtual bool IsAlive { get; set; }

    public virtual void Hit(float damage)
    {
        Health -= damage;
        Debug.Log(Name + " Health: " + Health.ToString());
    }

    public virtual void Heal(float heal)
    {
        Health += heal;
        Debug.Log(Name + " Health: " + Health.ToString());
    }

    public virtual void Death()
    {

    }



    public virtual void Awake()
    {
    }

    public virtual void Update()
    {
        if (!IsAlive)
            Death();
    }
}
