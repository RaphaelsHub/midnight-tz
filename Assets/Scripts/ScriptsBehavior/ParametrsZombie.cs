/*using UnityEngine;

namespace ScriptsBehavior
{
    public class Zombie : MonoBehaviour
    {
        private int health;
        private int maxHealth;
        public int Health
        {
            get => health;
            set => health = Mathf.Clamp(value, 0, maxHealth);
        }
        public int MaxHealth
        {
            get => maxHealth;
            set
            {
                maxHealth = Mathf.Max(0, value);
                Health = Mathf.Max(health, maxHealth);
            }
        }
        
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Die();
        }
        
        private void Die()
        {
            Destroy(gameObject);
        }
    }
}

*/