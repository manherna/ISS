using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FileReader : MonoBehaviour {
    StreamReader sRead;
    /*We will have a bidimensional array to express times and what state should go next.
    in the first row of the array only two values can be written: 0 for rest and 1 for task as next state.
    in the second row the time of the next state will be written.
    e.g.
    [ 0, 1, 0, 1, 1, 0 ] 
    [ 10, 15, 25, 40, 34, 10]
         */
    int [,] times;
    //GameOver we will use to pass the times array.
    public SceneLoader sceneLoader;
    int i;
    // Use this for initialization
    void Start () {
        sRead = new StreamReader("./Assets/files/test.txt");
        string line = sRead.ReadLine();
        string read;
        string prevRead = "";
        int nLines;
        int n;
         i = 0;
        if (int.TryParse(line, out nLines))
        {
            times = new int [nLines, nLines];
            while (!sRead.EndOfStream && i < nLines)
            {
                line = sRead.ReadLine();
                read =  line.Substring(0, 4);
                read = read.ToLower();
                if (read == "task" || read == "rest")
                {
                    if (read == "rest") times[0, i] = 0;
                    else times[0, i] = 1;

                       if (int.TryParse(line.Substring(4), out n))
                    {
                        times[1,i] = n;
                    }
                    else throw new FileLoadException("INVALID NUMBER AT LINE " + i.ToString());
                    prevRead = read;
                }
                else throw new FileLoadException("INVALID ARGUMENT: " + read);
                i++;
            }
                sceneLoader.updateScenes(times);
        }
        else
        {
            throw new FileLoadException("INVALID ARGUMENT. FIRST PARAMETER MUST BE NUMBER OF LINES");
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
