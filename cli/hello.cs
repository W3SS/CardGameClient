using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using Pomelo.DotNetClient;
using System.Threading;
 
public class HelloWorld
{
	static PomeloClient pclient = new PomeloClient ();

	static public void Main ()
	{
		Console.WriteLine ("Hello Mono World");
		string host = "hacklu.com";//(www.xxx.com/127.0.0.1/::1/localhost etc.)
		int port = 7778;
		PomeloClient pclient = HelloWorld.pclient;
		
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
					Log ("enter room");
				});
				pclient.on ("onRoomMessage", (responseJson) => {
					Log ("received roomMessage: " + responseJson["roomMessage"]);
				});
				pclient.on ("onLeaveRoom", (responseJson) => {
					Log ("leave room");
				});
				pclient.on ("disconnect", (responseJson) => {
					Log ("disconnect");
				});
				pclient.on ("serverRpc", (responseJson) => {
					int rpcId = Convert.ToInt32(responseJson["_rpcId"]);
					string cmd = (string)responseJson["_cmd"];
					rpcHandler(rpcId, cmd, responseJson);
				});
				pclient.on ("initHand", onInitHand);
				JsonObject msg = new JsonObject ();
				msg ["uid"] = "kira";
				pclient.request ("connector.gameHandler.roomTest", msg, (JsonObject result) => {
					Log ("entry result code : " + result ["code"]);
				});
			});
		});
		Thread.Sleep(10000);
	}

	static void Log(string msg) {
		// Console.WriteLine(msg);
	}

	static void DevLog(string msg) {
		Console.WriteLine(msg);
	}

	static void onInitHand(JsonObject responseJson) {
		JsonArray cards = (JsonArray)responseJson["cards"];
		int count = cards.Count;
		for( int i = 0; i < count; ++i ) {
			DevLog(""+Convert.ToInt32(cards[i]));
		}
		DevLog("initHand: " + count);
	}

	// 每个正在通信中的rpc通道都保存在这里
	// 第一个参数是rpcId的数值
	// 从设计上来说，rpc通信结束后应该从这个Dict中清理掉
	static Dictionary<int, IEnumerator> rpcHandlers = new Dictionary<int, IEnumerator>();

	// 本来应该是保存在各个Handler里的，也就是应该扩展IEnumerator，不过我最近赶时间，先这样吧！
	static Dictionary<int, JsonObject> rpcResponse = new Dictionary<int, JsonObject>();

	static void rpcHandler(int rpcId, string cmd, JsonObject dataJson) {
		DevLog("" + rpcId + ", " + cmd);
		// 如果是正在处理中的rpc通信，让他接着处理
		if (rpcHandlers.ContainsKey(rpcId)) {
			rpcResponse[rpcId] = dataJson;

			// 如果全部执行完了，就把这个handler移除掉
			if (!rpcHandlers[rpcId].MoveNext()) {
				rpcHandlers.Remove(rpcId);
			}
			return;
		}

		// 如果是初次到来的rpc，根据cmd寻找对应的处理函数

		// 这次暂时省略寻找cmd对应函数的步骤，直接用initHand函数来接待。
		IEnumerator ienum = initHand(rpcId, dataJson).GetEnumerator();
		bool result = ienum.MoveNext();

		// 如果还有后续，就先存起来，等以后再调用
		if (result) {
			rpcHandlers[rpcId] = ienum;
		}
	}

	static IEnumerable<int> initHand(int rpcId, JsonObject dataJson) {
		JsonArray cards = (JsonArray)dataJson["cards"];
		int count = cards.Count;
		for( int i = 0; i < count; ++i ) {
			DevLog(""+Convert.ToInt32(cards[i]));
		}
		DevLog("initHand: " + count);
		JsonObject msg = new JsonObject ();
		msg ["_rpcId"] = rpcId;
		msg ["cmd"] = "ok";
		HelloWorld.pclient.request(
			"connector.gameHandler.rpcResponse", 
			msg, 
			(JsonObject dummy) => {}
		);
		yield return 0;

		dataJson = rpcResponse[rpcId];
		DevLog(SimpleJson.SimpleJson.SerializeObject(dataJson));
	}
}
