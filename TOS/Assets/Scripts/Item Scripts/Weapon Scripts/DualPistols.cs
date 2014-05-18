using UnityEngine;
using System.Collections;

public class DualPistols : RangeWeapon {

	public DualPistols()
    {
        Name = "ol' Reliable";
        Damage = 300f;
        AudioClipName = "attack1"; // get bullet sounds
        Knockback = 7f;
        Distance = 5.0f;
    }
}
