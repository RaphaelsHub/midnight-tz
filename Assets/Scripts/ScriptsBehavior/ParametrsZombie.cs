using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametrsZombie : MonoBehaviour
{
    int global;
    public int health;
    public int maxHealth;
    private AudioSource mus;
    public AudioClip die;
    private Animator animator;
    [SerializeField ]private StorDataAboutKills count;

    public int Health
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = Mathf.Max(0, value);
            Health = Mathf.Max(health, maxHealth);
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        mus = GetComponent<AudioSource>();
        MaxHealth = 300;
    }
    public void TakeDamage(int damage)
    {
        if (damage == 999)
            Die();

        else
        {
            Health -= damage;
            if (Health <= 0)
                Die();
        }

        global = damage;
    }
    private void Die()
    {
        animator.SetTrigger("death");
        mus.PlayOneShot(die, 0.3f);
        count.Kills++;
        if (global > 200)
            count.HeadShots++;
        Destroy(gameObject);
    }
}

