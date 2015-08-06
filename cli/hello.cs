using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using Pomelo.DotNetClient;
using System.Threading;
 
public class HelloWorld
{
    static public void Main ()
    {
		PomeloClient pclient = new PomeloClient ();
        Console.WriteLine ("Hello Mono World");
		string host = "hacklu.com";//(www.xxx.com/127.0.0.1/::1/localhost etc.)
		int port = 7778;
		
		//listen on network state changed event
		pclient.NetWorkStateChangedEvent += (state) =>
		{
			Console.WriteLine("NetWorkStateChangedEvent: " + state);
		};
		
		pclient.initClient (host, port, () =>
		{
			//The user data is the handshake user params
			JsonObject user = new JsonObject ();
			pclient.connect (user, data =>
			{
				Console.WriteLine ("connect response msg: ");
				//process handshake call back data
				pclient.on ("onEnterRoom", (responseJson) => {
					Console.WriteLine ("enter room");
				});
				pclient.on ("onRoomMessage", (responseJson) => {
					Console.WriteLine ("received roomMessage: " + responseJson["roomMessage"]);
				});
				pclient.on ("onLeaveRoom", (responseJson) => {
					Console.WriteLine ("leave room");
				});
				pclient.on ("disconnect", (responseJson) => {
					Console.WriteLine ("disconnect");
				});
				JsonObject msg = new JsonObject ();
				msg ["uid"] = "kira";
				pclient.request ("connector.gameHandler.roomTest", msg, OnQuery);
			});
		});
		
		Thread.Sleep(10000);
    }
	
	static void OnQuery (JsonObject result)
	{
		Console.WriteLine ("entry result code : " + result ["code"]);
	}
}
