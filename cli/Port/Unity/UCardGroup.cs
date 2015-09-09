#if UNITY_EDITOR || UNITY_STANDALONE
using System.Collections.Generic;

// 存放多枚卡片的容器
// 相对轻量级，可以临时创建，比如初始手牌选择时，空中的一组卡牌
// 管理卡牌进入、离开这个组时的各种动画效果
// 理论上应该支持继承，比如初始手牌换牌动画之类特殊的动画效果
public class UCardGroup {

	// 卡牌的显示范围
	// 虽然是3D空间，但感觉上还是接近2D游戏，有这四个值就够了吧？
	public float _top;
	public float _bottom;
	public float _left;
	public float _right;

	// 逻辑管理
	CardGroup _cardGroup;

	List<UCard> _cards;

	public List<UCard> removeFromHead() {
		List<UCard> ret = new List<UCard>();
		return ret;
	}

	public List<UCard> removeFromTail() {
		List<UCard> ret = new List<UCard>();
		return ret;
	}

	public List<UCard> removeCards(List<UCard> cards) {
		List<UCard> ret = new List<UCard>();
		return ret;
	}

	public Dictionary<int, UCard> removeCardsByIndex(List<int> indexs) {
		Dictionary<int, UCard> ret = new Dictionary<int, UCard>();
		return ret;
	}

	// 把UCard插入对应index中
	public void insertCards(Dictionary<int, UCard> cards) {
		
	}

	public void appendCards(List<UCard> cards) {
		
	}

	public void appendCard(UCard card) {
		
	}
}
#endif