using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

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
        public static bool IsAlliance = true;
        public static DataTable AllFollowersAli;
        public static DataTable AllFollowersHrd;
        public static DataTable AliFollowerSkills;
        public static DataTable HrdFollowerSkills;
        public static List<Follower> AliFollowers;
        public static List<Follower> HrdFollowers;
        public static List<AbilityCombinationModel> combinationModelList;
        public static List<Follower> CurrentFollowers;
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
            for ( int i = 0; i < 9; i++ )
            {
                Follower.Abilities ability = (Follower.Abilities)i;
                AbilityImageSource.Add( ability, Follower.GetImageFromAbility( ability ) );
            }
            for ( int i = 0; i < 55; i++ )
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
        }

        private static void InitializeFollowerList()
        {
            AllFollowersAli = LoadData.LoadMissionFile( "Txts/ALI.txt" );
            AllFollowersHrd = LoadData.LoadMissionFile( "Txts/HRD.txt" );
            AliFollowerSkills = LoadData.LoadMissionFile( "Txts/AliSkill.txt" );
            HrdFollowerSkills = LoadData.LoadMissionFile( "Txts/HrdSkill.txt" );

            DataRow currentRow;
            List<int> abilityList = new List<int>();
            List<int> traitList = new List<int>();
            // Ali
            AliFollowers = new List<Follower>();
            foreach ( DataRow row in Globals.AllFollowersAli.Rows )
            {
                int quolaty;
                switch ( row[ "初始品质" ].ToString() )
                {
                    case "史诗": quolaty = 4; break;
                    case "精良": quolaty = 3; break;
                    case "优秀": quolaty = 2; break;
                    default: quolaty = 2; break;
                }
                abilityList = new List<int>();
                currentRow = Globals.AliFollowerSkills.Rows.OfType<DataRow>().First( x => x[ "ID" ].ToString() == row[ "ID" ].ToString() );
                if ( !string.IsNullOrEmpty( currentRow[ "应对ID" ].ToString() ) )
                    abilityList.Add( Convert.ToInt16( currentRow[ "应对ID" ] ) );
                traitList = new List<int>();
                if ( !string.IsNullOrEmpty( currentRow[ "特长ID" ].ToString() ) )
                    traitList.Add( Convert.ToInt16( currentRow[ "特长ID" ] ) );

                AliFollowers.Add( new Follower( row[ "ID" ].ToString(), row[ "英文名字" ].ToString(), quolaty, Convert.ToInt16( row[ "初始等级" ] ), 600, row[ "种族" ].ToString(),
                    Follower.GetClassByStr( row[ "职业" ].ToString(), row[ "专精" ].ToString() ), string.Empty, 1, abilityList, traitList,
                    row[ "英文名字" ].ToString(), row[ "简体名字" ].ToString(), row[ "繁体名字" ].ToString() ) );
            }
            //Hrd
            HrdFollowers = new List<Follower>();
            foreach ( DataRow row in Globals.AllFollowersHrd.Rows )
            {
                int quolaty;
                switch ( row[ "初始品质" ].ToString() )
                {
                    case "史诗": quolaty = 4; break;
                    case "精良": quolaty = 3; break;
                    case "优秀": quolaty = 2; break;
                    default: quolaty = 2; break;
                }
                abilityList = new List<int>();
                currentRow = Globals.HrdFollowerSkills.Rows.OfType<DataRow>().First( x => x[ "ID" ].ToString() == row[ "ID" ].ToString() );
                if ( !string.IsNullOrEmpty( currentRow[ "应对ID" ].ToString() ) )
                    abilityList.Add( Convert.ToInt16( currentRow[ "应对ID" ] ) );
                traitList = new List<int>();
                if ( !string.IsNullOrEmpty( currentRow[ "特长ID" ].ToString() ) )
                    traitList.Add( Convert.ToInt16( currentRow[ "特长ID" ] ) );

                HrdFollowers.Add( new Follower( row[ "ID" ].ToString(), row[ "英文名字" ].ToString(), quolaty, Convert.ToInt16( row[ "初始等级" ] ), 600, row[ "种族" ].ToString(),
                    Follower.GetClassByStr( row[ "职业" ].ToString(), row[ "专精" ].ToString() ), string.Empty, 1, abilityList, traitList,
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

    }
}
