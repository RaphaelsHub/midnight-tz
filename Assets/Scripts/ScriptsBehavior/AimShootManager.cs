using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimShootManager : MonoBehaviour
{
    [SerializeField] private Camera gunCamera;
    [SerializeField] private Camera fullCamera;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spotSpawn;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GunInventary shootWithThisWeapon;
    [SerializeField] private AnimationsManager startShootPersonAnim;


    private float speedOfBullet = 100;
    private bool canShoot = true;
    public bool ammoEnoungh = true;
    public bool isAiming = false;
    public static bool isShooting = false;

    private float fullCameraPrevPos;
    private float gunCameraPrevPos;

    private void Start()
    {
        fullCameraPrevPos = fullCamera.fieldOfView;
        gunCameraPrevPos = gunCamera.fieldOfView;
    }
    void Update()
    {
        if (GameManager.gameIsActive && !GameManager.gameIsOver)
        {
            if (Input.GetMouseButton(1))
                zoomCamera();
            if (Input.GetMouseButton(0) && canShoot && !shootWithThisWeapon.weapon[shootWithThisWeapon.rememberMyChoseOfWeapon].notEnoughtAmmo)
                StartCoroutine(delayShoot());
            else
                isShooting = false;

            if (shootWithThisWeapon.weapon[shootWithThisWeapon.rememberMyChoseOfWeapon].notEnoughtAmmo)
                ammoEnoungh = false;
        }
    }
    private void zoomCamera()
    {
        if (Input.GetMouseButtonDown(1) && !isAiming)
        {
            isAiming = true;
            fullCamera.fieldOfView = 20;
            gunCamera.fieldOfView = 40;
        }
        else if (Input.GetMouseButtonDown(1) && isAiming)
        {
            isAiming = false;
            fullCamera.fieldOfView = fullCameraPrevPos;
            gunCamera.fieldOfView = gunCameraPrevPos;
        }
    }
    private IEnumerator delayShoot()
    {
        shoot();
        startShootPersonAnim.isShooter(canShoot);
        canShoot = false;

        yield return new WaitForSeconds(shootWithThisWeapon.weapon[shootWithThisWeapon.rememberMyChoseOfWeapon].DelayAfterShoot);
        startShootPersonAnim.isShooter(canShoot);
        canShoot = true;
    }
    private void shoot()
    {
        Ray rayGun = fullCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 target;

        if (Physics.Raycast(rayGun, out hit))
            target = hit.point;
        else
            target = rayGun.GetPoint(100);

        Vector3 direction = target - spotSpawn.position;

        GameObject bul = Instantiate(bullet, spotSpawn.position, bullet.transform.rotation);

        bul.transform.forward = direction.normalized;

        isShooting = true;

        shootWithThisWeapon.weapon[shootWithThisWeapon.rememberMyChoseOfWeapon].Shoot();

        bul.GetComponent<Rigidbody>().AddForce(direction.normalized * speedOfBullet, ForceMode.Impulse);
    }
}
