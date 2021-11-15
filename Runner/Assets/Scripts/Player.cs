using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Action OnMalusHit = delegate { };
    public Action OnBonusHit = delegate { };
    public Action OnDie = delegate { };
    public int Health { get; private set; } 
    public int Score { get; private set; } 
    public bool IsDied { get; private set; }

    private void Awake()
    {
        Instance = this;
        Health = 3;
        Score = 0;
        IsDied = false;

        OnMalusHit += TakeDamage;
        OnBonusHit += IncrementScore;
    }

    public void TakeDamage()
    {
        if (Health > 0)
            Health--;

        if (!IsDied)
        {
            if (Health <= 0)
            {
                IsDied = true;
                OnDie.Invoke();
                SetMaxScore();
            }
        }
    }

    public void IncrementScore()
    {
        Score++;
    }

    public float GetMaxScore()
    {
        if(PlayerPrefs.HasKey("MaxScore"))
            return PlayerPrefs.GetFloat("MaxScore");
        return 0; 
    }

    private void SetMaxScore()
    {
        if (Score > GetMaxScore())
            PlayerPrefs.SetFloat("MaxScore", Score);
    }

    private void OnDisable()
    {
        OnMalusHit -= TakeDamage;
        OnBonusHit -= IncrementScore;
    }
}
