using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;

namespace YesCommander.Classes
{
    public class Globals
    {
        private Globals(){}

        public static Dictionary<Follower.Abilities, ImageSource> AbilityImageSource;
        public static Dictionary<Follower.Traits, ImageSource> TraitImageSource;
        public static Dictionary<string, ImageSource> FollowerImageSourceSmall;
        public static Dictionary<string, ImageSource> FollowerImageSourceBig;
        public static ImageBrush BorderBrush;
        public static List<ImageSource> missionIcionList;
        public static List<ImageSource> specIcionList;
        public static bool IsAlliance = true;
        public static bool IsUsingMaxILevelOnSimulateAll = true;
        public static DataTable AllFollowersAli;
        public static DataTable AllFollowersHrd;
        public static DataTable AliFollowerSkills;
        public static DataTable HrdFollowerSkills;
        public static List<Follower> AliFollowers;
        public static List<Follower> HrdFollowers;
        public static List<AbilityCombinationModel> combinationModelList;
        public static List<Follower> CurrentValidFollowers;
        public static List<Follower> AllFollowers;
        public static List<Follower> FavoriteFollowers;
        public static List<Follower> KeptFollowers;
        public static List<int> missionIdForGold = new List<int>();
        public static List<int> missionIdForGarrisonResource = new List<int>();
        public static Dictionary<Follower.Races, int> AliFemaleRaceAmount;
        public static Dictionary<Follower.Races, int> HrdFemaleRaceAmount;


        public static void Initialize()
        {
            InitialImageSources();
            InitializeFollowerList();
            InitializeCombination();
        }

        private static void InitialImageSources()
        {
            AbilityImageSource = new Dictionary<Follower.Abilities, ImageSource>();
            TraitImageSource = new Dictionary<Follower.Traits, ImageSource>();
            FollowerImageSourceSmall = new Dictionary<string, ImageSource>();
            FollowerImageSourceBig = new Dictionary<string, ImageSource>();
            missionIcionList = new List<ImageSource>();
            specIcionList = new List<ImageSource>();
            for ( int i = 0; i < 9; i++ )
            {
                Follower.Abilities ability = (Follower.Abilities)i;
                AbilityImageSource.Add( ability, Follower.GetImageFromAbility( ability ) );
            }
            for ( int i = 0; i < 63; i++ )
            {
                Follower.Traits trait = (Follower.Traits)i;
                TraitImageSource.Add( trait, Follower.GetImageFromFromTrait( trait ) );
            }
            BorderBrush = new ImageBrush( new BitmapImage( new Uri(
               "pack://application:,,,/YesCommander;component/Resources/imageBorder.png", UriKind.RelativeOrAbsolute ) ) );
            BorderBrush.Stretch = Stretch.None;

            missionIcionList.Add( Follower.GetImageFromPicName( "Treasures.jpg" ) );
            missionIcionList.Add( Follower.GetImageFromPicName( "archstone.jpg" ) );
            missionIcionList.Add( Follower.GetImageFromPicName( "runestone.jpg" ) );
            missionIcionList.Add( Follower.GetImageFromPicName( "Treasures2.jpg" ) );
            missionIcionList.Add( Follower.GetImageFromPicName( "Garrison_resource.jpg" ) );
            missionIcionList.Add( Follower.GetImageFromPicName( "Gold.jpg" ) );

            int[] classIndexList = new int[] { 9, 0, 4, 2, 10, 8, 6, 1, 5, 7, 3 };
            foreach ( int classIndex in classIndexList )
            {
                for ( int i = 0; i < ( classIndex == 0 ? 4 : 3 ); i++ )
                {
                    specIcionList.Add( new CroppedBitmap( new BitmapImage( new Uri(
                        "pack://application:,,,/YesCommander;component/Resources/Specs-20.png", UriKind.RelativeOrAbsolute ) ), new Int32Rect( 20 * i, classIndex*20, 20, 20 ) ) );
                }
            }
        }

        private static void InitializeFollowerList()
        {
            AllFollowersAli = LoadData.LoadMissionFile( "Txts/ALI.txt" );
            AllFollowersHrd = LoadData.LoadMissionFile( "Txts/HRD.txt" );
            AliFollowerSkills = LoadData.LoadMissionFile( "Txts/AliSkill.txt" );
            HrdFollowerSkills = LoadData.LoadMissionFile( "Txts/HrdSkill.txt" );

            AliFollowers = new List<Follower>();
            HrdFollowers = new List<Follower>();
            AddFollowerIntoList( AliFollowers, Globals.AllFollowersAli.Rows, Globals.AliFollowerSkills.Rows );
            AddFollowerIntoList( HrdFollowers, Globals.AllFollowersHrd.Rows, Globals.HrdFollowerSkills.Rows );

            AliFemaleRaceAmount = new Dictionary<Follower.Races, int>();
            HrdFemaleRaceAmount = new Dictionary<Follower.Races, int>();
            Globals.CountAmountFemale();
        }

        private static void CountAmountFemale()
        {
            foreach ( var ra in Enum.GetValues( typeof( Follower.Races ) ) )
            {
                Follower.Races race = (Follower.Races)ra;
                Globals.AliFemaleRaceAmount.Add( race, Globals.AliFollowers.Count( x => x.Race == race && x.IsFemale ) );
                Globals.HrdFemaleRaceAmount.Add( race, Globals.HrdFollowers.Count( x => x.Race == race && x.IsFemale ) );
            }
        }

        private static void AddFollowerIntoList( List<Follower> list, DataRowCollection followerRows,DataRowCollection skillRows )
        {
            List<int> abilityList;
            List<int> traitList;
            DataRow currentRow;
            foreach ( DataRow row in followerRows )
            {
                int quolaty;
                switch ( row[ "初始品质" ].ToString() )
                {
                    case "传奇": quolaty = 5; break;
                    case "史诗": quolaty = 4; break;
                    case "精良": quolaty = 3; break;
                    case "优秀": quolaty = 2; break;
                    default: quolaty = 2; break;
                }
                abilityList = new List<int>();
                currentRow = skillRows.OfType<DataRow>().First( x => x[ "ID" ].ToString() == row[ "ID" ].ToString() );
                if ( !string.IsNullOrEmpty( currentRow[ "应对ID" ].ToString() ) )
                    abilityList.Add( Convert.ToInt16( currentRow[ "应对ID" ] ) );
                traitList = new List<int>();
                if ( !string.IsNullOrEmpty( currentRow[ "特长ID" ].ToString() ) )
                    traitList.Add( Convert.ToInt16( currentRow[ "特长ID" ] ) );

                list.Add( new Follower( row[ "ID" ].ToString(), row[ "英文名字" ].ToString(), quolaty, Convert.ToInt16( row[ "初始等级" ] ), 600, row[ "种族" ].ToString(),
                    Follower.GetClassByStr( row[ "职业" ].ToString(), row[ "专精" ].ToString() ), string.Empty, 1, abilityList, traitList, row[ "性别" ].ToString() == "1",
                    row[ "英文名字" ].ToString(), row[ "简体名字" ].ToString(), row[ "繁体名字" ].ToString() ) );
            }
        }

        private static void InitializeCombination()
        {
            combinationModelList = GetAllCombinations();
        }

        private static List<AbilityCombinationModel> GetAllCombinations()
        {
            List<AbilityCombinationModel> list = new List<AbilityCombinationModel>();

            for ( int i = 0; i < 9; i++ )
            {
                Follower.Abilities ability1 = (Follower.Abilities)i;
                for ( int j = i + 1; j< 9; j++ )
                {
                    Follower.Abilities ability2 = (Follower.Abilities)j;
                    AbilityCombinationModel ac = new AbilityCombinationModel();
                    ac.Ability1 = ability1;
                    ac.Ability2 = ability2;
                    AddClassesIntoModel( ac );
                    list.Add( ac );
                }
            }

            AbilityCombinationModel acs = new AbilityCombinationModel();
            acs.Ability1 = Follower.Abilities.TimedBattle;
            acs.Ability2 = Follower.Abilities.TimedBattle;
            AddClassesIntoModel( acs );
            list.Add( acs );
            return list.OrderBy( x => x.ListOfClasses.Count ).ThenBy( y => y.NumberOfFollowersForAli ).ToList<AbilityCombinationModel>();
        }

        private static void AddClassesIntoModel( AbilityCombinationModel model )
        {
            for ( int j = 0; j < 34; j++ )
            {
                List<Follower.Abilities> classAbilities = Follower.GetAbilityFromClass( (Follower.Classes)j );
                if ( classAbilities.Contains( model.Ability1 ) )
                {
                    classAbilities.Remove( model.Ability1 );
                    if ( classAbilities.Contains( model.Ability2 ) )
                    {
                        model.ListOfClasses.Add( (Follower.Classes)j );
                        model.NumberOfFollowersForAli += Globals.AliFollowers.Count( x => x.Class == (Follower.Classes)j );
                        model.NumberOfFollowersForHrd += Globals.HrdFollowers.Count( x => x.Class == (Follower.Classes)j );
                    }
                }
            }
        }

        public static ImageSource GetFollowerHead( string followerNamePath )
        {
            if ( FollowerImageSourceSmall.ContainsKey( followerNamePath ) )
                return FollowerImageSourceSmall[ followerNamePath ];
            else
            {
                FollowerImageSourceSmall.Add( followerNamePath, Follower.GetImageFromPicName( followerNamePath ) );
                return FollowerImageSourceSmall[ followerNamePath ];
            }
        }

        public static ImageSource GetFollowerBody( string path )
        {
            if ( FollowerImageSourceBig.ContainsKey( path ) )
                return FollowerImageSourceBig[ path ];
            else
            {
                FollowerImageSourceBig.Add( path, GetImageSource( path ) );
                return FollowerImageSourceBig[ path ];
            }
        }

        public static string GetFollowerEnNameById( string id, bool isAli )
        {
            if ( isAli )
                return Globals.AliFollowers.Find( x => x.ID == id ).NameEN;
            else
                return Globals.HrdFollowers.Find( x => x.ID == id ).NameEN;
        }

        private static ImageSource GetImageSource( string path )
        {
            BitmapImage bi = new BitmapImage();
            FileStream f = File.OpenRead( path );
            MemoryStream ms = new MemoryStream();
            f.CopyTo( ms );
            f.Close();

            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }


        /// <summary>
        /// Gets an integer value from the given database row and
        /// returns the default value if it is null.
        /// </summary>
        public static int FetchDatabaseInteger( DataRow dataRow, string columnName, int defaultValue )
        {
            if ( dataRow == null )
            {
                throw new ArgumentNullException( "dataRow" );
            }
            if ( columnName == null )
            {
                throw new ArgumentNullException( "columnName" );
            }

            int returnValue = defaultValue;

            object dataObject = dataRow[ columnName ];
            if ( dataObject != Convert.DBNull )
            {
                returnValue = Convert.ToInt32( dataObject, CultureInfo.InvariantCulture );
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        public static float GetGoldRewardFromString( string reward )
        {
            float returnValue = 0;
            string[] nums;
            if ( reward.Contains( '，' ) )
                nums = reward.Split( '，' );
            else if ( reward.Contains( ',' ) )
                nums = reward.Split( ',' );
            else
                nums = reward.Split( ' ' );
            foreach ( string peice in nums )
            {
                if ( peice.Contains( 'G' ) )
                    returnValue += Convert.ToInt32( Regex.Replace( peice, @"[^\d.\d]", "" ) );
                if ( peice.Contains( 'S' ) )
                    returnValue += ( (float)Convert.ToInt16( Regex.Replace( peice, @"[^\d.\d]", "" ) ) ) / 100;
            }
            return returnValue;
        }
    }
}
