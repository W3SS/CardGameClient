using System;
using SimpleJson;
using System.Collections;
using System.Collections.Generic;
public class Game : EventDispatcher {
	Player _enemy = new Player();
	Player _self = new Player();
	
	static Game _instance = null;
	public static Game Instance() {
		return _instance;
	}

	public Game() {
		_instance = this;
		Network.registerRpc("chooseInitCard", rpc_initHand);
		Network.registerRpc("onTurnEnd", rpc_endTurn);
	}

	// test code
	public void requestRoomTest() {
		JsonObject msg = new JsonObject ();
		msg ["uid"] = "kira";
		Network.request("connector.gameHandler.roomTest", msg, (JsonObject result) => {
			Debug.Log ("entry result code : " + result ["code"]);
		});
	}

	IEnumerable<int> rpc_initHand(int rpcId, JsonObject dataJson) {
		JsonArray cards = (JsonArray)dataJson["cards"];
		int count = cards.Count;

		// 从卡组里取出count张牌
		CardGroup initCardGroup = _self.getDeck().removeCardsFromHead(count);
		// initCardGroup = new InitHandCardGroup(initCardGroup);

		// 揭示取出的卡牌
		List<int> cardIds = new List<int>();
		for(int i = 0; i < count; ++i) {
			cardIds.Add(Convert.ToInt32(cards[i]));
		}
		initCardGroup.revealCards(cardIds);

		// 等待玩家选择
		List<int> replacementCardIndexs = null;
		initCardGroup.waitChooseCardFinished((List<int> indexs) => {
			replacementCardIndexs = indexs;
			Network.rpcHandler(rpcId, (string)dataJson["_cmd"], new JsonObject());
		});
		yield return 0;

		// 如果需要换牌
		if (replacementCardIndexs != null) {
			JsonObject msg = new JsonObject ();
			msg ["_rpcId"] = rpcId;
			msg ["cmd"] = "changeCard";
			msg ["data"] = new JsonObject();

			// 填装数组
			JsonArray jsonArray = new JsonArray();
			foreach(int index in replacementCardIndexs) {
				jsonArray.Add(index);
			}
			msg ["indexs"] = jsonArray;

			Network.request(
				"connector.gameHandler.rpcResponse", 
				msg, 
				(JsonObject dummy) => {}
			);

			// 等待换牌结果
			yield return 0;

			// 收到换牌结果
			dataJson = Network.rpcResponse[rpcId];

			Dictionary<int,int> dict = new Dictionary<int,int>();
			JsonObject rawDict = (JsonObject)dataJson["changeCard"];
			foreach(string key in rawDict.Keys) {
				int index = -1;
				int cardId = -1;
				if (int.TryParse(key, out index)) {
					if (int.TryParse(rawDict[key].ToString(), out cardId)) {
						dict[index] = cardId;
					}
				}
			}
			CardGroup replacedCardGroup = initCardGroup.replaceCardsByIndexs(dict);
			_self.getDeck().appendCards(dict.Count);

			// test 等server的最后一个ok
			yield return 0;
			Debug.Log("Done");
		}
	}

	IEnumerable rpc_endTurn(int rpcId, JsonObject dataJson) {
		return null;
	}

	
}