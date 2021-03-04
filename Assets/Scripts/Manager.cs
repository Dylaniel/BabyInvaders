﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    menu,
    playing,
    paused,
    credits
}

public class Manager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject MainMenu;
    public GameObject Pause;
    public GameObject Credits;

    public GameObject ControllerObj;

    Controller controllerScript;

    private string currentScene;


    private GameState state;
    public GameState CurrentState
    {
        get { return state; }
    }


    // Start is called before the first frame update
    void Start()
    {
        controllerScript = ControllerObj.GetComponent<Controller>();

        GoToMainMenu();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        HUD.SetActive(false);
        Pause.SetActive(false);
        MainMenu.SetActive(true);
        Credits.SetActive(false);

        state = GameState.menu;

        ControllerObj.SetActive(false);

        UnloadScene(currentScene);
    }

    public void GoToPlay()
    {
        HUD.SetActive(true);
        Pause.SetActive(false);
        MainMenu.SetActive(false);
        Credits.SetActive(false);

        state = GameState.playing;

        ControllerObj.SetActive(true);
    }

    public void ShowPause()
    {
        HUD.SetActive(true);
        Pause.SetActive(true);
        MainMenu.SetActive(false);
        Credits.SetActive(false);

        state = GameState.paused;

        controllerScript.Pause();
    }

    public void HidePause()
    {
        HUD.SetActive(true);
        Pause.SetActive(false);
        MainMenu.SetActive(false);
        Credits.SetActive(false);

        state = GameState.playing;

        controllerScript.UnPause();
    }

    public void GoToCredits()
    {
        HUD.SetActive(false);
        Pause.SetActive(false);
        MainMenu.SetActive(false);
        Credits.SetActive(true);

        state = GameState.credits;
    }

    public AsyncOperation ChangeScene(string newScene)
    {
        Debug.Log("Changing to scene " + newScene);

        if (currentScene != null)
        {
            StartCoroutine(UnloadScene(currentScene));
        }

        // save the new scene name
        currentScene = newScene;

        // load the new scene
        return SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

    }

    IEnumerator UnloadScene(string oldSceneName)
    {
        // wait a brief amount of time
        yield return new WaitForEndOfFrame();

        // unload the target scene
        // (can't do this inside an Update() or similar function because the game will freeze!)
        SceneManager.UnloadSceneAsync(oldSceneName);
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        SceneManager.SetActiveScene(scene);
    }

    internal void TheGameIsOver()
    {
        Invoke("GoToCredits", 5f);
    }
}
