using Interfaces;
using UnityEngine;
using System.Collections;
using Systems;

namespace Controllers
{
    public class AimController : MonoBehaviour, IAim
    {
        private Camera mainCamera;
        private bool isReloadingActive;  

        private bool IsShooting => Input.GetMouseButtonDown(0) && !isReloadingActive;  
        private bool IsReloading => Input.GetKeyDown(KeyCode.R) && !isReloadingActive; 

        private InventorySystem inventory = new InventorySystem();
        
        private void Awake()
        {
            mainCamera = Camera.main;
            if (mainCamera == null) Debug.LogError("Main camera is not found!");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                inventory.SwitchGun(); 
            }
            if (IsShooting) Shoot();
            if (IsReloading) StartCoroutine(Reload());  
        }

        public void Shoot()
        {
            inventory.ShootCurrentGun(); 
            
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Центр экрана
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.gameObject.TryGetComponent(out EnemyController enemyController))
                {
                    enemyController.TakeDamage(10);
                }
            }
        }

        // Коррутина для перезарядки
        public IEnumerator Reload()
        {
            isReloadingActive = true;  
            var currentGun = inventory.GetCurrentGun();
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(3f);  
            if (currentGun != null)
            {
                currentGun.Reload(currentGun.MaxAmmo); // Перезарядка
                Debug.Log("Reload finished.");
                isReloadingActive = false;  
            }
        }
    }
}