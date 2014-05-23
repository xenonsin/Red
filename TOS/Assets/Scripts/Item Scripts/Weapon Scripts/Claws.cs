using UnityEngine;
using System.Collections;

public class Claws : MeleeWeapon
{

	public Claws()
    {
        Name = "grr";
        MinDamage = 10f;
        MaxDamage = 20f;
        AudioClipName = "attack1";
        Range = 6.0f;
        Knockback = 5f;
        Delay = 1f;
        Angle = 70f;
        Magnitude = 0.1f;
        ShakeDuration = 0.3f;
        ShakeSpeed = 5.0f;
    }
}
