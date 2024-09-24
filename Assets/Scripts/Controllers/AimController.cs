using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class AimController : MonoBehaviour, IAim
    {
        private float mainCameraPrevPos;
        private float gunCameraPrevPos;

        [SerializeField] private float speedOfBullet = 100f;

        [SerializeField] private Transform bullet;
        [SerializeField] private Transform bulletSpawn;
        [SerializeField] private Vector3 bulletDirection;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera gunCamera;

        public bool IsShooting => Input.GetMouseButtonDown(0);
        public bool IsAiming => Input.GetMouseButtonDown(1);
        public bool IsReloading => Input.GetKeyDown(KeyCode.R);

        private void Awake()
        {
            mainCamera = Camera.main;
            gunCamera = GameObject.FindGameObjectWithTag("GunCamera").GetComponent<Camera>();
            if (mainCamera != null) mainCameraPrevPos = mainCamera.fieldOfView;
            if (gunCamera != null) gunCameraPrevPos = gunCamera.fieldOfView;
        }

        void Update()
        {
            if (IsShooting) Shoot();
            if (!IsAiming) ZoomCamera();
            if (IsReloading) Reload();
        }

        public void Shoot()
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // 0.5f, 0.5f, 0 - center of the screen
            RaycastHit hit; // hit point of the ray
            var target = // if ray hit smth, target = hit.point, else target = 100 units from the camera
                Physics.Raycast(ray, out hit)
                ? hit.point
                : ray.GetPoint(100);
            GameObject bul = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation).gameObject;
            bulletDirection = target - bulletSpawn.position;
            bul.transform.forward = bulletDirection.normalized;
            bul.GetComponent<Rigidbody>().AddForce(bulletDirection.normalized * speedOfBullet, ForceMode.Impulse);
        }

        public void ZoomCamera()
        {
            if (IsAiming)
            {
                mainCamera.fieldOfView = 20;
                gunCamera.fieldOfView = 40;
            }
            else
            {
                mainCamera.fieldOfView = mainCameraPrevPos;
                gunCamera.fieldOfView = gunCameraPrevPos;
            }
        }

        public void Reload()
        {
            Debug.Log("Reloading...");
        }
    }
}