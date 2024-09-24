using UnityEngine;

namespace DefaultNamespace
{
    public class Gun
    {
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int MaxAmmo { get; set; }
        public GameObject Model { get; set; }

        public Gun(string name, int maxAmmo)
        {
            Name = name;
            MaxAmmo = maxAmmo;
            Ammo = maxAmmo; // При создании пистолета он полностью заряжен
        }

        public void Reload(int ammoCount)
        {
            Ammo = Mathf.Clamp(Ammo + ammoCount, 0, MaxAmmo); // Ограничиваем количество патронов
        }

        public bool Shoot()
        {
            if (Ammo > 0)
            {
                Ammo--;
                Debug.Log($"{Name} fired! Ammo left: {Ammo}");
                return true;
            }
            else
            {
                Debug.Log($"{Name} has no ammo!");
                return false;
            }
        }
    }

}