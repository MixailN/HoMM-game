using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2
{
    class Battle
    {

        private InitiativeManager initiativeManager = InitiativeManager.GetInitiativeManager();

        private Dictionary<int, BattleArmy> Armies = new Dictionary<int, BattleArmy>();

        private int TurnQuantity;
        public bool IsEnd; //=> Armies[1].isDead() || Armies[2].isDead();

        public Battle(BattleArmy nArmy1, BattleArmy nArmy2)
        {
            Armies.Add(nArmy1.ArmyID, nArmy1);
            Armies.Add(nArmy2.ArmyID, nArmy2);
            IsEnd = false;
            TurnQuantity = 0;
        }

        public void Attack(BattleUnitsStack Attacking, BattleUnitsStack Defending)
        {
            if(!Attacking.isDead() && !Defending.isDead())
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
                    Console.WriteLine($"-------------------Attack: {Attacking.pCharacter.Type} -> {Defending.pCharacter.Type} {fullDamage}");
                    AllHP -= fullDamage;
                    if(AllHP < 0)
                    {
                        AllHP = 0;
                    }
                    Defending.curLastHP = AllHP % Defending.pCharacter.HitPoints;
                    Defending.curQuantity = AllHP / Defending.pCharacter.HitPoints;

                   
                }
                else
                {
                    minDamage = (int)(Attacking.curQuantity * (Attacking.curDamage.min) / (1 + 0.05 * (Defending.curDefence - Attacking.curAttack)));
                    maxDamage = (int)(Attacking.curQuantity * (Attacking.curDamage.max ) / (1 + 0.05 * (Defending.curDefence - Attacking.curAttack)));
                    fullDamage = rand.Next(minDamage, maxDamage + 1);
                    Console.WriteLine($"-------------------Attack: {Attacking.pCharacter.Type} -> {Defending.pCharacter.Type} {fullDamage}");
                    AllHP -= fullDamage;
                    if (AllHP < 0)
                    {
                        AllHP = 0;
                    }
                    Defending.curLastHP = AllHP % Defending.pCharacter.HitPoints;
                    Defending.curQuantity = AllHP / Defending.pCharacter.HitPoints;
                }
            }
            else
            {
                throw new Exception("Can't choose dead stack");
            }
            
        }
        
        public void Action()
        {
            if(initiativeManager.isEmpty())
            {
                initiativeManager.CreateQueue(Armies[1], Armies[2]);
                if (TurnQuantity > 0)
                {
                    Console.WriteLine("-------------------Round completed-------------------");
                 
                }
                TurnQuantity++;
            }
            string queue = initiativeManager.ShowInitiative();
            Console.WriteLine(queue);
            BattleUnitsStack Current;
            Current = initiativeManager.GetNext();
            Current.curTurnCounter++;
            foreach (var i in Armies[1].Stacks)
            {
                i.CheckModifiers();
            }
            foreach (var i in Armies[2].Stacks)
            {
                i.CheckModifiers();
            }

            string action;
            string target;
            int k;
            SkillContext skillContext;

            Console.WriteLine("State of the armies:");
            Console.WriteLine("#1");
            foreach(var i in Armies[1].Stacks)
            {
                Console.WriteLine($"{i.pCharacter.Type} [{i.curQuantity}]");
            }
            Console.WriteLine("#2");
            foreach (var i in Armies[2].Stacks)
            {
                Console.WriteLine($"{i.pCharacter.Type} [{i.curQuantity}]");
            }
            Console.WriteLine($"Current stack is {Current.pCharacter.Type} [{Current.curQuantity}] from {Current.ArmyID} army");
            Console.WriteLine("Choose action: \n 1. Attack \n 2. Defence \n 3. Use Magic \n 4. Wait \n 5. Concede");
            action = Console.ReadLine();
            switch(action)
            {
                case "1":

                    k = 1;
                    Console.WriteLine($"Choose target from {3 - Current.ArmyID} army:");
                    foreach (var i in Armies[3 - Current.ArmyID].Stacks)
                    {
                        Console.WriteLine($"{k}) {i.pCharacter.Type} [{i.curQuantity}]");
                        k++;
                    }
                    target = Console.ReadLine();
                    BattleUnitsStack Defending = Armies[3 - Current.ArmyID][Int32.Parse(target) - 1];

                    skillContext = SkillContext.GetSkillContext(Armies[Current.ArmyID], Armies[3 - Current.ArmyID], Current, Defending, TurnQuantity);
                    Current.UsePassiveSkills(skillContext);
                    Attack(Current, Defending);
                    if(Defending.isDead())
                    {
                        Armies[3 - Current.ArmyID].RemoveStack(Defending);
                        initiativeManager.RemoveStack(Defending);
                    }
                    else
                    {
                        Defending.PassiveSkills[0].Effect(skillContext); // Recharge
                        if (Defending.PassiveSkills[0].curOnOff != false)
                        {
                            Defending.AddModifier(new Modifier(Baff.Skill, 1, TurnQuantity, Defending.PassiveSkills[0], 0));
                        }
                        if (Current.isDead())
                        {
                            Armies[Current.ArmyID].RemoveStack(Current);
                            initiativeManager.RemoveStack(Current);
                        }
                    }
                    initiativeManager.RemoveStack(Current);
                    IsEnd = Armies[1].isDead() || Armies[2].isDead();
                    break;
                case "2":
                    Current.AddModifier(new Modifier(Baff.Defence, 1, ValueType.Percent, 0.3, Current.curTurnCounter));
                    initiativeManager.RemoveStack(Current);
                    break;
                case "3":
                    if (Current.ActiveSkills.Count != 0)
                    {
                        k = 1;
                        string spell;
                        Console.WriteLine("Choose magic:");

                        foreach (var i in Current.ActiveSkills)
                        {
                            Console.WriteLine($"{k}) {i.Title} ");
                            k++;
                        }
                        spell = Console.ReadLine();
                        k = 1;
                        Console.WriteLine($"Choose target from {Current.ArmyID} army:");
                        foreach (var i in Armies[Current.ArmyID].Stacks)
                        {
                            Console.WriteLine($"{k}) {i.pCharacter.Type} [{i.curQuantity}]");
                            k++;
                        }
                        target = Console.ReadLine();
                        BattleUnitsStack Target = Armies[Current.ArmyID][Int32.Parse(target) - 1];
                        skillContext = SkillContext.GetSkillContext(Armies[Current.ArmyID], Armies[3 - Current.ArmyID], Current, Target, TurnQuantity);
                        Current.ActiveSkills[Int32.Parse(spell) - 1].Effect(skillContext);
                        initiativeManager.RemoveStack(Current);
                    }
                    else
                    {
                        Console.WriteLine("-------------------This unit doesn't have active skills!-------------------");
                        Current.curTurnCounter--;
                    }
                    break;
                case "4":
                    if(Current.curQueue == "Base")
                    {
                        initiativeManager.AddToWaiting(Current);
                    }
                    else
                    {
                        Console.WriteLine("-------------------Can't wait twice!-------------------");
                        Current.curTurnCounter--;
                    }
                    break;
                case "5":
                    IsEnd = true;
                    Console.WriteLine($"-------------------Army {Current.ArmyID} lost-------------------");
                    break;
                default:
                    Current.curTurnCounter--;
                    break;
            }
        }
    }
}
