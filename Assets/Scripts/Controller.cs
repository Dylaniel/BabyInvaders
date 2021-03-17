using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Controller : MonoBehaviour
{
    public Text scoreText, levelText, gameOverText, livesText;

    private int level;
    private int score;
    public bool gameOver;
    private float spawnRate;
    public int [] nBabies;

    public GameObject root;
    public GameObject[] babies;
    public GameObject heartPrefab;
    private List<GameObject> lives = new List<GameObject>();

    private Basket basket;
    
    public GameObject manager;
    Manager managerScript;
    private AsyncOperation sceneLoading;

    private void OnEnable()
    {
        nBabies = new int[4];

        managerScript = manager.GetComponent<Manager>();

        gameOverText.text = "";
        gameOver = false;
        score = 0;
        
        Vector2 textPosition = livesText.transform.position;
        Vector2 pos = new Vector2(textPosition.x + .25f, textPosition.y);
        for (int i = 0; i < 5; i++)
        {
            GameObject newLife = Instantiate(heartPrefab, pos, Quaternion.identity);
            lives.Add(newLife);
            pos.x += newLife.GetComponent<Renderer>().bounds.size.x;

            newLife.transform.parent = managerScript.HUD.transform;
        }

        activateFirstLevel();

        Debug.Log("controller is enabled");
    }

    private void OnDisable()
    {
        foreach (GameObject life in lives)
        {
            Destroy(life);
        }

        lives.Clear();

        Debug.Log("controller is disabled");
    }

    public void SetDifficulty(string difficulty)
    {
        if (difficulty == "Easy")
        {
            spawnRate = 1.5f;

            nBabies[0] = 10;
            nBabies[1] = 20;
            nBabies[2] = 25;
            nBabies[3] = 25;
        }
        else if (difficulty == "Medium")
        {
            spawnRate = 1f;
            
            nBabies[0] = 10;
            nBabies[1] = 25;
            nBabies[2] = 30;
            nBabies[3] = 30;
        }
        else if (difficulty == "Hard")
        {
            spawnRate = .75f;

            nBabies[0] = 10;
            nBabies[1] = 30;
            nBabies[2] = 40;
            nBabies[3] = 40;
        }
    }

    private void activateFirstLevel()
    {
        level = 1;
        activateNextLevel();
    }

    void OnGUI()
    {
        if (managerScript.CurrentState != GameState.paused)
        {
            if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
            {
                Debug.Log("Escape key is pressed.");

                managerScript.ShowPause();

                
                babies = GameObject.FindGameObjectsWithTag("baby");
            }
        }
    }

    public void Pause()
    {
        root.SetActive(false);
    }

    public void UnPause()
    {
        root.SetActive(true);
    }

    private void FindRoot()
    {
        root = GameObject.FindGameObjectWithTag("Root");
    }

    private void findBasket()
    {
        GameObject basketObject = GameObject.Find("Basket");

        basket = basketObject.GetComponent<Basket>();

        basket.ApplyDifficulty(nBabies[level-2],spawnRate);
    }

    private void Update()
    {
        if (sceneLoading != null && sceneLoading.isDone)
        {
            findBasket();
            FindRoot();
            sceneLoading = null;
        }
    }

    private void activateNextLevel()
    {
        if (level <= 4)
        {
            if (level == 1)
            {
                sceneLoading = managerScript.ChangeScene("Laundry Room");
            }
            else if (level == 2)
            {
                sceneLoading = managerScript.ChangeScene("Kitchen Room");
            }
            else if (level == 3)
            {
                sceneLoading = managerScript.ChangeScene("Living Room");
            }
            else if (level == 4)
            {
                sceneLoading = managerScript.ChangeScene("Hallway Room");
            }

            level++;
            levelText.text = "Level: " + (level - 1);
        }
        else
        {
            gameOverText.text = "you won";
            Debug.Log("you won!!");
            gameOver = true;
            managerScript.TheGameIsOver();
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
