using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text maxScore;

    [SerializeField]
    private List<GameObject> hearts = new List<GameObject>();

    [SerializeField]
    private Button RestartButton;

    [SerializeField]
    private Canvas canvasInGame;

    [SerializeField]
    private Canvas canvasDeath;

    private void Start()
    {
        SetMaxScoreText();
        Player.Instance.OnBonusHit += SetScoreText;
        Player.Instance.OnMalusHit += SetHeartUI;
        Player.Instance.OnDie += ShowDeathScreen;
        Player.Instance.OnDie += SetMaxScoreText;
        RestartButton.onClick.AddListener(GameManager.Instance.StartGame);
    }

    private void ShowDeathScreen()
    {
        canvasInGame.gameObject.SetActive(false);
        canvasDeath.gameObject.SetActive(true);
    }

    private void SetHeartUI()
    {
        if(hearts.Count > 0)
        {
            hearts[hearts.Count - 1].SetActive(false);
            hearts.RemoveAt(hearts.Count - 1);
        }
    }

    private void SetScoreText()
    {
        scoreText.text = Player.Instance.Score.ToString();
    }

    private void SetMaxScoreText()
    {
        maxScore.text = Player.Instance.GetMaxScore().ToString();
    }

    private void OnDisable()
    {
        Player.Instance.OnBonusHit -= SetScoreText;
        Player.Instance.OnMalusHit -= SetHeartUI;
        Player.Instance.OnDie -= ShowDeathScreen;
        Player.Instance.OnDie -= SetMaxScoreText;
    }
}
