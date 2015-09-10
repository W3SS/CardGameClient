public class Player : EventDispatcher {
	// 英雄技能
	// 测试开发阶段，先默认技能都是火焰冲击吧
	ISkill _currSkill = new Skill_FireBall();

	CardGroup _deck = new CardGroup_Deck(30);

	// 初始HP & 初始HP上限
	const int INIT_HP_COUNT = 30;

	Hp _hp = new Hp(INIT_HP_COUNT);
	Mana _mana = new Mana();

	public Player() {
		Game.Instance().dispatchEvent("create", ret.GetType().Name, ret);
	}

	public CardGroup getDeck() {
		return _deck;
	}
}