using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public  AudioClip pistol, shotgun, sniper;
    public  AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pistol = Resources.Load<AudioClip>("PistolSound");
        shotgun = Resources.Load<AudioClip>("ShotgunSound");
        sniper = Resources.Load<AudioClip>("SniperSound");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public  void PlayPistol()
    {
        audioSrc.PlayOneShot(pistol);
    }

    public  void PlayShotgun()
    {
        audioSrc.PlayOneShot(shotgun);
    }

    public  void PlaySniper()
    {
        audioSrc.PlayOneShot(sniper);
    }
}
