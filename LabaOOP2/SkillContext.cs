using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2
{
    public class SkillContext
    {
        private BattleArmy CurrentArmy;

        private BattleArmy EnemyArmy;

        private BattleUnitsStack Current;

        private BattleUnitsStack Target;

        private int Turn;

        private static SkillContext Data;

        public BattleArmy curArmy
        {
            get => CurrentArmy; set
            {
                CurrentArmy = value;
            }
        }

        public BattleArmy curEnemyArmy
        {
            get => EnemyArmy; set
            {
                EnemyArmy = value;
            }
        }

        public BattleUnitsStack curStack
        {
            get => Current; set
            {
                Current = value;
            }
        }

        public BattleUnitsStack curTargetStack
        {
            get => Target; set
            {
                Target = value;
            }
        }

        public int curTurn
        {
            get => Turn; set
            {
                Turn = value;
            }
        }
        private SkillContext(BattleArmy nCurrentArmy, BattleArmy nEnemyArmy, BattleUnitsStack nCurrent, BattleUnitsStack nTarget, int nTurn)
        {
            CurrentArmy = nCurrentArmy;
            EnemyArmy = nEnemyArmy;
            Current = nCurrent;
            Target = nTarget;
            Turn = nTurn;
        }

        public static SkillContext GetSkillContext(BattleArmy nCurrentArmy, BattleArmy nEnemyArmy, BattleUnitsStack nCurrent, BattleUnitsStack nTarget, int nTurn)
        {
            if (SkillContext.Data == null)
            {
                SkillContext.Data = new SkillContext(nCurrentArmy, nEnemyArmy, nCurrent, nTarget, nTurn);
            }
            else
            {
                Data.CurrentArmy = nCurrentArmy;
                Data.EnemyArmy = nEnemyArmy;
                Data.Current = nCurrent;
                Data.Target = nTarget;
                Data.Turn = nTurn;
            }
            return SkillContext.Data;
        }
    }
}
