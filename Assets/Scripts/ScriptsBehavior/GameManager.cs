using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image win;
    [SerializeField] private Image loose;
    [SerializeField] private Image ache;
    [SerializeField] private Button restart;


    [SerializeField] private TextMeshProUGUI[] textes;
    [SerializeField] private TextMeshProUGUI[] textesOfTheEnd;

    [SerializeField] private GameObject GameWasStarted;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject screenOfTheEnd;

    [SerializeField] private Timer timer;
    [SerializeField] private GunInventary Inventary;
    [SerializeField] private GetDamage healthh;
    [SerializeField] private StorDataAboutKills data;
    [SerializeField] private AudioClip endOfGame;
    [SerializeField] private AudioSource endOf;

    public static bool gameIsActive = false;
    public static bool gameIsOver = false;
    public static bool playerWon = false;


    private void Start()
    {
        endOf = GetComponent<AudioSource>();
        textes = GameWasStarted.GetComponentsInChildren<TextMeshProUGUI>();
        textesOfTheEnd = screenOfTheEnd.GetComponentsInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (gameIsActive && !gameIsOver)
           outPutUIInfo();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        if(Input.GetMouseButton(1))
            Cursor.lockState = CursorLockMode.Locked;
    }
    public void menu(int selectedMenu)
    {
        switch (selectedMenu)
        {
            case 1:
                startTheGame();
                break;
            case 2:
                exitTheGame();
                break;
            case 3:
                pauseTheGame();
                break;
        }
    }
    private void startTheGame()
    {
        gameIsActive = true;
        titleScreen.gameObject.SetActive(!gameIsActive);
        GameWasStarted.gameObject.SetActive(gameIsActive);
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void pauseTheGame()
    {
        gameIsActive = false;
        GameWasStarted.gameObject.SetActive(gameIsActive);
        titleScreen.gameObject.SetActive(!gameIsActive);
        Cursor.lockState = CursorLockMode.None;
    }
    private void exitTheGame()
    {
        data.Kills = 0;
        data.HeadShots = 0;
        data.Waves = 0;
        Application.Quit();
    }
    public void restartTheGame()
    {
        gameIsOver = false;
        gameIsActive = false;
        playerWon = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
    
    private void outPutUIInfo()
    {
        textes[0].text = "" +healthh.health;
        textes[1].text = "" + Inventary.weapon[Inventary.rememberMyChoseOfWeapon].AmmoPerMag + "/" + Inventary.weapon[Inventary.rememberMyChoseOfWeapon].full * Inventary.weapon[Inventary.rememberMyChoseOfWeapon].AmmountOfMag;
        textes[2].text = "" + Inventary.weapon[Inventary.rememberMyChoseOfWeapon].NameWeapon;
        //textes[3].text = "0" + savedSystem.myProgress.minutes + ":" + savedSystem.secondsInt;
        textes[3].text = string.Format("{0:00}:{1:00}", timer.myTime.minutes, timer.secondsInt);
        textes[4].text = "Wave: " + data.Waves;
    }
    public void gameOver()
    {
        gameIsOver = true;
        titleScreen.SetActive(!gameIsActive);
        GameWasStarted.SetActive(!gameIsActive);
        screenOfTheEnd.SetActive(gameIsActive);
        restart.gameObject.SetActive(gameIsActive);
        endOf.PlayOneShot(endOfGame, 1f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (playerWon)
            win.gameObject.SetActive(true);
        else
            loose.gameObject.SetActive(true);

        outPutSavedSystemProgress();

        Invoke("restartTheGame", 10f);
    }
    private void outPutSavedSystemProgress()
    {
        textesOfTheEnd[0].text = "Time played: 3 minutes ";
        textesOfTheEnd[1].text = "Number of kills: " + data.Kills;
        textesOfTheEnd[2].text = "Number of HeadShots: " + data.HeadShots;
        textesOfTheEnd[3].text = "Number of played waves: " + data.Waves;
        textesOfTheEnd[4].text = "The job was done by Alexandr Pekel. A\n \b Thanks for playing!!!";
    }
}