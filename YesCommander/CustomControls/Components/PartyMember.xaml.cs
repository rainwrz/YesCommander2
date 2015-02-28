using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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

namespace YesCommander.CustomControls.Components
{
    /// <summary>
    /// Interaction logic for PartyMember.xaml
    /// </summary>
    public partial class PartyMember : UserControl
    {
        public PartyMember()
        {
            InitializeComponent();
        }

        public void PlaceFollower( Follower follower, Mission mission )
        {
            this.followerName.Text = follower.Name;
            this.followerIlevel.Text = "(" + follower.ItemLevel.ToString() + ")";
            if ( !follower.IsActive )
                this.followerFrozen.Text = "(冻结)";

            if ( follower.Quolaty == 5 )
                this.followerName.Foreground = Brushes.OrangeRed;
            else if ( follower.Quolaty == 4 )
                this.followerName.Foreground = Brushes.BlueViolet;
            else if ( follower.Quolaty == 3 )
                this.followerName.Foreground = Brushes.DodgerBlue;
            else if ( follower.Quolaty == 2 )
                this.followerName.Foreground = Brushes.Lime;
            this.followerIlevel.Foreground = follower.ItemLevel >= mission.ItemLevelNeed ? Brushes.Lime : Brushes.Red;

            this.followerImages.Children[ 0 ].Visibility = System.Windows.Visibility.Visible;
            ( ( this.followerImages.Children[ 0 ] as StackPanel ).Children[ 0 ] as TinyImage ).SetUp( follower.AbilityCollection[ 0 ] );
            if ( mission.CounterAbilitiesCollection.ContainsKey( follower.AbilityCollection[ 0 ] ) )
            {
                ( this.followerImages.Children[ 0 ] as StackPanel ).Background = Globals.BorderBrush;
            }

            if ( follower.AbilityCollection.Count > 1 )
            {
                this.followerImages.Children[ 1 ].Visibility = System.Windows.Visibility.Visible;
                ( ( this.followerImages.Children[ 1 ] as StackPanel ).Children[ 0 ] as TinyImage ).SetUp( follower.AbilityCollection[ 1 ] );
                if ( mission.CounterAbilitiesCollection.ContainsKey( follower.AbilityCollection[ 1 ] ) )
                {
                    ( this.followerImages.Children[ 1 ] as StackPanel ).Background = Globals.BorderBrush;
                }
            }
            int i = 2;
            foreach ( Follower.Traits trait in follower.TraitCollection )
            {
                if ( mission.partyBuffs.Contains( trait ) )
                {
                    if ( Follower.IsRaceLoverTrait( trait ) && !mission.AssignedFollowers.Exists( x => ( x.Name != follower.Name && Follower.GetRaceMatchedByTrait( trait ).Contains( x.Race ) ) ) )
                    {
                        continue;
                    }
                    this.followerImages.Children[ i ].Visibility = System.Windows.Visibility.Visible;
                    ( ( this.followerImages.Children[ i ] as StackPanel ).Children[ 0 ] as TinyImage ).SetUp( trait );
                    i++;
                }
            }
            //string nameEn = Globals.IsAlliance ? Globals.AllFollowersAli.Rows.OfType<DataRow>().First( x => x[ "ID" ].ToString() == follower.ID )[ "英文名字" ].ToString() :
            //    Globals.AllFollowersHrd.Rows.OfType<DataRow>().First( x => x[ "ID" ].ToString() == follower.ID )[ "英文名字" ].ToString();
            //this.head.PopulateFullImage( nameEn );

            this.ToolTip = new FollowerPanel( follower, Globals.GetFollowerEnNameById( follower.ID, Globals.IsAlliance) );
            ToolTipService.SetInitialShowDelay( this, 0 );
        }

        public void Clear()
        {
            this.followerName.Text = string.Empty;
            this.followerIlevel.Text = string.Empty;
            this.followerFrozen.Text = string.Empty;
            foreach ( StackPanel panel in this.followerImages.Children )
            {
                panel.Background = null;
                panel.Visibility = System.Windows.Visibility.Collapsed;
                ( panel.Children[ 0 ] as TinyImage ).ClearUp();
            }
        }
    }
}
