using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System.Threading;
 
public class Global
{
	static public void Start () {
		Debug.Log("Hello Mono World");
		Game game = new Game();
		Network.connect(() => {
			// test
			game.requestRoomTest();
		});
		Thread.Sleep(10000);
	}
}
