using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetDamage : MonoBehaviour
{
    [SerializeField] private Image blood;
    [SerializeField] private Animator animator;
    [SerializeField] private GameManager gameManager;
    public static bool isDamaging = false;

    public int health;
    private int maxHealth;

    public int Health
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }
    private void Start()
    {
        health = 150;
        maxHealth = 150;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        isDamaging = true;

        StartCoroutine(bloodImag());

        if (Health <= 0)
            Die();
    }
    private IEnumerator bloodImag()
    {
        blood.gameObject.SetActive(isDamaging);
        yield return new WaitForSeconds(3);
        isDamaging = false;
        blood.gameObject.SetActive(isDamaging);
    }
    private void Die()
    {
        animator.SetTrigger("Dead");
        GameManager.playerWon = false;
        gameManager.gameOver();
    }
}
