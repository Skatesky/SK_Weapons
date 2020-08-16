using System;
namespace SK_Weapons
{
    // 带Buff的子弹基类
    public class ModExtension_SKBaseBullet : DefModExtension
    {
        // 构造函数
        public ModExtension_SKBaseBullet()
        {
            
        }

        // 概率，默认0.05，可以被覆盖
        public float addHediffChance = 0.05; //默认值会被xml覆盖
                                             // Buff的定义，也就是何种类型的buff
        public HediffDef hediffToAdd;
    }
}
