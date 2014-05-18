using UnityEngine;
using System.Collections;

public interface IEntity {

    string Name { get; set; }
    int MeleeDamage { get; set; }
    int SpellDamage { get; set; }
    int GunDamage { get; set; }

    int Health { get; set; }

    int RageMeter { get; set; }

    bool IsAlive { get; }

    void Hit(int damage);

    void Heal(int heal);

    void Death();
}
