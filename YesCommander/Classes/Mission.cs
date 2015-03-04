using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace YesCommander.Classes
{
    public class Mission
    {
        public string MissionId;
        public string MissionName;
        public string MissionNameCN;

        public int FollowersNeed;
        public float TotalCounterAbilitiesNeed;
        public int ItemLevelNeed;
        public bool IsUsingMaxiLevel;
        public Follower.Traits SlayerNeed;
        public string MissionTimeStr;
        public float MisstionTimeNeed; //hours
        public float MissionTimeCaculated;
        public string MissionTimeCaculatedStr;
        public string MissionReward;
        public string Remark;
        public Dictionary<Follower.Abilities, float> CounterAbilitiesCollection;
        public Dictionary<Follower.Abilities, float> CounterAbilitiesLack = new Dictionary<Follower.Abilities, float>();
        public List<Follower> AssignedFollowers;

        public double SucessPerAbility;
        public double SucessPerFollower;
        public double SucessPerRaceLover;
        public double SucessPerBurstStamCombatExpSlayer;
        public double SucessPerItemLevel;
        public double TotalSucessChance;
        public float BasicSucessChange;
        public List<Follower.Traits> partyBuffs;
        public static readonly int MAXITEMLEVEL = 675;
        public float DancerSucessFactor = 2 / 3;

        public Mission( string missionId, string missionName, string missionNameCN, int itemLevelNeed, int followersNeed, Dictionary<Follower.Abilities, float> abilities, Follower.Traits slayerNeed, string time, string reward, string remark, float basicSucessChange = 0, bool isUsingMaxiLevel = false )
        {
            this.MissionId = missionId;
            this.MissionName = missionName;
            this.MissionNameCN = missionNameCN;
            this.ItemLevelNeed = itemLevelNeed;
            this.FollowersNeed = followersNeed;
            this.CounterAbilitiesCollection = abilities;
            this.MissionReward = reward;
            this.Remark = remark;
            this.IsUsingMaxiLevel = isUsingMaxiLevel;
            this.BasicSucessChange = basicSucessChange;

            this.TotalCounterAbilitiesNeed = 0;
            foreach ( KeyValuePair<Follower.Abilities, float> pair in abilities )
                this.TotalCounterAbilitiesNeed += pair.Value;

            this.SlayerNeed = slayerNeed;
            if ( time.Contains( "\"" ) )
                time = time.Replace( "\"", "" );
            if ( time.Contains( "," ) )
                time = time.Replace( ",", "" );
            if ( time.Contains( " " ) )
                time = time.Replace( " ", "" );
            this.MissionTimeStr = time;
            if ( time.Contains( 'h' ) )
            {
                string[] tableHead = time.Split( 'h' );
                this.MisstionTimeNeed = Convert.ToInt16( tableHead[ 0 ] );
                if ( tableHead.Length > 1 && tableHead[ 1 ].Contains( 'm' ) )
                    this.MisstionTimeNeed += ( (float) (Convert.ToInt16( tableHead[ 1 ].Replace( "m", "" ) ) )) / 60;
            }
            else
                this.MisstionTimeNeed = ( (float)( Convert.ToInt16( time.Replace( "m", "" ) ) ) ) / 60;
            this.CalculateSucess();
        }

        public Mission Copy()
        {
            return new Mission( this.MissionId, this.MissionName, this.MissionNameCN, this.ItemLevelNeed, this.FollowersNeed, this.CounterAbilitiesCollection, this.SlayerNeed, this.MissionTimeStr, this.MissionReward, this.Remark, this.BasicSucessChange, this.IsUsingMaxiLevel );
        }

        private void CalculateSucess()
        {
            //int stringLength = ( 1 / ( (float)this.TotalCounterAbilitiesNeed + 1 ) ).ToString().Length;
            //stringLength = stringLength > 6 ? 6 : stringLength;
            //string stringValue = ( 1 / ( (float)this.TotalCounterAbilitiesNeed + 1 ) ).ToString().Substring( 0, stringLength );
            //this.SucessPerAbility = Convert.ToDouble( stringValue );
            if ( this.MissionId == "503" )
            {
                this.SucessPerAbility = 1 / ( (float)this.TotalCounterAbilitiesNeed + (float)this.FollowersNeed * 2 / 3 );
                this.SucessPerFollower = this.SucessPerAbility * 2 / 3;
                this.SucessPerRaceLover = this.SucessPerAbility;
                this.SucessPerBurstStamCombatExpSlayer = this.SucessPerAbility * 2 / 3;
                this.DancerSucessFactor = (float)4 / 3;
                this.SucessPerItemLevel = 0;
            }
            else
            {
                this.SucessPerAbility = 1 / ( (float)this.TotalCounterAbilitiesNeed + (float)this.FollowersNeed / 3 );
                this.SucessPerFollower = this.SucessPerAbility / 3;
                this.SucessPerRaceLover = this.SucessPerAbility / 2;
                this.SucessPerBurstStamCombatExpSlayer = this.SucessPerAbility / 3;
                this.DancerSucessFactor = (float)2 / 3;
                this.SucessPerItemLevel = this.SucessPerFollower / 30; //max at this.SucessPerFollower/2
            }
        }

        public void AssignFollowers( List<Follower> followers )
        {
            this.AssignedFollowers = followers;
            this.TotalSucessChance = this.CalculateFinalSucess() * ( 1 - this.BasicSucessChange ) + this.BasicSucessChange;
        }

        private double CalculateFinalSucess()
        {
            double result = 0;
            if ( this.ItemLevelNeed > 0 || ( Convert.ToInt16( this.MissionId ) <= 413 && Convert.ToInt16( this.MissionId ) >= 403 ) )
            {
                foreach ( Follower follower in this.AssignedFollowers )
                {
                    int followerILevel = this.IsUsingMaxiLevel ? MAXITEMLEVEL : follower.ItemLevel;
                    int ilevelIncrease = followerILevel - ( this.ItemLevelNeed == 0 ? 600 : this.ItemLevelNeed );
                    ilevelIncrease = ilevelIncrease > 15 ? 15 : ilevelIncrease;
                    if ( ilevelIncrease > 0 )
                        result += this.SucessPerItemLevel * ilevelIncrease;
                }
            }

            this.partyBuffs = new List<Follower.Traits>();

            if ( this.AssignedFollowers.Count == this.FollowersNeed )
                result += this.FollowersNeed * this.SucessPerFollower;
            // Abilities
            Dictionary<Follower.Abilities, float> followerRemain = new Dictionary<Follower.Abilities, float>();
            foreach ( Follower follower in this.AssignedFollowers )
            {
                foreach ( Follower.Abilities ability in follower.AbilityCollection )
                {
                    float validNumber = 1;
                    if ( this.ItemLevelNeed > 0 || ( Convert.ToInt16( this.MissionId ) <= 413 && Convert.ToInt16( this.MissionId ) >= 403 ) )
                    {
                        float followerILevel = this.IsUsingMaxiLevel ? MAXITEMLEVEL : follower.ItemLevel;
                        float ilevelIncrease = followerILevel - ( this.ItemLevelNeed == 0 ? 600 : this.ItemLevelNeed );
                        ilevelIncrease = ilevelIncrease > 15 ? 15 : ilevelIncrease;
                        if ( ilevelIncrease > 0 )
                            validNumber = 1 + ( ilevelIncrease / 15 ) / 3;
                    }
                    if ( followerRemain.ContainsKey( ability ) )
                        followerRemain[ ability ] += validNumber;
                    else
                        followerRemain.Add( ability, validNumber );
                }
            }
            foreach ( KeyValuePair<Follower.Abilities, float> pair in this.CounterAbilitiesCollection )
            {
                float dancerNumber = 0;
                if ( followerRemain.ContainsKey( pair.Key ) )
                {
                    if ( followerRemain[ pair.Key ] >= pair.Value )
                    {
                        result += this.SucessPerAbility * pair.Value;
                    }
                    else
                    {
                        result += this.SucessPerAbility * followerRemain[ pair.Key ];
                        if ( pair.Key == Follower.Abilities.DangerZones )
                        {
                            float required = pair.Value - followerRemain[ pair.Key ];
                            foreach ( Follower follower in this.AssignedFollowers )
                            {
                                if ( follower.TraitCollection.Contains( Follower.Traits.Dancer ) )
                                    foreach ( Follower.Traits t in follower.TraitCollection )
                                        if ( t == Follower.Traits.Dancer )
                                            dancerNumber++;
                            }
                            if ( dancerNumber > 0 )
                            {
                                if ( dancerNumber * this.DancerSucessFactor >= required )
                                {
                                    result += required * this.SucessPerAbility;
                                }
                                else
                                {
                                    result += ( dancerNumber * this.DancerSucessFactor ) * this.SucessPerAbility;
                                    this.CounterAbilitiesLack.Add( pair.Key, pair.Value - followerRemain[ pair.Key ] - dancerNumber * this.DancerSucessFactor );
                                }
                            }
                            else
                            {
                                this.CounterAbilitiesLack.Add( pair.Key, pair.Value - followerRemain[ pair.Key ] );
                            }
                        }
                        else
                        {
                            this.CounterAbilitiesLack.Add( pair.Key, pair.Value - followerRemain[ pair.Key ] );
                        }
                    }
                }
                else if ( pair.Key == Follower.Abilities.DangerZones )
                {
                    float required = pair.Value;
                    foreach ( Follower follower in this.AssignedFollowers )
                    {
                        if ( follower.TraitCollection.Contains( Follower.Traits.Dancer ) )
                            foreach ( Follower.Traits t in follower.TraitCollection )
                                if ( t == Follower.Traits.Dancer )
                                    dancerNumber++;
                    }
                    if ( dancerNumber > 0 )
                    {
                        if ( dancerNumber * this.DancerSucessFactor >= required )
                        {
                            result += required * this.SucessPerAbility;
                        }
                        else
                        {
                            result += ( dancerNumber * this.DancerSucessFactor ) * this.SucessPerAbility;
                            this.CounterAbilitiesLack.Add( pair.Key, pair.Value - dancerNumber * this.DancerSucessFactor );
                        }
                    }
                    else
                    {
                        this.CounterAbilitiesLack.Add( pair.Key, pair.Value );
                    }
                }
                else
                {
                    this.CounterAbilitiesLack.Add( pair.Key, pair.Value );
                }


                for ( int i = 0; i < dancerNumber; i++ )
                {
                    this.partyBuffs.Add( Follower.Traits.Dancer );
                }
            }

            // Traits
            int numberOfHighStamina = 0;
            int numberOfBurstOfPower = 0;
            float factorOfEpicMount = 1;
            foreach ( Follower follower in this.AssignedFollowers )
            {
                if ( follower.TraitCollection.Contains( this.SlayerNeed ) )
                {
                    result += this.SucessPerBurstStamCombatExpSlayer;
                    this.partyBuffs.Add( this.SlayerNeed );
                }
                if ( follower.TraitCollection.Contains( Follower.Traits.CombatExperience ) )
                {
                    result += this.SucessPerBurstStamCombatExpSlayer;
                    this.partyBuffs.Add( Follower.Traits.CombatExperience );
                }
                if ( follower.TraitCollection.Contains( Follower.Traits.EpicMount ) )
                {
                    factorOfEpicMount *= 2;
                    this.partyBuffs.Add( Follower.Traits.EpicMount );
                }
                if ( follower.TraitCollection.Contains( Follower.Traits.SpeedOfLight ) )
                {
                    factorOfEpicMount *= 2;
                    this.partyBuffs.Add( Follower.Traits.SpeedOfLight );
                }
                if ( follower.TraitCollection.Contains( Follower.Traits.HighStamina ) )
                    numberOfHighStamina++;
                if ( follower.TraitCollection.Contains( Follower.Traits.BurstOfPower ) )
                    numberOfBurstOfPower++;
            }

            float timeNeed = this.MisstionTimeNeed / factorOfEpicMount;
            this.MissionTimeCaculated = timeNeed;
            this.MissionTimeCaculatedStr = string.Empty;
            if ( Math.Truncate( timeNeed ) != 0 )
                this.MissionTimeCaculatedStr = Math.Truncate( timeNeed ).ToString() + "小时";
            double minute = Math.Round( ( timeNeed - Math.Truncate( timeNeed ) ) * 60, 0 );
            if( minute!=0)
                this.MissionTimeCaculatedStr +=minute.ToString() + "分钟";
            if ( timeNeed > 7 )
            {
                result += this.SucessPerBurstStamCombatExpSlayer * numberOfHighStamina;
                for ( int i = 0; i < numberOfHighStamina; i++ )
                    this.partyBuffs.Add( Follower.Traits.HighStamina );
            }
            else
            {
                result += this.SucessPerBurstStamCombatExpSlayer * numberOfBurstOfPower;
                for ( int i = 0; i < numberOfBurstOfPower; i++ )
                    this.partyBuffs.Add( Follower.Traits.BurstOfPower );
            }
            // RaceLover
            if ( this.FollowersNeed == 3 )
            {
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 0 ], this.AssignedFollowers[ 1 ], this.AssignedFollowers[ 2 ] );
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 1 ], this.AssignedFollowers[ 0 ], this.AssignedFollowers[ 2 ] );
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 2 ], this.AssignedFollowers[ 0 ], this.AssignedFollowers[ 1 ] );
            }
            else if ( this.FollowersNeed == 2 )
            {
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 0 ], this.AssignedFollowers[ 1 ] );
                result += this.SingleTraitRaceMatching( this.AssignedFollowers[ 1 ], this.AssignedFollowers[ 0 ] );
            }
            return result;
        }

        private double SingleTraitRaceMatching( Follower follower1, Follower follower2, Follower follower3 = null )
        {
            double result = 0;
            foreach ( Follower.Traits trait in Follower.FilteredRaceTrait( follower1 ) )
            {
                if ( Follower.GetRaceMatchedByTrait( trait ).Contains( follower2.Race ) || ( follower3 != null && Follower.GetRaceMatchedByTrait( trait ).Contains( follower3.Race ) ) )
                {
                    result += this.SucessPerRaceLover;
                    this.partyBuffs.Add( trait );
                }
            }
            return result;
        }


    }
}
