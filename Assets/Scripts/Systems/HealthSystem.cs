using UnityEngine;

namespace Systems
{
    public class HealthSystem
    {
        public uint MaxHealth { get; private set; }
        public uint Health { get; private set; }

        public HealthSystem(uint maxHealth)
        {
            MaxHealth = maxHealth;
            Health = MaxHealth;
        }

        public void TakeDamage(uint damage)
        {
            Health = (uint)Mathf.Clamp(Health - damage, 0, MaxHealth);
            if (Health <= 0)
            {
                Debug.Log("Entity is dead!");
                OnDeath();
            }
        }

        public void Heal(uint healAmount)
        {
            Health = (uint)Mathf.Clamp(Health + healAmount, 0, MaxHealth);
        }

        protected virtual void OnDeath()
        {
            Debug.Log("OnDeath called");
        }
    }
}