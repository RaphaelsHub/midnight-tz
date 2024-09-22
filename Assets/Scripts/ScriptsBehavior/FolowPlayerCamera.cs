using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class FolowPlayerCamera : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0.8f, 0f);
    [SerializeField] private PlayerController playerController;

    void LateUpdate()
    {
        if (GameManager.gameIsActive && !GameManager.gameIsOver)
            transform.position = playerController.transform.position + offset;
    }
}
