#if UNITY_EDITOR || UNITY_STANDALONE
// 注意逻辑和表现的分离
// 逻辑部分以后可能用lua重写
public class UCard : MonoBehaviour {

	// Card逻辑管理
	Card	_card;

	enum CardState {
		BUSY,				// 动画切换之类，不能和用户交互的状态
		INIT_HAND_CHOOSE,	// 初始手牌（浮空状态）
		HAND,				// 手牌中
		DECK				// 牌库中
	}
	CardState _state = CardState.DECK;

	public UCard() {
		
	}

	void OnMouseEnter() {
		switch(_state) {
			case CardState.INIT_HAND_CHOOSE:
				
			break;
		}
	}

	void OnMouseExit() {
		switch(_state) {
			case CardState.INIT_HAND_CHOOSE:
				
			break;
		}
	}

	void OnMouseDown() {
		switch(_state) {
			case CardState.INIT_HAND_CHOOSE:
				
			break;
		}
	}

	void OnMouseUp() {
		
	}

	void OnMouseDrag() {
		
	}

	void moveCardTo(CardGroup cardGroup) {
		
	}
}
#endif