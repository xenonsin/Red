using UnityEngine;
using System.Collections;

public abstract class BaseWeapon {

    public string Name { get; set; }
    public float Damage { get; set; }
    public float Knockback { get; set; }
    public string AudioClipName { get; set; }
    public float Distance { get; set; }
    //public string SpriteName{ get; set;}

	
}
