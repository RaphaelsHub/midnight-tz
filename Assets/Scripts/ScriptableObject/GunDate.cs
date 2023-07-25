using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data")]
public class GunDate : ScriptableObject
{
    public string weaponName;
    public int ammoPerMgazine;
    public int amountOfMagazines;
    public int full;
    public float delayShoot;
}
