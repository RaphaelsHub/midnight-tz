using DefaultNamespace;
using UnityEngine;

namespace Systems
{
    using System.Collections.Generic;

    public class InventorySystem
    {
        private List<Gun> guns = new List<Gun>();
        private int currentGunIndex = 0;
        private int maxGuns = 3;
        public List<Gun> Guns;

        public InventorySystem()
        {
            // Создаем оружие и добавляем его в инвентарь
            AddGun(new Gun("Pistol", 30));
            AddGun(new Gun("Shotgun", 30));
            AddGun(new Gun("Machine gun", 30));
        }
        
        // Добавление оружия в инвентарь
        public bool AddGun(Gun newGun)
        {
            if (guns.Count >= maxGuns)
            {
                Debug.Log("Inventory is full. Can't add more guns.");
                return false;
            }
        
            guns.Add(newGun);
            Debug.Log($"{newGun.Name} added to inventory.");
            return true;
        }

        // Переключение на следующее оружие
        public void SwitchGun()
        {
            if (guns.Count == 0) return;

            currentGunIndex = (currentGunIndex + 1) % guns.Count;
            Debug.Log($"Switched to {guns[currentGunIndex].Name}");
        }

        // Получение текущего оружия
        public Gun GetCurrentGun()
        {
            if (guns.Count == 0) return null;

            return guns[currentGunIndex];
        }

        // Стрельба из текущего оружия
        public void ShootCurrentGun()
        {
            var gun = GetCurrentGun();
            if (gun != null)
            {
                gun.Shoot();
            }
        }
    }

}