public class Card {

	// 初始发牌换牌用flag
	private bool isUnChoose = false;

	// 初始发牌换牌用flag
	public void setUnChooseMark() {
		
	}

	// Card所属的卡片组
	private CardGroup parentCardGroup;

	// 改变卡片的所属组。
	// 比如从【Deck组】移动到【初始手牌备选组】
	// 比如从【初始手牌备选组】移动到【Hand组】
	public void changeCardGroup() {
		
	}

	public void moveCard() {
		
	}

	int _cardId = 0;
	public void initCardByCardId(int cardId) {
		_cardId = cardId;
	}
}