using UnityEngine;

namespace Interfaces
{
    public interface IPlayerMovement
    {
        void Move(Vector3 direction);
        void RotatePlayer(Vector2 direction);
        void Jump();
    }
}