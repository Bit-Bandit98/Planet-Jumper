using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource SoundSource, SoundSource2;
    [SerializeField]
    AudioListener Listener;
    public static SoundManager Singleton;
    [SerializeField]
    public AsteroidSounds ASounds;

    [SerializeField]
    AudioClip MoneyGet, PlanetReached, LevelUp, Error, Button, TurretGet;

    [System.Serializable]
    public class AsteroidSounds
    {

     [SerializeField]
     AudioClip HitSound, DestroySound;

     public void PlayHitSound()
     {
         Singleton.SoundSource.pitch = 1;
         Singleton.SoundSource.PlayOneShot(HitSound);
           
           
     }

    public void PlayDestroySound()
     {
            Singleton.SoundSource.pitch = 1;
            Singleton.SoundSource.PlayOneShot(DestroySound);
            
     }


    }

    public void PlayMoneySound()
    {

        Singleton.SoundSource2.pitch = 1.33f;
        Singleton.SoundSource2.PlayOneShot(MoneyGet);
    }


    void Awake(){ Singleton = this; }

    public void PlayLevelUpSound()
    {
        Singleton.SoundSource.pitch = 1;
        SoundSource.PlayOneShot(LevelUp);
    }

    public void PlayPlanetReachedSound()
    {
        Singleton.SoundSource.pitch = 1f;
        SoundSource.PlayOneShot(PlanetReached);
    }

    public void PlayTurretGet()
    {
        SoundSource.PlayOneShot(TurretGet);
    }

    public void PlayErrorSound()
    {
        Singleton.SoundSource.pitch = 1f;
        SoundSource.PlayOneShot(Error);
    }

    public void PlayButtonSound()
    {
        Singleton.SoundSource.pitch = 1f;
        SoundSource.PlayOneShot(Button);
    }

    public void ChangeMasterAudio(float NewValue)
    {
        AudioListener.volume = NewValue;
    }
}
