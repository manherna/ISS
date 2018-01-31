using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMusicManager : MonoBehaviour {
    public AudioClip startles, scream, shots;
    AudioSource [] audioSources;
    System.Random rnd;
    System.Random rnd2;

    // Use this for initialization
    void Start () {
        if (startles == null) throw new System.Exception("Startles not loaded");
        if (scream == null) throw new System.Exception("Scream not loaded");
        if (shots == null) throw new System.Exception("Shots not loaded");

        audioSources = GetComponents<AudioSource>();

        //Initializing background sound
        audioSources[0].clip = startles;
       // audioSources[0].loop = true;
        audioSources[0].Play();


        audioSources[1].clip = scream;
        audioSources[2].clip = shots;

        audioSources[2].loop = audioSources[1].loop = false;

        rnd = new System.Random();
        rnd2= new System.Random();

        playScream();
        Invoke("playShots", (float)rnd.Next(1, 2));
    }
    //Functions for playing
   void playScream()
    {
        audioSources[1].Play();
        Invoke("playScream", (float)rnd.Next(3, 6));
    }
    void playShots()
    {
        audioSources[2].Play();
        Invoke("playShots", (float)rnd2.Next(2, 5));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
