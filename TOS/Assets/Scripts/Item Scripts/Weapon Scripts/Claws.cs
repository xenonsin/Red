using UnityEngine;
using System.Collections;

public class Claws : MeleeWeapon
{

	public Claws()
    {
        Name = "grr";
        MinDamage = 5f;
        MaxDamage = 20f;
        AudioClipName = "attack1";
        Range = 4.0f;
        Knockback = 5f;
        Delay = 1f;
        Angle = 60f;
        Magnitude = 0.1f;
        ShakeDuration = 0.3f;
        ShakeSpeed = 5.0f;
    }
}
