using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    private float X;
    private float Y;

    [SerializeField] private PlayerController checkConditions;
    [SerializeField] private Animator PlayerAnimation;
    [SerializeField] private Camera watch;

    void Update()
    {
        if (GameManager.gameIsActive && !GameManager.gameIsOver)
        {
            watchAround();
            checkIfIsWalking();
            if (checkConditions.IsJumping)
                StartCoroutine(startJumping());
        }
    }
    private void watchAround()
    {
        if (watch.transform.eulerAngles.x <= 50f && watch.transform.eulerAngles.x >= 30f)
            Y = -1f;
        if (watch.transform.eulerAngles.x <= 30f && watch.transform.eulerAngles.x >= 10f)
            Y = -0.5f;
        if (watch.transform.eulerAngles.x <= 10f && watch.transform.eulerAngles.x >= -10f)
            Y = 0f;
        if (watch.transform.eulerAngles.x <= 350f && watch.transform.eulerAngles.x >= 330f)
            Y = 0.5f;
        if (watch.transform.eulerAngles.x <= 330f && watch.transform.eulerAngles.x >= 310f)
            Y = 1f;
        X = 0;
        PlayerAnimation.SetFloat("X", X);
        PlayerAnimation.SetFloat("Y", Y);
    }
    private void checkIfIsWalking()
    {
        if (checkConditions.PlayerMovementDirection.x != 0 || checkConditions.PlayerMovementDirection.y != 0)
        {
            PlayerAnimation.SetBool("IsGoingToRun", true);
            PlayerAnimation.SetFloat("X", checkConditions.PlayerMovementDirection.x);
            PlayerAnimation.SetFloat("Y", checkConditions.PlayerMovementDirection.y);
        }
        else
        {
            PlayerAnimation.SetBool("IsGoingToRun", false);
        }
    }
    private IEnumerator startJumping()
    {

        PlayerAnimation.SetBool("IsGoingToJump", true);
        yield return new WaitForSeconds(0.05f);
        PlayerAnimation.SetBool("IsGoingToJump", false);
    }
    public void isShooter(bool isShoot)
    {
        PlayerAnimation.SetBool("IsShooting", isShoot);
    }
}
