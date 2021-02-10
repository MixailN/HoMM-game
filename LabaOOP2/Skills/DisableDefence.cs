using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2.Skills
{
    public class DisableDefence : Skill
    {
        public DisableDefence() : base("Disable Defence", true)
        {

        }
        public override void Effect(SkillContext Context)
        {
            if (curOnOff == true)
            {
                Context.curTargetStack.AddModifier(new Modifier(Baff.Defence, 0, ValueType.Percent, -1, Context.curTargetStack.curTurnCounter));
            }
        }
    }
}
