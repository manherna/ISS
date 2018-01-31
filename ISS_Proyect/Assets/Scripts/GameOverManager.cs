using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SocketComServer s = GameObject.FindObjectOfType<SocketComServer>();
        s.EndServer();
        Application.Quit();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
