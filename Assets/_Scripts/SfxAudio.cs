using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxAudio : MonoBehaviour
{
    public AudioSource ShootAudioSource;
    public AudioSource ShootNlo;
    public AudioSource DieSpaceShip;
    public AudioSource DieNlo;
    public AudioSource DieAsteroid;

    public static SfxAudio _instance;

    public void Awake()
    {
        _instance = this;
    }
}
