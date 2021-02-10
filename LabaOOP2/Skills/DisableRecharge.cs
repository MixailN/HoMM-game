using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2.Skills
{
    public class DisableRecharge : Skill
    {
        public DisableRecharge() : base("Disable Recharge", true)
        {

        }

        public override void Effect(SkillContext Context)
        {
            if(curOnOff == true)
            {
                Context.curTargetStack.AddModifier(new Modifier(Baff.Skill, 0, Context.curTargetStack.curTurnCounter, Context.curTargetStack.PassiveSkills[0], 0));
            }
        }
    }
}
