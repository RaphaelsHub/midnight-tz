using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource music;
    private AudioSource music1;
    private AudioSource music2;
    private AudioSource music5;
    private AudioSource music6;


    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip phone;
    [SerializeField] private AudioClip reload;
    [SerializeField] private AudioClip scream;
    [SerializeField] private AudioClip breath;

    [SerializeField] GunInventary Inv;

    private void Start()
    {
        music = GetComponent<AudioSource>();
        music1 = GetComponent<AudioSource>();
        music2 = GetComponent<AudioSource>();
        music5 = GetComponent<AudioSource>();
        music6 = GetComponent<AudioSource>();

        InvokeRepeating("zombieAudio", 4f, 7f);
        Invoke("PlayASec", 0.1f);
    }
    void Update()
    {
        if (GameManager.gameIsActive && !GameManager.gameIsOver)
        {
            if (AimShootManager.isShooting)
                music.PlayOneShot(shoot, 0.3f);

            if (music1.isPlaying == false)
                music1.UnPause();

            if (Inv.weapon[Inv.rememberMyChoseOfWeapon].isReloading)
            {
                music2.PlayOneShot(reload, 0.5f);
                Inv.weapon[Inv.rememberMyChoseOfWeapon].isReloading = false;
            }



            if (music6.isPlaying == false)
                music6.UnPause();
        }
        else if (!GameManager.gameIsActive && !GameManager.gameIsOver)
        {
            music1.Pause();
            music6.Pause();
        }
        else if (GameManager.gameIsOver)
        {
            music1.Pause();
            music6.Pause();
        }

    }
    private void PlayASec()
    {
        music1.PlayOneShot(phone, 1f);
        music1.Pause();
        music6.PlayOneShot(breath, 1f);
        music6.Pause();
    }
    private void zombieAudio()
    {
        float volume = Random.Range(0.1f, 0.9f);
        music5.PlayOneShot(scream, volume);
    }
}
