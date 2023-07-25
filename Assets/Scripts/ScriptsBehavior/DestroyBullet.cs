using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyBullet : MonoBehaviour
{
    private int Damage;
    private ParametrsZombie transferDamage;
    private EnemyAI enemy;

    private void Start()
    {
        Destroy(gameObject, 2);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Head"))
            Damage = 999;
        else if (collision.gameObject.CompareTag("Body"))
            Damage = 25;
        else if (collision.gameObject.CompareTag("Legs"))
            Damage = 10;
        else
        {
            // Если пуля сталкивается с другими объектами без тегов "Head", "Body" или "Legs", уничтожаем пулю
            Destroy(gameObject);
            return; 
        }
        transferDamage = collision.gameObject.GetComponentInParent<ParametrsZombie>();
        transferDamage.TakeDamage(Damage);
        Destroy(gameObject);
    }
}
