using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    AudioClip [] musics;
    public AudioClip relax1, relax2;
    public GameObject cam;
    System.Random rnd;

	// Use this for initialization
	void Start () {
        rnd = new System.Random();
        musics = new AudioClip[2];
        musics[0] = relax1;
        musics[1] = relax2;
        if (musics[0] == null) throw new System.Exception("LOKO MUERETE");


        AudioSource src = cam.GetComponent<AudioSource>();
        int aux = rnd.Next(0, 2);
        src.clip = musics[aux];
        Debug.Log("Playing!!");
        src.Play();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
