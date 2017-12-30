using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour {
    public GameObject pinkRect;
    public Text txt;
    public AudioClip aux;
    private AudioSource source;
    float startTime;
    float acc;
    public float cicleTime;
    int clicked;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        clicked = 0;
        txt.text = "";
        source = GetComponent<AudioSource>();
        source.PlayOneShot(aux);
       

        for(int x = 0; x < 7; x++)
        {
            Vector3 aux = new Vector3(Mathf.Sin(Random.Range(-Screen.width, Screen.width)), x);
            GameObject.Instantiate(pinkRect, aux, Quaternion.identity);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        acc += Time.time - startTime;
        if (acc > cicleTime)
        {
            acc = 0;
        }
		
	}
    public void pinkClicked(GameObject a,float x, float y)
    {
        Debug.Log("Clicked at" + x + " " + y);
        clicked++;
        Debug.Log(clicked);
        txt.text = clicked.ToString();
        Destroy(a);
    }
}
