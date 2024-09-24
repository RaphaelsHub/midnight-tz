/*using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimatorGun : MonoBehaviour
{
    [SerializeField] private AimController aimController;
    [SerializeField] private Animator gunAnimator;
    private static readonly int IsShooting = Animator.StringToHash("isShooting");
    private static readonly int IsZooming = Animator.StringToHash("IsZooming");

    private void Awake()
    {
        aimController = GameObject.FindGameObjectWithTag("Player").GetComponent<AimController>();
    }

    void Update()
    {
        if(aimController.IsShooting) gunAnimator.SetBool(IsShooting, true);
        else gunAnimator.SetBool(IsShooting, false);
        
        if(aimController.IsAiming) gunAnimator.SetBool(IsZooming, true);
        else gunAnimator.SetBool(IsZooming, false);
    }
}
*/
