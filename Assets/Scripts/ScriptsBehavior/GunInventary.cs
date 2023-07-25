using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInventary : MonoBehaviour
{
    public int rememberMyChoseOfWeapon;
    public List<GameObject> guns;
    public List<Weapon> weapon = new List<Weapon>();
    public List<GunDate> date = new List<GunDate>();

    public class Weapon
    {
        public string NameWeapon;
        public int AmmoPerMag;
        public int AmmountOfMag;
        public float DelayAfterShoot;

        public int full;

        public bool notEnoughtAmmo = false;
        public bool isReloading = false;

        public Weapon(string weaponName, int ammoCount, int magCount, float delta)
        {
            NameWeapon = weaponName;
            AmmoPerMag = ammoCount;
            AmmountOfMag = magCount;
            DelayAfterShoot = delta;
            full = ammoCount;
        }

        public void Shoot()
        {
            if (AmmoPerMag > 0)
                AmmoPerMag--;
            else
                Reload(full);
        }

        // Перезаока оружия
        private void Reload(int ammo)
        {
            if (AmmountOfMag > 0)
            {
                isReloading = true;
                AmmoPerMag += ammo;
                AmmountOfMag--;
            }
            else
            {
                Debug.Log("Not enought ammo-magazines, try to change the weapon");
                notEnoughtAmmo = true;
            }
        }
    }


    private void Awake()
    {
        for (int i = 0; i < date.Count; i++)
            weapon.Add(new Weapon(date[i].weaponName, date[i].ammoPerMgazine, date[i].amountOfMagazines, date[i].delayShoot));
    }

    public void UseWeapon(int index)
    {
        if (index >= 0 && index < weapon.Count)
        {
            rememberMyChoseOfWeapon = index;

            for (int i = 0; i < weapon.Count; i++)
            {
                if (i == rememberMyChoseOfWeapon)
                    guns[i].gameObject.SetActive(true);
                else
                    guns[i].gameObject.SetActive(false);
            }
        }
    }
}

