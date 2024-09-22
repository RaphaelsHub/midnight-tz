using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class PlayerStaticAfterAnimations : MonoBehaviour
{
    private Vector3 offsetEuler = new Vector3(0, 50f, 0);
    private Vector3 offset = new Vector3(0, -1f, 0);
    [SerializeField] private PlayerController playerController;


    void LateUpdate()
    {
        if (GameManager.gameIsActive && !GameManager.gameIsOver)
        {
            transform.rotation = Quaternion.Euler(playerController.transform.rotation.eulerAngles + offsetEuler);
            transform.position = playerController.transform.position + offset;
        }
    }
}