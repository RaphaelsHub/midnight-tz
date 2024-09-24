using System;
using UnityEngine;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        private GameObject indexPage; 
        private GameObject gamePage;
        private GameObject deathPage;
    
        public bool IsGameStarted { get; private set; }
        public bool IsPLayerDead { get; set; }

        private void Awake()
        {
            indexPage = GetComponentInChildren<Transform>().Find("IndexPage").gameObject;
            gamePage = GetComponentInChildren<Transform>().Find("GamePage").gameObject;
            deathPage = GetComponentInChildren<Transform>().Find("DeathPage").gameObject;
            PauseTheGame(); 
        }
    
        private void Update()
        {
            if(Input.GetMouseButton(1))
                Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetKeyDown(KeyCode.Escape))
                Cursor.lockState = CursorLockMode.None;
        }
    
        public void Menu(int selectedMenu)
        {
            switch (selectedMenu)
            {
                case 1:
                    StartTheGame();
                    break;
                case 2:
                    ExitTheGame();
                    break;
                case 3:
                    PauseTheGame();
                    break;
                case 4:
                    RestartTheGame();
                    break;
                case 5:
                    EndGame();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(selectedMenu), selectedMenu, null);
            }
        }

        private void StartTheGame()
        {
            indexPage.SetActive(false);
            gamePage.SetActive(true);
            deathPage.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            IsGameStarted = true;
        }
    
        private void PauseTheGame()
        {
            indexPage.SetActive(true);
            gamePage.SetActive(false);
            deathPage.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            IsGameStarted= false;
        }
    
        private void RestartTheGame() => StartTheGame();

        private void ExitTheGame()
        {
            Application.Quit();
        }

        private void EndGame()
        {
            indexPage.SetActive(false);
            gamePage.SetActive(false);
            deathPage.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            IsGameStarted = false;
            GameObject photo = IsPLayerDead ? deathPage.GetComponentInChildren<Transform>().Find("Defeat").gameObject : 
                deathPage.GetComponentInChildren<Transform>().Find("Victory").gameObject;
            photo.SetActive(true);
            Invoke(nameof(RestartTheGame), 10f);
            photo.SetActive(false);
        }
    }
}