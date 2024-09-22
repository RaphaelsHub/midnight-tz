using UnityEngine;

namespace Interfaces
{
    public interface IPlayerAnimation
    {
        void Move();
        void Jump();
        void RotatePlayer(Vector2 direction);
        void Shoot();
        void Reload();
        void Die();
    }
}