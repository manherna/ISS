
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class GameManager2 : MonoBehaviour {

    public GameObject pinkRect;
    public AudioClip aux;
    private AudioSource source;
    public SceneLoader sLoader;

    public float IT = 1.0f;
    public float MCT =1.1f;
    float T;
    int N;
    int clicked;
    Vector2 pinkSpriteSize;
    GameObject[] objs;

    // Use this for initialization
    void Start () {
        try
        {
            sLoader = GameObject.FindObjectOfType<SceneLoader>();
        }
        catch (Exception e) { }

        T = sLoader.getTaskTime();

        //Initialization of our ecuation N = (T-IT)/MCT
        N = (int)((T - IT) / MCT);

        Debug.Log("T is : " + T + "N is : " + N);

        pinkSpriteSize = new Vector2(pinkRect.GetComponent<SpriteRenderer>().size.x, pinkRect.GetComponent<SpriteRenderer>().size.y);
        Debug.Log(pinkSpriteSize.x + " " + pinkSpriteSize.y);
       

        sceneSet();

        Debug.Log(N + " squares generated");


        clicked = 0;
    //    source = GetComponent<AudioSource>();

    //    source.PlayOneShot(aux);

      
		
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}
    void sceneSet()
    {
        System.Random rnd = new System.Random();
        objs = new GameObject[N];
        Camera c = Camera.main;
        Vector3 p;
        Vector2 min, max;
        min.x = c.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + pinkSpriteSize.x;
        max.x = c.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - pinkSpriteSize.x;
        min.y = c.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + pinkSpriteSize.y;
        max.y = c.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - pinkSpriteSize.y;

        Debug.Log("The minimum is: " + min.x + " " + min.y + " " + ". Maximum is: " + max.x + " " + max.y);
        float l, r;
        Debug.Log(-min.x + max.x);
        l = (-min.x + max.x) /(3*N);
        float x = min.x;

        Debug.Log("L: " + l + " X: " + x);
        for (int i = 0; i < N; i++)
        {
            Debug.Log(x);
            int aux = rnd.Next((int)(x+=l),(int)(x+=2*l));
            int aux2 = rnd.Next((int)min.y, (int)max.y);
            p = new Vector3(aux, aux2, 0);

            objs[i] = GameObject.Instantiate(pinkRect, p, Quaternion.identity);
 
        }




    }

    public void pinkClicked(GameObject a,float x, float y)
    {
        Debug.Log("Clicked at" + x + " " + y);
        clicked++;
        Debug.Log(clicked);
        Destroy(a);
    }
}
