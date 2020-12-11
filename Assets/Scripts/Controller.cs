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


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;

        score = 0;

        lives = new List<GameObject>();

        Vector2 textPosition = livesText.transform.position;
        Vector2 pos = new Vector2(textPosition.x + .25f, textPosition.y);

        for (int i = 0; i<5; i++)
        {
            
            GameObject newLife = Instantiate(heartPrefab, pos, Quaternion.identity);
            lives.Add(newLife);
            pos.x += newLife.GetComponent<Renderer> ().bounds.size.x;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (gameOver == false)
        {
            gameOverText.text = "";
        }
        else
        {
            gameOverText.text = "Game Over";
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
        }
    }

    public void BabyKilled()
    {
        score++;
        scoreText.text = "Score: " + score;

        gameOver = false;

        int babiesLeft = GameObject.FindGameObjectsWithTag("baby").Length;

        if(babiesLeft <= 0)
        {
            Debug.Log("No Babies Left");

            level++;

            levelText.text = "Level: " + level;
        }
    }
}
