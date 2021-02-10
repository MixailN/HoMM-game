using System;
using LabaOOP2.Units;
using System.Collections.Generic;

namespace LabaOOP2
{
    class Program
    {
        static void Main(string[] args)
        {
            string answer;
            int k;
            string quantity;
            List<Army> armies = new List<Army>();
            armies.Add(new Army());
            armies.Add(new Army());
            for (int j = 0; j < 2; j++)
            {
                Console.WriteLine($"-------------------Creating {j + 1} armie-------------------");
                List<Unit> AllUnits = Mods.LoadAllUnits();
                for(int l =0; l < 6; l++)
                {
                    k = 1;
                    Console.WriteLine("Choose unit: ");
                    foreach (var i in AllUnits)
                    {
                        Console.WriteLine($"{k}) {i.Type} ");
                        k++;
                    }
                    Console.WriteLine($"{k}) Exit.");
                    answer = Console.ReadLine();
                    if (Int32.TryParse(answer, out int number))
                    {
                        if (number == k)
                        {
                            break;
                        }
                        else
                        {
                            if (number <= AllUnits.Count && number > 0)
                            {
                                Console.WriteLine("Enter unit's quantity: ");
                                quantity = Console.ReadLine();
                                UnitsStack stack = new UnitsStack(AllUnits[Int32.Parse(answer) - 1], Int32.Parse(quantity));
                                armies[j].AddStack(stack);
                            }
                            else
                            {
                                Console.WriteLine("-------------------Enter correct number!-------------------");
                                l--;
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("-------------------Wrong input format!-------------------");
                        l--;
                    }
                   
                    
                }
            }
            BattleArmy army1 = new BattleArmy(armies[0], 1);
            BattleArmy army2 = new BattleArmy(armies[1], 2);
            Battle my_battle = new Battle(army1, army2);
            InitiativeManager initiativeManager = InitiativeManager.GetInitiativeManager();
            try
            {
                while (!my_battle.IsEnd)
                {
                    my_battle.Action();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
    }
}
