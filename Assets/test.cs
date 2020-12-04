using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSharpGRPC.services;
using System.Threading;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
	//static Thread listenserverThread = new Thread(ARServer.StartServer);
    void Start()
    {
		Thread listenserverThread = new Thread(ARServer.StartServer);
        //ARServer.StartServer();
		Debug.Log("Start Listen Server Thread");
		listenserverThread.Start();
		Thread.Sleep(1000);
		ARServer.StopServer();
		Debug.Log("Stop AR Server");
		Thread.Sleep(1000);
		//Debug.Log("Start AR Listen Server");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
