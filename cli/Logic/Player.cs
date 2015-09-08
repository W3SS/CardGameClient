public class Player {

	// 最大魔法值上限
	const int MAX_MANA_COUNT = 10;

	// 当前剩余魔法值
	int _currManaCount = 0;

	// 当前魔法值上限
	int _currMaxManaCount = 0;

	// 初始HP & 初始HP上限
	const int INIT_HP_COUNT = 30;

	// 当前HP值
	int _currHpCount = INIT_HP_COUNT;

	// 当前HP上限
	int _currMaxHpCount = INIT_HP_COUNT;

	// 英雄技能
	// 测试开发阶段，先默认技能都是火焰冲击吧
	ISkill _currSkill = new Skill_FireBall();
}