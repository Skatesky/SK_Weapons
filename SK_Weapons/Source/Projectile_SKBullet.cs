using System;
using RimWorld;
using Verse;

namespace SK_Weapons
{
    // 麻醉子弹
    public class Projectile_SKBullet : Bullet
    {
        // 构造函数
        public Projectile_SKBullet() {
            
        }

        public ModExtension_SKBaseBullet Props => base.def.GetModExtension<ModExtension_SKBaseBullet>();

        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);

            if (Props != null && hitThing != null && hitThing is Pawn hitPawn)
            {
                float rand = Rand.Value;
                if (rand <= Props.addHediffChance)
                {
                    Messages.Message("SK_Bullet_SuccessMessage".Translate(
                        this.launcher.Label, hitPawn.Label
                    ), MessageTypeDefOf.NeutralEvent);

                    // 判断一下目标是否已经触发了瘟疫效果
                    Hediff plagueOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(Props.hediffToAdd);
                    float randomSeverity = Rand.Range(0.15f, 0.30f);
                    // 已经触发瘟疫
                    if (plagueOnPawn != null)
                    {
                        // 严重程度叠加，超过100%会即死
                        plagueOnPawn.Severity += randomSeverity;
                    }
                    else
                    {
                        // 我们调用HediffMaker.MakeHediff生成一个新的hediff状态，类型就是我们之前设置过的HediffDefOf.Plague瘟疫类型
                        Hediff hediff = HediffMaker.MakeHediff(Props.hediffToAdd, hitPawn);
                        // 严重程度
                        hediff.Severity = randomSeverity;
                        // 把状态添加到被击中目标的身上
                        hitPawn.health.AddHediff(hediff);
                    }
                }
                //本次没有触发
                else
                {
                    //这个方法可以在某个位置(这里是被击中目标的身旁)弹出一小行字，比如未击中，击中头部之类的，也是可以
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "SK_Bullet_FailureMote".Translate(Props.addHediffChance), 12f);
                }
            }
        }
    }
}
