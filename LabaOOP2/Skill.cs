using System;
using System.Collections.Generic;
using System.Text;

namespace LabaOOP2
{
    public class Skill
    {
        public string Title { get; }

        private bool OnOff;
        public bool curOnOff
        {   
            get => OnOff; set
            {
                OnOff = value;
            }
        }

        public Skill(string nTitle, bool nOnOff)
        {
            Title = nTitle;
            OnOff = nOnOff;
        }

        public virtual void Effect(SkillContext Context)
        {

        }
    }
}
