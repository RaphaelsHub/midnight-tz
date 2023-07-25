using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asas : MonoBehaviour
{
    private GetDamage getDam;

    private void Start()
    {
        getDam = GameObject.Find("Player").GetComponent<GetDamage>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && getDam != null)
        {
            getDam.TakeDamage(5);
            Debug.Log("-5");
        }
    }

}
