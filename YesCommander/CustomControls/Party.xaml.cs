using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YesCommander.Classes;
using YesCommander.CustomControls.Components;

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for Party.xaml
    /// </summary>
    public partial class Party : UserControl
    {
        //public List<string> nameEns;
        public Party()
        {
            InitializeComponent();
        }
        public Party( Mission mission )
        {
            InitializeComponent();
            //this.nameEns = nameEns;
            this.Clear();
            this.Create( mission );
        }

        public void Create( Mission mission )
        {
            this.Clear();
            this.Visibility = System.Windows.Visibility.Visible;
            this.member1.PlaceFollower( mission.AssignedFollowers[ 0 ], mission );
            if ( mission.AssignedFollowers.Count > 1 )
                this.member2.PlaceFollower( mission.AssignedFollowers[ 1 ], mission );
            if ( mission.AssignedFollowers.Count > 2 )
                this.member3.PlaceFollower( mission.AssignedFollowers[ 2 ], mission );
            this.PlacePartyBuff( mission );
        }

        public void PlacePartyBuff( Mission mission )
        {
            int i = 0;
            foreach ( Follower.Traits trait in mission.partyBuffs )
            {
                if ( trait == Follower.Traits.Unknow )
                    continue;
                ( this.partyBuffs.Children[ i ] as TinyImage ).SetUp( trait );
                ( this.partyBuffs.Children[ i ] as TinyImage ).Visibility = System.Windows.Visibility.Visible;
                i++;
            }
            int k = 0;
            foreach ( KeyValuePair<Follower.Abilities, float> pair in mission.CounterAbilitiesLack )
            {
                if ( k >= this.lacks.Children.Count )
                    break;
                for ( int j = 0; j < pair.Value; j++ )
                {
                    ( this.lacks.Children[ k ] as TinyImage ).SetUp( pair.Key );
                    ( this.lacks.Children[ k ] as TinyImage ).Visibility = System.Windows.Visibility.Visible;
                    k++;
                }
            }

            this.timeNeed.Text = mission.MissionTimeCaculatedStr;
            this.timeNeed.Foreground = mission.partyBuffs.Contains( Follower.Traits.EpicMount ) ? Brushes.Lime : Brushes.White;
            double sucessChance = 100 * mission.TotalSucessChance;

            if ( ( mission.MissionReward.Contains( "要塞物资" ) || mission.MissionReward.Contains( "G" ) || mission.MissionReward.Contains( "油" ) ) && mission.FollowersNeed > 1 )
            {
                this.sucessChance.Text = mission.CurrencyReward.ToString( "#,##0", new CultureInfo( "en-US" ) ) + "(" + sucessChance.ToString( "#,##0.##", new CultureInfo( "en-US" ) ) + "%)";
            }
            else
            {
                this.sucessChance.Text = sucessChance.ToString( "#,##0.##", new CultureInfo( "en-US" ) );
            }
            this.sucessChance.Foreground = sucessChance >= 100 ? Brushes.Lime : Brushes.Red;
            if ( this.sucessChance.Text == "100" )
                this.sucessChance.Foreground = Brushes.Lime;
        }

        public void Clear()
        {
            this.sucessChance.Text = string.Empty;
            this.timeNeed.Text = string.Empty;
            this.member1.Clear();
            this.member2.Clear();
            this.member3.Clear();
            foreach ( TinyImage image in this.partyBuffs.Children)
            {
                image.ClearUp();
                image.Visibility = System.Windows.Visibility.Collapsed;
            }
            foreach ( TinyImage image in this.lacks.Children )
            {
                image.ClearUp();
                image.Visibility = System.Windows.Visibility.Collapsed;
            }
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void Collapsed()
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
