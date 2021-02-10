using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2.Skills
{
    public class Recharge : Skill
    {
        public Recharge(bool OnOff) : base("Recharge", OnOff)
        {

        }

        public override void Effect(SkillContext Context)
        {
            if(curOnOff == true)
            {
                BattleUnitsStack Attacking = Context.curTargetStack;
                BattleUnitsStack Defending = Context.curStack;
                if (!Attacking.isDead() && !Defending.isDead())
                {
                    Random rand = new Random();
                    int minDamage;
                    int maxDamage;
                    int fullDamage;
                    int AllHP = Defending.pCharacter.HitPoints * (Defending.curQuantity - 1) + Defending.curLastHP;
                    if (Attacking.curAttack >= Defending.curDefence)
                    {
                        minDamage = Attacking.curQuantity * (int)(Attacking.curDamage.min * (1 + 0.05 * (Attacking.curAttack - Defending.curDefence)));
                        maxDamage = Attacking.curQuantity * (int)(Attacking.curDamage.max * (1 + 0.05 * (Attacking.curAttack - Defending.curDefence)));
                        fullDamage = rand.Next(minDamage, maxDamage + 1);
                        Console.WriteLine($"-------------------Recharge: {Attacking.pCharacter.Type} -> {Defending.pCharacter.Type} {fullDamage}");
                        AllHP -= fullDamage;
                        if (AllHP < 0)
                        {
                            AllHP = 0;
                        }
                        Defending.curLastHP = AllHP % Defending.pCharacter.HitPoints;
                        Defending.curQuantity = AllHP / Defending.pCharacter.HitPoints;


                    }
                    else
                    {
                        minDamage = Attacking.curQuantity * (int)(Attacking.curDamage.min / (1 + 0.05 * (Defending.curDefence - Attacking.curAttack)));
                        maxDamage = Attacking.curQuantity * (int)(Attacking.curDamage.max / (1 + 0.05 * (Defending.curDefence - Attacking.curAttack)));
                        fullDamage = rand.Next(minDamage, maxDamage + 1);
                        Console.WriteLine($"-------------------Recharge: {Attacking.pCharacter.Type} -> {Defending.pCharacter.Type} {fullDamage}");
                        AllHP -= fullDamage;
                        if (AllHP < 0)
                        {
                            AllHP = 0;
                        }
                        Defending.curLastHP = AllHP % Defending.pCharacter.HitPoints;
                        Defending.curQuantity = AllHP / Defending.pCharacter.HitPoints;
                    }
                }
            }
           
        }
    }
}
