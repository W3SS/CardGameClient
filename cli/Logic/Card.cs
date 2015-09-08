// 这里只写纯逻辑的代码，供外部使用
// 最终目标是把这些逻辑都改写成lua逻辑，方便以后通过更新lua脚本来热更新游戏逻辑
// 为了能机器学习，这里的逻辑都应该是瞬时的
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
}