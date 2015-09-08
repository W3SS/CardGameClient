using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System.Threading;
 
public class Global
{
	static public void Start () {
		Debug.Log("Hello Mono World");
		Game game = null;
		Network.connect(() => {
			// test
			game = new Game();
		});
		Thread.Sleep(10000);
	}
}
