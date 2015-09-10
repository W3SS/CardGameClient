public class Mana : EventDispatcher {

	// 最大魔法值上限
	const int MAX_MANA_COUNT = 10;

	// 当前剩余魔法值
	int _currManaCount = 0;

	// 当前魔法值上限
	int _currMaxManaCount = 0;

	// 过载
	int _currOverLoadCount = 0;

	public Mana() {

	}

	// 增加魔法上限
	public bool increaseMaxManaCount(int count, bool isAddCurrMana = false) {
		if (count < 0) {
			return false;
		}
		_currMaxManaCount += count;
		_currMaxManaCount = Math.Min(_currMaxManaCount, MAX_MANA_COUNT);

		if (isAddCurrMana) {
			_currManaCount += count;
			_currManaCount = Math.Min(_currManaCount, _currMaxManaCount);
		}
		return true;
	}

	// 消费魔法
	public bool costMana(int count) {
		if (_currManaCount < count || count < 0) {
			return false;
		}
		_currManaCount -= count;
	}

	// 获得当前可用的魔法值 （算上过载的影响）
	public int getUsableManaCount() {
		return Math.Max(_currManaCount - _currOverLoadCount, 0);
	}

	public void setCurrOverLoadCount(int count) {
		_currOverLoadCount = count;
	}

	public void setMaxManaCount(int count) {
		_currMaxManaCount = count;
	}

	public void setCurrManaCount(int count) {
		_currManaCount = count;
	}
}