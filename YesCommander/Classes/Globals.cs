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

        public static void Initialize()
        {
            InitialImageSources();
            AllFollowersAli = LoadData.LoadMissionFile( "Txts/ALI.txt" );
            AllFollowersHrd = LoadData.LoadMissionFile( "Txts/HRD.txt" );
            AliFollowerSkills = LoadData.LoadMissionFile( "Txts/AliSkill.txt" );
            HrdFollowerSkills = LoadData.LoadMissionFile( "Txts/HrdSkill.txt" );
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
