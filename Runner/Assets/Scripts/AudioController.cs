using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSourceBurp;

    [SerializeField]
    private AudioSource audioSourceKillSound;

    private void Start()
    {
        Player.Instance.OnBonusHit += PlayBurp;
        Player.Instance.OnMalusHit += PlayKillSound;
    }

    private void PlayBurp()
    {
        audioSourceBurp.Play();
    }

    private void PlayKillSound()
    {
        audioSourceKillSound.Play();
    }

    private void OnDisable()
    {
        Player.Instance.OnBonusHit -= PlayBurp;
        Player.Instance.OnMalusHit -= PlayKillSound;
    }
}
