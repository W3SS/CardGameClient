using System;
using SimpleJson;
using Pomelo.DotNetClient;
using System.Collections;
using System.Collections.Generic;
public class Network {
	public static PomeloClient pclient = new PomeloClient ();
	public const string HOST = "hacklu.com";//(www.xxx.com/127.0.0.1/::1/localhost etc.)
	public const int PORT = 7778;

	// 每个正在通信中的rpc通道都保存在这里
	// 第一个参数是rpcId的数值
	// 从设计上来说，rpc通信结束后应该从这个Dict中清理掉
	static Dictionary<int, IEnumerator> rpcHandlers = new Dictionary<int, IEnumerator>();

	// 本来应该是保存在各个Handler里的，也就是应该扩展IEnumerator，不过我最近赶时间，先这样吧！
	// TODO remove 'public'
	public static Dictionary<int, JsonObject> rpcResponse = new Dictionary<int, JsonObject>();

	static Dictionary<string, Func<int, JsonObject, IEnumerable> > rpcFunctions = new Dictionary<string, Func<int, JsonObject, IEnumerable> >();
	
	public static int CONNECT_RESULT_OK = 1;
	public static int CONNECT_RESULT_ERROR = 2;
	
	public static void connect(Action cb = null){
		pclient.initClient (HOST, PORT, () => {
			JsonObject user = new JsonObject();
			pclient.connect(user, data => {
				Console.WriteLine ("connect response msg: ");

				pclient.on ("onEnterRoom", (responseJson) => {
					Debug.Log ("enter room");
				});
				pclient.on ("onRoomMessage", (responseJson) => {
					Debug.Log ("received roomMessage: " + responseJson["roomMessage"]);
				});
				pclient.on ("onLeaveRoom", (responseJson) => {
					Debug.Log ("leave room");
				});
				pclient.on ("disconnect", (responseJson) => {
					Debug.Log ("disconnect");
				});
				pclient.on ("serverRpc", (responseJson) => {
					int rpcId = Convert.ToInt32(responseJson["_rpcId"]);
					string cmd = (string)responseJson["_cmd"];
					Network.rpcHandler(rpcId, cmd, responseJson);
				});

				if (cb != null) {
					cb();
				}
			});
		});
	}

	public static void request (string handlerName, JsonObject msg, Action<JsonObject> cb) {
		pclient.request(handlerName, msg, cb);
	}

	static void rpcHandler(int rpcId, string cmd, JsonObject dataJson) {
		Debug.DevLog("" + rpcId + ", " + cmd);
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
		if (rpcFunctions.ContainsKey(cmd)){
			// 这次暂时省略寻找cmd对应函数的步骤，直接用initHand函数来接待。
			IEnumerator ienum = rpcFunctions[cmd](rpcId, dataJson).GetEnumerator();
			bool result = ienum.MoveNext();

			// 如果还有后续，就先存起来，等以后再调用
			if (result) {
				rpcHandlers[rpcId] = ienum;
			}
		} else {
			Debug.Log("error 0001");
		}
	}

	public static void registerRpc(string rpcName, Func<int, JsonObject, IEnumerable> func){
		rpcFunctions[rpcName] = func;
	}
}