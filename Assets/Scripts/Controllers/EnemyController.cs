using Systems;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    // Наш злодей может только бегать и атаковать
    public class EnemyController : MonoBehaviour
    {
        // Components
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform playerPos;
        [SerializeField] public LayerMask playerMask;
        [SerializeField] private int watchingRange = 7;
        [SerializeField] private int attackingRange = 1;
        [SerializeField] private bool hasFoundPoint;
        [SerializeField] private Transform randomPoint;
        private HealthSystem healthSystem;
        private static readonly int Run = Animator.StringToHash("run");
        private static readonly int IsAttack = Animator.StringToHash("isAttack");
        
        private Vector3 lastPlayerPosition;


        // Start is called before the first frame update
        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            playerPos = GameObject.FindWithTag("Player").transform; // Используем тег вместо имени
        }
        
        private void Start()
        {
            healthSystem = new HealthSystem(150);
        }

        private void Update()
        {
            var isPlayerNear = Physics.CheckSphere(transform.position, watchingRange, playerMask); 
            var canEnemyAttack = Physics.CheckSphere(transform.position, attackingRange, playerMask);

            if (!canEnemyAttack && !isPlayerNear)
                FindEnemy();
            else if (isPlayerNear && !canEnemyAttack)
                ChaseEnemy();
            else
                AttackEnemy();
        }

        private void AttackEnemy()
        {
            if ((playerPos.position - lastPlayerPosition).sqrMagnitude > 0.1f) // Обновляем только при изменении позиции
            {
                agent.SetDestination(playerPos.position);
                lastPlayerPosition = playerPos.position;
            }
            
            if (!animator.GetBool(IsAttack)) // Избегаем лишних вызовов
            {
                animator.SetBool(IsAttack, true);
                playerPos.GetComponent<PlayerController>().TakeDamage(10);
                animator.ResetTrigger(Run);
            }
        }

        private void ChaseEnemy()
        {
            if ((playerPos.position - lastPlayerPosition).sqrMagnitude > 0.1f) // Обновляем только при изменении позиции
            {
                agent.SetDestination(playerPos.position);
                lastPlayerPosition = playerPos.position;
            }

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("run")) // Избегаем повторных триггеров
            {
                animator.SetTrigger(Run);
            }

            animator.SetBool(IsAttack, false);
        }

        private void FindEnemy()
        {
            if (!hasFoundPoint)
            {
                randomPoint.position = new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5));
                hasFoundPoint = true;
            }
            agent.SetDestination(randomPoint.position);

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("run")) // Проверка текущей анимации
            {
                animator.SetTrigger(Run);
            }

            if ((transform.position - randomPoint.position).magnitude < 1f)
                hasFoundPoint = false;
        }

        public void TakeDamage(uint damage)
        {
             healthSystem.TakeDamage(damage);
        }
    }
}
