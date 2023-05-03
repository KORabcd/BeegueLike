using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [System.Serializable]
    public struct WeaponStatus
    {
        public int damage;
        public double attackDelay;
    }
    public WeaponStatus weaponStatus;
}
