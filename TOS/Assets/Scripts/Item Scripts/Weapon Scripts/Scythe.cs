using UnityEngine;
using System.Collections;

public class Scythe : MeleeWeapon {

    public Scythe()
    {
        Name = "ol' Reliable";
        Damage = 300f;
        AudioClipName = "attack1";
        Radius = 5f;
        Knockback = 7f;
        Distance = 5.0f;
        Delay = 0.5f;
        Angle = 40f;
        Magnitude = 0.2f;
        ShakeDuration = 0.3f;
        ShakeSpeed = 5.0f;
    }

    public Scythe(string name, float damage)
    {
        Name = name;
        Damage = damage;
        AudioClipName = "attack1";
        Radius = 5f;
        Knockback = 7f;
        Distance = 5.0f;
        Delay = 0.5f;
        Angle = 40f;
        Magnitude = 0.2f;
        ShakeDuration = 0.3f;
        ShakeSpeed = 5.0f;
    }

    public Scythe(string name, string audioClipName, float damage, float radius, float knockback, float distance, float delay, float angle, float magnitude, float shakeDuration, float shakeSpeed)
    {
        Name = name;
        AudioClipName = audioClipName;
        Damage = damage;
        Radius = radius;
        Knockback = knockback;
        Distance = distance;
        Delay = delay;
        Angle = angle;
        Magnitude = magnitude;
        ShakeDuration = shakeDuration;
        ShakeSpeed = shakeSpeed;

    }
    


}
