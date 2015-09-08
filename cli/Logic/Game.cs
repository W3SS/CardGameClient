using System;
using SimpleJson;
using System.Collections.Generic;
public class Game {
	Player _enemy;
	Player _self;
	
	static Game _instance = null;
	public static Game Instance() {
		return _instance;
	}

	public Game() {
		_instance = this;
		Network.registerRpc("chooseInitCard", rpc_initHand);
		Network.registerRpc("onTurnEnd", rpc_endTurn);
		// test code
		requestRoomTest();
	}

	// test code
	void requestRoomTest() {
		JsonObject msg = new JsonObject ();
		msg ["uid"] = "kira";
		Network.request("connector.gameHandler.roomTest", msg, (JsonObject result) => {
			Debug.Log ("entry result code : " + result ["code"]);
		});
	}

	IEnumerable<int> rpc_initHand(int rpcId, JsonObject dataJson) {
		JsonArray cards = (JsonArray)dataJson["cards"];
		int count = cards.Count;
		for( int i = 0; i < count; ++i ) {
			Debug.DevLog(""+Convert.ToInt32(cards[i]));
		}
		Debug.DevLog("initHand: " + count);
		JsonObject msg = new JsonObject ();
		msg ["_rpcId"] = rpcId;
		msg ["cmd"] = "ok";
		Network.request(
			"connector.gameHandler.rpcResponse", 
			msg, 
			(JsonObject dummy) => {}
		);
		yield return 0;

		dataJson = Network.rpcResponse[rpcId];
		Debug.DevLog(SimpleJson.SimpleJson.SerializeObject(dataJson));
	}

	IEnumerable<int> rpc_endTurn(int rpcId, JsonObject dataJson) {
		
	}

	
}