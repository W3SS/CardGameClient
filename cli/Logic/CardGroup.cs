using System;
using System.Linq;
using System.Collections.Generic;

public class CardGroup {
	List<Card> _cards = new List<Card>();
	List<Action<List<int> > > _chooseFinishListener = new List<Action<List<int> > >();

	public CardGroup(int count = 0) {
		if (count > 0) {
			appendCards(count);
		}
	}

	public CardGroup removeCardsFromHead(int count) {
		CardGroup ret = new CardGroup(count);
		return ret;
	}

	// 揭示卡牌 = 给卡牌赋值
	// 可以理解为 『翻开卡牌』
	public void revealCards(List<int> cardIds) {
		for(int i = 0; i < Math.Min(cardIds.Count, _cards.Count); ++i) {
			_cards[i].initCardByCardId(cardIds[i]);
		}
	}

	public void waitChooseCardFinished(Action<List<int> > cb) {
		_chooseFinishListener.Add(cb);

		// test
		System.Threading.Thread t = new System.Threading.Thread(()=>{
			Debug.Log("input please:");
			string line = Console.ReadLine();
			Debug.Log(line);
			var tmp = line.Split(',');
			List<int> ret = new List<int>();
			foreach (string str in tmp) {
				int number = -1;
				if (int.TryParse(str.Trim(), out number)) {
					ret.Add(number);
				}
			}
			onChooseCardFinished(ret);
		});
		t.IsBackground = true;
		t.Start();
	}

	void onChooseCardFinished(List<int> cardIndexs) {
		foreach(Action<List<int>> action in _chooseFinishListener) {
			action(cardIndexs);
		}
	}

	public CardGroup replaceCardsByIndexs(Dictionary<int,int> index_cardId_map) {
		return null;
	}

	public void appendCards(int count) {
		_cards.AddRange(Enumerable.Repeat(new Card(), count).ToList());
	}
}