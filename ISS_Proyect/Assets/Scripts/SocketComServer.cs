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
    Socket s;
    public bool serverClose;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void OnDestroy()
    {
        serverClose = true;
        server.Join();
    }

    // Use this for initialization
    void Start() {
        serverClose = false;
        server = new Thread(comunicate);
        buffer = new LinkedList<string>();
        s = null;
        server.Start();
       System.Diagnostics.Process.Start(Application.dataPath + "/SideProgram/BasicClient.exe");

    }

    // This method was obtained from https://stackoverflow.com/questions/6803073/get-local-ip-address

    public static string GetLocalIPAddress()
    {

        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    } 

    #region threadfunction
    void comunicate()
    {
        try
        {
            IPAddress ipAd;
            TcpListener serverSocket;
            ipAd = IPAddress.Parse(GetLocalIPAddress());
            serverSocket = new TcpListener(ipAd, 8001);
            Debug.Log("Starting server...");
            serverSocket.Start();

   


            s = serverSocket.AcceptSocket();
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
            byte []end = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};
            s.Send(end);
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
        updateBuffer(scene + " started ");
    }
    public void OnSceneEnd(string scene)
    {
        updateBuffer(scene + " ended ");
    }
    public void EndServer()
    {
        Debug.Log("Closing Server...");
        serverClose = true;
    }
    public void sendMessage(string message)
    {
        updateBuffer(message);
    }
#endregion  
}
