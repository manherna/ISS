/*
 * Script used for changing the scene between Rest and Task scenes.
 * After that it will go to GameOver Scene.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    int [,] sTimes = null;
    int actScene = -1;
    SocketComServer com;
    private string currentScene;
    void Awake()
    {
        DontDestroyOnLoad(this);
        System.Console.WriteLine("IEE");
    }

    // Use this for initialization
    void Start () {
        com = GameObject.FindObjectOfType<SocketComServer>();
        currentScene = "START";
	}
	
	// Update is called once per frame
	void Update () {		
	}

    //Function to update the Stimes array with new values.
    public void updateScenes(int[,] t)
    {
        sTimes = t;
        actScene = 0;
        changeScene();
    }

    // Function to change scene after some time.
    void changeScene()
    {
        if (actScene < sTimes.GetLength(0))
        {
            if (sTimes[0, actScene] == 0)
            {
                Invoke("changeToRest", 0);

            }
            else if (sTimes[0, actScene] == 1)
            {
                Invoke("changeToTask", 0);

            }
            com.OnSceneEnd(currentScene);
        }
        else
        {
            Invoke("changeToGameOver", 0);
            com.EndServer();
            
        }

    }
    void changeToTask() {
        
        SceneManager.LoadScene("TaskScene", LoadSceneMode.Single);
        com.OnSceneStart("TASK");
        currentScene = "TASK";
        Invoke("changeScene", sTimes[1, actScene]);
        actScene++;
    }
    void changeToRest()
    {
        SceneManager.LoadScene("RestScene", LoadSceneMode.Single);
        com.OnSceneStart("REST");
        currentScene = "REST";
        Invoke("changeScene", sTimes[1, actScene]);
        actScene++;
    }
    void changeToGameOver()
    {
        Debug.Log("gameoverrr");
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        actScene++;
    }

}
