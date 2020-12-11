using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    public Text scoreText;
    
    public Text levelText;
    
    public Text livesText;

    public Text gameoverText;

    private int level;

    private int lives;

    private int score;

    private bool gameover;

    private int babiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        gameover = false;

        score = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (gameover == false)
        {
            gameoverText.text = "";
        }
        else
        {
            gameoverText.text = "Game Over";
        }

    }

    public void BabyKilled()
    {
        score++;
        scoreText.text = "Score: " + score;

        gameover = false;

        int babiesLeft = GameObject.FindGameObjectsWithTag("babies").Length;

        if(babiesLeft <= 0)
        {
            Debug.Log("No Babies Left");

            level++;

            levelText.text = "Level: " + level;
        }
    }
}
