using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    int Wave = 0;
    private int EnemyCount;
    private float spawnRadius = 2f;

    private int[] index = { 5, 7, 10 };
    private int[] healthLvl = { 125, 150, 300 };


    public GameObject ZombiEnemy;
    public StorDataAboutKills Data;
    [SerializeField] GameManager gameManager;

    private bool amItoGoNextLvl = true;
    private void Start()
    {
        Data.Waves = 1;
    }
    private void Update()
    {

        if (GameManager.gameIsActive && !GameManager.gameIsOver)
        {
            if (amItoGoNextLvl)
            {
                StartCoroutine(Starting(index[Wave]));
                amItoGoNextLvl = false;
            }
        }
    }
    private IEnumerator Starting(int countOfEnemies)
    {
        yield return new WaitForSeconds(10);

        for (int i = 0; i < countOfEnemies; i++)
            SpawnInstatiate();

        StartCoroutine(checkForStep());
    }
    private IEnumerator checkForStep()
    {
        yield return new WaitForSeconds(50);

        EnemyCount = FindObjectsOfType<ParametrsZombie>().Length;

        if (EnemyCount != 0)
        {
            GameManager.playerWon = false;
            gameManager.gameOver();
        }

        if (EnemyCount == 0 && Wave == 2)
        {
            GameManager.playerWon = true;
            gameManager.gameOver();
        }
        if (EnemyCount == 0 && Wave < 2)
        {
            Data.Waves++;
        }
        Wave++;
        amItoGoNextLvl = true;
    }
    void SpawnInstatiate()
    {
        Vector3 randomPosition = GetRandomPosition();

        // Продолжаем генерировать новую позицию, пока текущая занята
        while (IsPositionOccupied(randomPosition))
            randomPosition = GetRandomPosition();

        if (!IsPositionOccupied(randomPosition))
        {
            Quaternion randomRot = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            Instantiate(ZombiEnemy, randomPosition, randomRot);

            ParametrsZombie link = ZombiEnemy.gameObject.GetComponent<ParametrsZombie>();
            link.Health = healthLvl[Wave];
        }
    }
    bool IsPositionOccupied(Vector3 position)
    {
        // Проверяем, свободна ли позиция
        Collider[] colliders = Physics.OverlapSphere(position, spawnRadius);

        foreach (Collider collider in colliders)
            if (collider.CompareTag("SpawnArea"))
                return true;

        return false;
    }
    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-10, 10);
        float randomZ = Random.Range(-10, 10);
        float y = 0.2f; // или установите нужное значение высоты

        return new Vector3(randomX, y, randomZ);
    }
}
