using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2.Skills
{
    public class InitiativeBaff : Skill
    {
        public InitiativeBaff() : base("Initiative Baff", true)
        {

        }
        public override void Effect(SkillContext Context)
        {
            if (curOnOff == true)
            {
                Context.curTargetStack.AddModifier(new Modifier(Baff.Initiative, 2, ValueType.Value, 5, Context.curTargetStack.curTurnCounter));
            }
        }
    }
}
