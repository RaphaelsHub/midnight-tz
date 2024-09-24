using UnityEngine;

namespace ScriptsBehavior
{
    public class SpawnEnemy : MonoBehaviour
    {
        private readonly float spawnRadius = 2f;
        [SerializeField] private GameObject enemy;
        
        void Start()
        {
            InvokeRepeating(nameof(SpawnInstantiate), 3, 10);
        }
    
        void SpawnInstantiate()
        {
            Vector3 randomPosition = GetRandomPosition();

            while (IsPositionOccupied(randomPosition))
                randomPosition = GetRandomPosition();

            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            Instantiate(enemy, randomPosition, randomRotation);
        }

        bool IsPositionOccupied(Vector3 position)
        {
            Collider[] colliders = Physics.OverlapSphere(position, spawnRadius);

            foreach (Collider col in colliders)
                if (col.CompareTag("SpawnArea"))
                    return true;

            return false;
        }

        Vector3 GetRandomPosition()
        {
            float randomX = Random.Range(-10, 10);
            float randomZ = Random.Range(-10, 10);
            float y = 0.2f;

            return new Vector3(randomX, y, randomZ);
        }
    }
}