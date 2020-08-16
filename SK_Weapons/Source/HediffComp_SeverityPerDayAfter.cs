
using System;
using System.Text;
using Verse;

namespace SK_Weapons
{

  class HediffComp_SeverityPerDayAfter : HediffComp_SeverityPerDay {

    private HediffCompProperties_SeverityPerDayAfter Props {
      get {
        return (HediffCompProperties_SeverityPerDayAfter)props;
      }
    }

    /* 自然治癒開始までの残り時間を取得します */

    private int StartChangeSeverityAfter {
      get {
        return Math.Max(0, Props.startChangeSeverityAfter - parent.ageTicks);
      }
    }

    /* デバッグモード時に追加で情報を表示します */

    public override string CompDebugString (){
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(base.CompDebugString().TrimEndNewlines());
      if (base.Pawn.Dead == false){
        stringBuilder.AppendLine("change severity after: " + StartChangeSeverityAfter.ToString());
      }
      return stringBuilder.ToString().TrimEndNewlines();
    }

    /* 自然治癒開始まで時間がまだある場合、進行度の変化量を 0 にします */

    protected override float SeverityChangePerDay (){
      if (0 < StartChangeSeverityAfter){
        return 0.0f;
      }
      else {
        return base.SeverityChangePerDay();
      }
    }

  }

}
