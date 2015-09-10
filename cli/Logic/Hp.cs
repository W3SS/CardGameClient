public class Hp {

	// 当前HP值
	int _currHpCount = 0;

	// 当前HP上限
	int _currMaxHpCount = 0;

// 下面两个属性，随从的攻击力也要用到
// 以后也许可以抽象上去
	// 是否满血
	bool _isFullHp = true;

	// 是否涨过Hp上限
	bool _isMoreHp = false;

	public Hp(int initHp) {
		_currHpCount = initHp;
		_currMaxHpCount = initHp;

		Game.Instance().dispatchEvent("create", ret.GetType().Name, ret);
	}
}