using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YesCommander.Model;

namespace YesCommander.Classes
{
    public class Solution
    {
        private bool isContainHighmaul;
        private bool isContainRingStage1;
        private bool isContainRingStage2;
        private bool isContainEquipment645;
        private bool isContainBlackFoundry;
        private bool isContainTwoFollowerMissions;
        private bool isContainGarrisonResourceMissions;
        private bool isContainGoldMissions;
        public List<int> uncompleteIDs;
        private Dictionary<int, List<Mission>> dic;
        private List<int> missionIDs;
        private Dictionary<string, int> followerValue;
        private List<KeyValuePair<int, List<Mission>>> orderedList;

        public string HighmaulString;
        public string RingStage1String;
        public string RingStage2String;
        public string Equipment645String;

        public List<Follower> suggestedFollowers;

        public Solution( bool isContainHighmaul, bool isContainRingStage1, bool isContainRingStage2, bool isContainEquipment645, bool isContainBlackFoundry,
            bool isContainTwoFollowerMissions, bool isContainGarrisonResourceMissions, bool isContainGoldMissions )
        {
            this.isContainHighmaul = isContainHighmaul;
            this.isContainRingStage1 = isContainRingStage1;
            this.isContainRingStage2 = isContainRingStage2;
            this.isContainEquipment645 = isContainEquipment645;
            this.isContainBlackFoundry = isContainBlackFoundry;
            this.isContainTwoFollowerMissions = isContainTwoFollowerMissions;
            this.isContainGarrisonResourceMissions = isContainGarrisonResourceMissions;
            this.isContainGoldMissions = isContainGoldMissions;
        }

        public void CalculateBasicData( Missions allMissions, List<Follower> allFollowers )
        {
            this.uncompleteIDs = new List<int>();
            this.dic = new Dictionary<int, List<Mission>>();
            this.missionIDs = new List<int>();
            this.followerValue = new Dictionary<string, int>();
            this.orderedList = new List<KeyValuePair<int, List<Mission>>>();

            if ( isContainHighmaul )
            {
                for ( int i = 313; i <= 316; i++ )
                    missionIDs.Add( i );
            }
            if ( isContainRingStage1 )
            {
                for ( int i = 403; i <= 407; i++ )
                    missionIDs.Add( i );
            }
            if ( isContainRingStage2 )
            {
                for ( int i = 408; i <= 413; i++ )
                    missionIDs.Add( i );
            }
            if ( isContainEquipment645 )
            {
                for ( int i = 290; i <= 296; i++ )
                    missionIDs.Add( i );
            }
            if ( this.isContainBlackFoundry )
            {
                for ( int i = 427; i <= 430; i++ )
                    missionIDs.Add( i );
            }
            if ( this.isContainTwoFollowerMissions )
            {
                missionIDs.Add( 379 );
                missionIDs.Add( 381 );
                missionIDs.Add( 391 );
                for ( int i = 394; i <= 399; i++ )
                    missionIDs.Add( i );
                missionIDs.Add( 401 );
                missionIDs.Add( 402 );
                missionIDs.Add( 444 );
                missionIDs.Add( 445 );
                missionIDs.Add( 495 );
                missionIDs.Add( 496 );
                missionIDs.Add( 503 );
            }
            if ( this.isContainGarrisonResourceMissions )
            {
                missionIDs.Add( 132 );
                missionIDs.Add( 133 );
                missionIDs.Add( 268 );
                missionIDs.Add( 269 );
                missionIDs.Add( 311 );
                missionIDs.Add( 312 );
            }
            if ( this.isContainGoldMissions )
                foreach ( int i in Globals.missionIdForGold )
                    if ( !missionIDs.Contains( i ) )
                        missionIDs.Add( i );

            foreach ( int i in missionIDs )
            {
                Mission theMission = allMissions.AllMissions[ i ];
                List<Mission> missions=null;
                if ( theMission.FollowersNeed == 3 )
                    missions = this.AssignMissionForThreeFollowers( theMission, allFollowers );
                else if ( theMission.FollowersNeed == 2 )
                    missions = this.AssignMissionForTwoFollowers( theMission, allFollowers );

                var data = from temp in missions.AsEnumerable()
                           where Math.Round( temp.TotalSucessChance, 6 ) >= 1
                           select temp;
                List<Mission> missions2 = new List<Mission>();
                foreach ( Mission mission in data )
                    missions2.Add( mission );

                this.dic.Add( i, missions2 );
            }
            foreach ( KeyValuePair<int, List<Mission>> pair in dic )
            {
                if ( pair.Value.Count == 0 )
                {
                    uncompleteIDs.Add( pair.Key );
                    continue;
                }
                
                foreach ( Follower follower in allFollowers )
                {
                    if ( pair.Value.Exists( x => x.AssignedFollowers.Exists( y => y.ID == follower.ID ) ) )
                    {
                        if ( !followerValue.ContainsKey( follower.ID ) )
                            followerValue.Add( follower.ID, 1 );
                        else
                            followerValue[ follower.ID ]++;
                    }
                }
            }
            this.followerValue = this.followerValue.OrderBy( x => x.Value ).ToDictionary( x => x.Key, y => y.Value );

            var orderList = from temp in dic
                            where temp.Value.Count > 0
                            orderby temp.Value.Count
                            select temp;
            foreach ( KeyValuePair<int, List<Mission>> pair in orderList )
                orderedList.Add( new KeyValuePair<int, List<Mission>>( pair.Key, pair.Value ) );


            //test
            //this.CalculateAllSolutions();
        }

        public void CalculateBasicDataSimple( Missions allMissions, List<Follower> allFollowers )
        {
            this.uncompleteIDs = new List<int>();
            this.missionIDs = new List<int>();

            if ( isContainHighmaul )
            {
                for ( int i = 313; i <= 316; i++ )
                    missionIDs.Add( i );
            }
            if ( isContainRingStage1 )
            {
                for ( int i = 403; i <= 407; i++ )
                    missionIDs.Add( i );
            }
            if ( isContainRingStage2 )
            {
                for ( int i = 408; i <= 413; i++ )
                    missionIDs.Add( i );
            }
            if ( isContainEquipment645 )
            {
                for ( int i = 290; i <= 296; i++ )
                    missionIDs.Add( i );
            }
            if ( this.isContainBlackFoundry )
            {
                for ( int i = 427; i <= 430; i++ )
                    missionIDs.Add( i );
            }
            if ( this.isContainTwoFollowerMissions )
            {
                missionIDs.Add( 379 );
                missionIDs.Add( 381 );
                missionIDs.Add( 391 );
                for ( int i = 394; i <= 399; i++ )
                    missionIDs.Add( i );
                missionIDs.Add( 401 );
                missionIDs.Add( 402 );
                missionIDs.Add( 444 );
                missionIDs.Add( 445 );
                missionIDs.Add( 495 );
                missionIDs.Add( 496 );
                missionIDs.Add( 503 );
            }
            if ( this.isContainGarrisonResourceMissions )
            {
                missionIDs.Add( 132 );
                missionIDs.Add( 133 );
                missionIDs.Add( 268 );
                missionIDs.Add( 269 );
                missionIDs.Add( 311 );
                missionIDs.Add( 312 );
            }
            if ( this.isContainGoldMissions )
                foreach ( int i in Globals.missionIdForGold )
                    if ( !missionIDs.Contains( i ) )
                        missionIDs.Add( i );


            foreach ( int i in missionIDs )
            {
                Mission theMission = allMissions.AllMissions[ i ];
                List<Mission> missions = null;
                if ( theMission.FollowersNeed == 3 )
                    missions = this.AssignMissionForThreeFollowers( theMission, allFollowers );
                else if ( theMission.FollowersNeed == 2 )
                    missions = this.AssignMissionForTwoFollowers( theMission, allFollowers );
                var data = from temp in missions.AsEnumerable()
                           where Math.Round( temp.TotalSucessChance, 6 ) >= 1
                           select temp;
                if ( data.Count() == 0 )
                    uncompleteIDs.Add( i );
            }

        }

        public void ListAllPossiblities()
        {
            this.suggestedFollowers = new List<Follower>();

            if ( Globals.KeptFollowers.Count > 0 )
                this.suggestedFollowers.AddRange( Globals.KeptFollowers );

            Dictionary<int,Mission> cleanMissions = new Dictionary<int, Mission>();
            int number = 0;
            if ( this.orderedList.Count == 0 )
                return;
            KeyValuePair<int, List<Mission>> pair = this.orderedList[ number ];
            number++;
            List<Mission> result = this.GetHighestValueMissions( pair.Value, this.followerValue );
            cleanMissions.Add( pair.Key, result[ 0 ] );
            this.AddFollowerIntoList( this.suggestedFollowers, result[ 0 ].AssignedFollowers[ 0 ] );
            this.AddFollowerIntoList( this.suggestedFollowers, result[ 0 ].AssignedFollowers[ 1 ] );
            if ( result[ 0 ].AssignedFollowers.Count > 2 )
                this.AddFollowerIntoList( this.suggestedFollowers, result[ 0 ].AssignedFollowers[ 2 ] );

            while ( number < this.orderedList.Count )
            {
                pair = this.orderedList[ number ];
                List<Mission> resultNew = this.GetHighestValueMissions( pair.Value, this.followerValue, suggestedFollowers );
                cleanMissions.Add( pair.Key, resultNew[ 0 ] );
                foreach ( Follower follower in resultNew[ 0 ].AssignedFollowers )
                {
                    if ( !suggestedFollowers.Exists( x => x.ID == follower.ID ) )
                        suggestedFollowers.Add( follower );
                }
                number++;
            }
        }

        public void ReduceRedundency(Missions allMissions )
        {
            this.ReCalculateMissions( allMissions, this.suggestedFollowers );

            foreach ( KeyValuePair<string, int> pair in this.followerValue )
            {
                if ( !this.suggestedFollowers.Exists( x => x.ID == pair.Key ) )
                    continue;
                if ( Globals.KeptFollowers.Contains( this.suggestedFollowers.First( x => x.ID == pair.Key ) ) )
                    continue;

                bool isNecessary = false;
                foreach ( KeyValuePair<int, List<Mission>> listPair in this.dic )
                {
                    if ( listPair.Value.Count>0 && !listPair.Value.Exists( x => !x.AssignedFollowers.Exists( y => y.ID == pair.Key ) ) )
                    {
                        isNecessary = true;
                        break;
                    }
                }
                if ( isNecessary )
                    continue;
                else
                {
                    this.suggestedFollowers.Remove( this.suggestedFollowers.First( x => x.ID == pair.Key ) );
                    this.ReCalculateMissions( allMissions, this.suggestedFollowers );
                }

            }
        }

        private void ReCalculateMissions( Missions allMissions, List<Follower> allFollowers )
        {
            this.dic = new Dictionary<int, List<Mission>>();
            foreach ( int i in missionIDs )
            {
                Mission theMission = allMissions.AllMissions[ i ];
                List<Mission> missions = null;
                if ( theMission.FollowersNeed == 3 )
                    missions = this.AssignMissionForThreeFollowers( theMission, allFollowers );
                else if ( theMission.FollowersNeed == 2 )
                    missions = this.AssignMissionForTwoFollowers( theMission, allFollowers );
                var data = from temp in missions.AsEnumerable()
                           where Math.Round( temp.TotalSucessChance, 6 ) >= 1
                           select temp;
                List<Mission> missions2 = new List<Mission>();
                foreach ( Mission mission in data )
                    missions2.Add( mission );

                this.dic.Add( i, missions2 );
            }
        }


        private List<Mission> AssignMissionForThreeFollowers( Mission mission, List<Follower> list )
        {
            List<Mission> missions = new List<Mission>();
            if ( mission.MissionReward.Contains( "要塞物资" ) )
                list = list.FindAll( x => x.TraitCollection.Contains( Follower.Traits.Scavenger ) );
            else if ( mission.MissionReward.Contains( "G" ) )
                list = list.FindAll( x => x.TraitCollection.Contains( Follower.Traits.TreasureHunter ) );
            if ( list.Count >= 3 )
            {
                for ( int i = 0; i < list.Count; i++ )
                {
                    for ( int j = 0; j < list.Count; j++ )
                    {
                        if ( j <= i )
                            continue;
                        for ( int k = 0; k < list.Count; k++ )
                        {
                            if ( k <= j )
                                continue;
                            else
                            {
                                Mission newMission = mission.Copy();
                                newMission.IsUsingMaxiLevel = Globals.IsUsingMaxILevelOnSimulateAll;
                                newMission.AssignFollowers( new List<Follower>() { list[ i ], list[ j ], list[ k ] } );
                                missions.Add( newMission );
                            }
                        }
                    }
                }
            }
            return missions;
        }

        private List<Mission> AssignMissionForTwoFollowers( Mission mission, List<Follower> list )
        {
            List<Mission> missions = new List<Mission>();
            if ( mission.MissionReward.Contains( "要塞物资" ) )
                list = list.FindAll( x => x.TraitCollection.Contains( Follower.Traits.Scavenger ) );
            else if ( mission.MissionReward.Contains( "G" ) )
                list = list.FindAll( x => x.TraitCollection.Contains( Follower.Traits.TreasureHunter ) );
            for ( int j = 0; j < list.Count; j++ )
            {
                for ( int k = 0; k < list.Count; k++ )
                {
                    if ( k <= j )
                        continue;
                    else
                    {
                        Mission newMission = mission.Copy();
                        newMission.IsUsingMaxiLevel = Globals.IsUsingMaxILevelOnSimulateAll;
                        newMission.AssignFollowers( new List<Follower>() { list[ j ], list[ k ] } );
                        missions.Add( newMission );
                    }
                }
            }
            return missions;
        }

        public int GetPartyValue( Mission mission, Dictionary<string, int> followerValue )
        {
            int returnValue = 0;
            foreach ( Follower follower in mission.AssignedFollowers )
            {
                returnValue += followerValue[ follower.ID ];
            }
            return returnValue;
        }

        public List<Mission> GetHighestValueMissions( List<Mission> list, Dictionary<string, int> followerValue )
        {
            if ( list == null || list.Count == 0 )
                return null;

            List<Mission> returnValuie = new List<Mission>();
            var data = from temp in list
                       orderby this.GetPartyValue( temp, followerValue ) descending
                       select temp;
            foreach ( Mission mission in data )
            {
                if ( this.GetPartyValue( mission, followerValue ) == this.GetPartyValue( data.First() as Mission, followerValue ) )
                    returnValuie.Add( mission );
            }
            return returnValuie;
        }

        public int GetContainsValue( Mission mission, List<Follower> list )
        {
            int returnValue = 0;
            if ( list.Exists( x => x.ID == mission.AssignedFollowers[ 0 ].ID ) )
                returnValue++;
            if ( list.Exists( x => x.ID == mission.AssignedFollowers[ 1 ].ID ) )
                returnValue++;

            if ( mission.AssignedFollowers.Count > 2 && list.Exists( x => x.ID == mission.AssignedFollowers[ 2 ].ID ) )
                returnValue++;
            return returnValue;
        }

        private List<Mission> GetHighestValueMissions( List<Mission> list, Dictionary<string, int> followerValue, List<Follower> followers )
        {
            if ( list == null || list.Count == 0 )
                return null;

            List<Mission> returnValuie = new List<Mission>();
            var data = from temp in list
                       orderby this.GetContainsValue( temp, followers ) descending
                       orderby this.GetPartyValue( temp, followerValue ) descending
                       select temp;
            //data = data.OrderByDescending( x => this.GetContainsValue( x, followers ) ).ThenByDescending( x => this.GetHighestValueMissions( list, followerValue ) );

            returnValuie.Add( data.First() );
            return returnValuie;
        }

        private void CalculateAllSolutions()
        {
            Dictionary<int, List<Mission>> diction1 = new Dictionary<int, List<Mission>>();
            foreach ( KeyValuePair<int, List<Mission>> pair in this.dic )
                diction1.Add( pair.Key,pair.Value );




            foreach ( int id in this.missionIDs )
            {
                Dictionary<int, List<Mission>> tempdiction = new Dictionary<int, List<Mission>>();
                if ( this.uncompleteIDs.Contains( id ) )
                    continue;

                foreach ( Mission mission in diction1[id] )
                {
                    //tempdiction.Add( id,  );
                }
            }

        }

        private void AddFollowerIntoList( List<Follower> list, Follower follower )
        {
            if ( !list.Contains( follower ) )
                list.Add( follower );
        }
    }
}
