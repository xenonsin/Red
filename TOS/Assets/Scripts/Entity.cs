using UnityEngine;
using System.Collections;

public abstract class Entity: MonoBehaviour {

    private SpriteManager _spriteManager;
    private FloatingNumberManager _floatingNumberManager;
    private BloodManager _bloodManager;

    public string Name { get; set; }
    public float Health { get; set; }
    public float Height { get; set; }

    public virtual float RageMeter { get; set; }

    public virtual bool IsAlive { get; set; }

    public virtual void Hit(float damage)
    {
        Health -= damage;
        Debug.Log(Name + " Health: " + Health.ToString());

        StartCoroutine(_spriteManager.FlashRed(0.2f));

        _floatingNumberManager.DisplayDamage(damage, gameObject, Height + 1f);

        _bloodManager.EmitBlood(transform.position, Height);
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
        _spriteManager = this.GetComponent<SpriteManager>();
        _floatingNumberManager = GameObject.FindGameObjectWithTag("Floating Numbers").GetComponent<FloatingNumberManager>();
        _bloodManager = GameObject.FindGameObjectWithTag("Blood").GetComponent<BloodManager>();
    }

    public virtual void Update()
    {
        if (!IsAlive)
            Death();
    }
}
