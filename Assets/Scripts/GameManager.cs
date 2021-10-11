using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player_pink;
    public Player player_cyan;
    public TextMeshProUGUI pink_score;
    public TextMeshProUGUI cyan_score;
    public TextMeshProUGUI endText;

    public int pink_lives;
    public int cyan_lives;
    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        pink_score.text = "Pink Lives: " + player_pink.lives;
        cyan_score.text = "Cyan Lives: " + player_cyan.lives;
        if (player_cyan.lives <= 0 || player_pink.lives <= 0)
        {
            Time.timeScale = 0;
            gameOver = true;
        }
        if (gameOver) {
            endCard();
        }
    }

    void endCard()
    {
        if (player_pink.lives > player_cyan.lives)
        {
            endText.text = "Pink Wins!";
        }
        if (player_cyan.lives > player_pink.lives)
        {
            endText.text = "Cyan Wins!";
        }
        if (player_cyan.lives == player_pink.lives)
        {
            endText.text = "Tragedy!";
        }
    }
}
