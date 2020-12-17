using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    public Text scoreText;
    
    public Text levelText;

    public Text gameOverText, livesText;

    private int level;

    private int score;

    public bool gameOver;

    public GameObject heartPrefab;

    private List<GameObject> lives;

    public GameObject[] levels;

    private Basket basket;



    // Start is called before the first frame update
    void Start()
    {
        gameOverText.text = "";

        gameOver = false;
        score = 0;
        level = -1;
        lives = new List<GameObject>();

        Vector2 textPosition = livesText.transform.position;
        Vector2 pos = new Vector2(textPosition.x + .25f, textPosition.y);
        for (int i = 0; i < 5; i++)
        {
            GameObject newLife = Instantiate(heartPrefab, pos, Quaternion.identity);
            lives.Add(newLife);
            pos.x += newLife.GetComponent<Renderer>().bounds.size.x;
        }

        foreach (GameObject goLevel in levels)
        {
            goLevel.SetActive(false);
        }

        activateNextLevel();
    }

    private void findBasket()
    {
        GameObject currentLevel = levels[level];
        basket = GameObject.Find(currentLevel.name + "/Basket").GetComponent<Basket>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void activateNextLevel()
    {
        if (level < (levels.Length - 1))
        {
            if (level >= 0)
            {
                levels[level].SetActive(false);
            }

            level++;
            levels[level].SetActive(true);
            levelText.text = "Level: " + (level + 1);

            findBasket();
        }
        else
        {
            gameOverText.text = "you won";
            Debug.Log("you won!!");
            gameOver = true;
        }
    }

    public void EmmaHit()
    {
        Debug.Log("emma was hit");

        if(lives.Count > 0)
        {

            GameObject[] lifeObjects = lives.ToArray();
            GameObject lifeGone = lifeObjects[lifeObjects.Length - 1];
            Destroy(lifeGone);
            lives.Remove(lifeGone);
        }
        if (lives.Count == 0)
        {
            gameOver = true;
            gameOverText.text = "Game Over";
        }
    }

    public void BabyEscaped()
    {
        checkEndOfLevel("baby escaped");
    }

    public void BabyKilled()
    {
        score++;
        scoreText.text = "Score: " + score;

        checkEndOfLevel("baby killed");
    }

    private void checkEndOfLevel (string message)
    {
        int babiesLeft = GameObject.FindGameObjectsWithTag("baby").Length;
        Debug.Log(message + ", babies left=" + babiesLeft);

        if (basket.BabiesDone && babiesLeft <= 0)
        {
            Debug.Log("No Babies Left");

            activateNextLevel();
        }
    }
}
