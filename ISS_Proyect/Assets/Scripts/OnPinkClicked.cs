using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPinkClicked : MonoBehaviour {
    public GameManager2 gameManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        gameManager.pinkClicked(this.gameObject, Input.mousePosition.x, Input.mousePosition.y);
    }
}
