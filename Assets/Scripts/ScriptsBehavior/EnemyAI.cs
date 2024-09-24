/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Animator animator;
    public NavMeshAgent agent; //enemyController
    public Transform player; //playersPos
    public AudioClip getDamage;
    private AudioSource music3;


    public LayerMask playerMask;

    public Vector3 walkVector;
    public bool hasFoundVectorPoint;
    private GetDamage playerDamage;


    private bool isAttacking = false;
    private float timeAttack;

    private float watchingRange = 7;
    private float attackingRange = 0.5f;
    public bool playerAbleWatching;
    public bool playerAbleToAtacking;

    public bool isDead = false;

   private void Start()
    {
        player = GameObject.Find("Player").transform;
        playerDamage = GameObject.Find("Player").GetComponent<GetDamage>();
        animator = GetComponent<Animator>();
        music3 = GetComponent<AudioSource>();
    }

    private void Update()
    {
            playerAbleWatching = Physics.CheckSphere(transform.position, watchingRange, playerMask); //Позиция, радиус, и слой
            playerAbleToAtacking = Physics.CheckSphere(transform.position, attackingRange, playerMask);

            if (!playerAbleToAtacking && !playerAbleWatching)
                findMeatPlayer();
            if (playerAbleWatching && !playerAbleToAtacking)
                runAfterMeat();
            if (playerAbleToAtacking && playerAbleWatching)
                eatMeat();
    }


    private void findMeatPlayer()
    {
        if (!hasFoundVectorPoint)
            getRandomNextPoint();
        else
            agent.SetDestination(walkVector);
        animator.SetTrigger("run");

        Vector3 distance = transform.position - walkVector;

        if (distance.magnitude <1f )
            hasFoundVectorPoint = false;
    }

    private void getRandomNextPoint()
    {
        float RandomZ = Random.Range(-5,5);
        float RandomX = Random.Range(-5, 5);

        walkVector = new Vector3 (transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

       hasFoundVectorPoint = true;
    }
    private void runAfterMeat()
    {
        animator.SetTrigger("run");
        agent.SetDestination(player.position);
   //     transform.LookAt(player);
    }
    private void eatMeat()
    {
        agent.SetDestination(transform.position);
         //transform.LookAt(player);

        if(!isAttacking)
        {
            StartCoroutine(wait());
            /// ....
            /// 

        }
        else
        {
            animator.SetBool("isAttack", false);
            Invoke("Reset", timeAttack);
        }

    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        playerDamage.TakeDamage(5);
        animator.SetBool("isAttack", true);
        music3.PlayOneShot(getDamage, 0.4f);
        isAttacking = true;
    }
    private void Reset()
    {
        isAttacking = false;
    }
}
*/