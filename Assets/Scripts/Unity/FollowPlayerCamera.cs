using Controllers;
using UnityEngine;

namespace Unity
{
    public class FollowPlayerCamera : MonoBehaviour
    {
        private readonly Vector3 offset = new Vector3(0, 0.8f, 0f);
        private PlayerController playerController;

        private void Awake()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        void LateUpdate()
        {
            transform.position = playerController.transform.position + offset;
        }
    }
}
