using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IEntity  {

    [SerializeField]
    private string _name;
    public string Name { get { return _name; } set { _name = value; } }

    [SerializeField]
    private int _meleeDamage;
    public int MeleeDamage { get { return _meleeDamage; } set { _meleeDamage = value; } }

    [SerializeField]
    private int _spellDamage;
    public int SpellDamage { get { return _spellDamage; } set { _spellDamage = value; } }

    [SerializeField]
    private int _gunDamage;
    public int GunDamage { get { return _gunDamage; } set { _gunDamage = value; } }

    [SerializeField]
    private int _health;
    public int Health { get { return _health; } set { _health = value; } }

    [SerializeField]
    private int _rageMeter;
    public int RageMeter { get { return _rageMeter; } set { _rageMeter = value; } }


    public bool IsAlive { get { return Health <= 0 ? true : false; } }

    private SpriteManager _spriteManager;
	// Use this for initialization
	void Awake() {
        _spriteManager = this.GetComponent<SpriteManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!IsAlive)
            Death();
	
	}


    public void Hit(int damage)
    {
        Health -= damage;
       // _animator.SetTrigger("Hit");
        Debug.Log("Health: " + Health.ToString());

        StartCoroutine( _spriteManager.FlashRed(0.2f));

        //instantiate floating numbers
        //flash red
    }


    public void Heal(int heal)
    {
        Health += heal;
    }

    public void Death()
    {
        //play death animation
    }
}
