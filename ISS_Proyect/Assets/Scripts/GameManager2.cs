
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
    public SocketComServer server;

    public float IT = 1.0f;
    public float MCT =1.1f;
    float T;
    int N;
    int goodClicked;
    int clicksMade;
    Vector2 pinkSpriteSize;
    GameObject[] objs;
    bool started;

    // Use this for initialization
    void Start () {
        try
        {
            sLoader = GameObject.FindObjectOfType<SceneLoader>();
            server = GameObject.FindObjectOfType<SocketComServer>();
        }
        catch (Exception e) { }


        started = false;
        T = 15;// sLoader.getTaskTime();

        //Initialization of our ecuation N = (T-IT)/MCT
        N = (int)((T - IT) / MCT);

        pinkSpriteSize = new Vector2(pinkRect.GetComponent<SpriteRenderer>().size.x, pinkRect.GetComponent<SpriteRenderer>().size.y);

        sceneSet();
        //    source = GetComponent<AudioSource>();
        //    source.PlayOneShot(aux);
        Invoke("disableRendering", IT);
		
	}
    void CheckDistances()
    {
        Debug.Log(clicksMade);
        if (clicksMade >= N)
        {
            Debug.Log("CLICKS MADE: " + clicksMade);
            return;
        }
        

        Vector3 mousep = Input.mousePosition;
        mousep.z = 0;
        mousep = Camera.main.ScreenToWorldPoint(mousep);

        float dist = float.MaxValue;

        foreach(GameObject i in objs)
        {
            //First we check if it's inside the square
            Collider2D k = i.GetComponent<Collider2D>();
           if(k.OverlapPoint(mousep))
            {
                i.SetActive(false);
                goodClicked++;
                Debug.Log(goodClicked);
                server.sendMessage("SQUARE CLICKED AT: " + mousep.x + " " + mousep.y);
                return;
            }

            float aux = (float)euclideanDistance(new Vector2(mousep.x, mousep.y), new Vector2(i.transform.position.x, i.transform.position.y));
            if (aux < dist) dist = aux;
        }
        server.sendMessage("CLICK AT "+mousep.x+ " "+ mousep.y+ " FAILED");
        server.sendMessage("DISTANCE TO CLOSEST SQUARE: " + dist);
    }

    double euclideanDistance(Vector2 a, Vector2 b)
    {
        return Math.Sqrt(Math.Pow(a.x + b.x, 2) + (Math.Pow(a.y + b.y, 2)));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && started)
        {
            clicksMade++;
            CheckDistances();
        }
		
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

   
        float l, r;
        l = (-min.x + max.x) /(3*N);
        float x = min.x;

        for (int i = 0; i < N; i++)
        {
            int aux = rnd.Next((int)(x += l), (int)(x += 2 * l));
            int aux2 = rnd.Next((int)min.y, (int)max.y);
            p = new Vector3(aux, aux2, 0);

            objs[i] = GameObject.Instantiate(pinkRect, p, Quaternion.identity);
        }
        clicksMade = 0;
        started = true;
    }
    void disableRendering()
    {
        foreach(GameObject i in objs)
        {
            i.GetComponent<SpriteRenderer>().enabled = false;
        }
        started = true;
    }
}
