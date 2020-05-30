using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image liveImageDisplay;
    public Text scoreText;
    public GameObject main;
    public GameObject player;
   

    public int score;
    public int points = 10;
    public string resul;
    public string text = "Score: ";

    public void UpdateLives(int currentLives)
    {
        liveImageDisplay.sprite = lives[currentLives];
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void LoseScore()
    {
        score -= points;
        scoreText.text = "Score: " + score;
    }

    public void ShowMainMenu()
    {
        main.SetActive(true);
    }

    public void HideMainMenu()
    {
        main.SetActive(false);
        scoreText.text = "Score: ";
        points = 10;
    }
}
