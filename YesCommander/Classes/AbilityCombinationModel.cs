using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YesCommander.Classes
{
    public class AbilityCombinationModel
    {
        public Follower.Abilities Ability1;
        public Follower.Abilities Ability2;
        public List<Follower.Classes> ListOfClasses;
        public int NumberOfFollowersForAli;
        public int NumberOfFollowersForHrd;
        public AbilityCombinationModel()
        {
            this.ListOfClasses = new List<Follower.Classes>();
            this.NumberOfFollowersForAli = 0;
            this.NumberOfFollowersForHrd = 0;
        }
    }
}
