using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
public class SocketComServer : MonoBehaviour
{
    Thread server;
    LinkedList<string> buffer;
    public bool serverClose;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void OnDestroy()
    {
      
    }

    // Use this for initialization
    void Start() {
        serverClose = false;
        server = new Thread(comunicate);
        buffer = new LinkedList<string>();
        server.Start();
    }

    #region threadfunction
    void comunicate()
    {
        try
        {
            IPAddress ipAd;
            TcpListener serverSocket;
            //Localhost: 127.0.0.1
            ipAd = IPAddress.Parse("127.0.0.1");
            serverSocket = new TcpListener(ipAd, 8001);
            Debug.Log("Starting server...");
            serverSocket.Start();

   


            Socket s = serverSocket.AcceptSocket();
            Debug.Log("Connection accepted from " + s.RemoteEndPoint);

      
            while (!serverClose)
            {
                lock (buffer)
                {
                    for (LinkedListNode<string> it = buffer.First; it != null; it = it.Next)
                    {
                        string a = it.Value;
                        s.Send(Encoding.ASCII.GetBytes(a));
                        buffer.RemoveFirst();


                    }
                }
            }
            updateBuffer("END");
            s.Close();
            serverSocket.Stop();
              }
        catch (System.Exception e)
        {
            Debug.LogError(e.StackTrace);
            serverClose = true;
        }
    }

    #endregion

    // Update is called once per frame
    void Update() {

    }
    private void updateBuffer (string message){


        lock (buffer)
        {
            buffer.AddLast(message);
        }

    }

    #region Notifying methods
    public void OnSceneStart(string scene)
    {
        updateBuffer(scene + " started");
    }
    public void OnSceneEnd(string scene)
    {
        updateBuffer(scene + " ended");
    }
    public void EndServer()
    {
        Debug.Log("Closing Server...");


        serverClose = true;
    }
#endregion  
}
