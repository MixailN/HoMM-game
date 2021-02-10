using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2.Skills
{
    public class AttackBaff : Skill
    {
        public AttackBaff() : base("Attack Baff", true)
        {

        }
        public override void Effect(SkillContext Context)
        {
            if (curOnOff == true)
            {
                Context.curTargetStack.AddModifier(new Modifier(Baff.Attack, 2, ValueType.Value, 12, Context.curTargetStack.curTurnCounter));
            }
        }
    }
}
