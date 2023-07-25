using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorGun : MonoBehaviour
{
    private AimShootManager infoShoot;
    [SerializeField] private Animator Gun;

    private void Awake()
    {
        infoShoot = GameObject.FindGameObjectWithTag("Player").GetComponent<AimShootManager>();
    }

    void Update()
    {
        if (GameManager.gameIsActive && !GameManager.gameIsOver)
        {
            //если прицеливаемся, анимация
            if (infoShoot.isAiming)
                Gun.SetBool("IsZooming", true);
            else
                Gun.SetBool("IsZooming", false);

            //Анимация выстрела только, когда стреляет и есть патроны
            if (AimShootManager.isShooting && infoShoot.ammoEnoungh)
                Gun.SetBool("isShooting", true);
            else
                Gun.SetBool("isShooting", false);
        }
    }
}
