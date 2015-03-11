using System;
using System.Collections.Generic;
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
    /// Interaction logic for FollowerRow.xaml
    /// </summary>
    public partial class FollowerRow : UserControl
    {
        public Follower currentFollower;
        private bool isFavoriteMode = false;

        public FollowerRow()
        {
            InitializeComponent();
        }

        public FollowerRow( Follower follower, List<Follower> list, string nameEN, bool isFavorite = false )
        {
            InitializeComponent();
            this.currentFollower = follower;
            this.isFavoriteMode = isFavorite;
            this.SetFollower( follower );
            if ( isFavorite )
            {
                this.isFavorit.IsChecked = true;
                this.isFavorit.IsEnabled = false;
                this.isIgnored.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                this.isFavorit.IsChecked = list.Count > 0 && list.Contains( follower ) ? true : false;
            }
            this.isIgnored.IsChecked = !Globals.CurrentValidFollowers.Contains( this.currentFollower );
            this.followerHead.PopulateFullImage( nameEN );
        }

        /// <summary>
        /// For disabled faverite check box
        /// </summary>
        /// <param name="follower"></param>
        /// <param name="isFavorite"></param>
        public FollowerRow( Follower follower, string nameEN, bool isFavorite )
        {
            InitializeComponent();
            this.isFavoriteMode = true;
            this.isFavorit.IsEnabled = false;
            this.isIgnored.IsEnabled = false;
            this.isFavorit.IsChecked = isFavorite;
            this.currentFollower = follower;
            this.isIgnored.IsChecked = !Globals.CurrentValidFollowers.Contains( follower );
            this.SetFollower( follower );
            this.isIgnored.IsChecked = !Globals.CurrentValidFollowers.Contains( this.currentFollower );
            this.followerHead.PopulateFullImage( nameEN );
        }

        public void Clear()
        {
            this.textName.Text = string.Empty;
            this.textRace.Text = string.Empty;
            this.textClass.Text = string.Empty;
            this.specIcon.Clear();
            this.raceIcon.Source = null;
            this.raceIcon.Visibility = System.Windows.Visibility.Collapsed;
            this.textLevel.Text = string.Empty;
            this.textItemLevel.Text = string.Empty;
            this.textIsFrozen.Text = string.Empty;
            foreach ( TinyImage image in this.abilities.Children )
            {
                image.ClearUp();
            }
            foreach ( TinyImage image in this.possibleAblities.Children )
            {
                image.ClearUp();
            }
            foreach ( TinyImage image in this.traits.Children )
            {
                image.ClearUp();
            }
        }

        private void SetFollower( Follower follower )
        {
            this.Clear();

            this.textName.Text = follower.Name;
            if ( follower.Quolaty == 5 )
                this.textName.Foreground = Brushes.OrangeRed;
            else if ( follower.Quolaty == 4 )
                this.textName.Foreground = Brushes.BlueViolet;
            else if ( follower.Quolaty == 3 )
                this.textName.Foreground = Brushes.DodgerBlue;
            else if ( follower.Quolaty == 2 )
                this.textName.Foreground = Brushes.Lime;

            if ( (int)follower.Race >= 0 && (int)follower.Race <= 12 )
            {
                this.raceIcon.Visibility = System.Windows.Visibility.Visible;
                this.raceIcon.Source = Globals.TraitImageSource[ Follower.GetTraitOfLoverByRace( follower.Race ) ];
                TextBlock toolTip = new TextBlock();
                toolTip.Text = follower.Race.ToString();
                this.raceIcon.ToolTip =  toolTip;
                ToolTipService.SetInitialShowDelay( this.raceIcon, 0 );
            }
            else
                this.textRace.Text = follower.Race.ToString();
            this.specIcon.IconIndex = (int)follower.Class;
            this.textClass.Text = Follower.GetCNStringByClass( follower.Class );
            this.textLevel.Text = follower.Level.ToString();
            this.textItemLevel.Text = follower.ItemLevel.ToString();
            if ( follower.ItemLevel >=645 )
                this.textItemLevel.Foreground = Brushes.BlueViolet;
            else if ( follower.ItemLevel >= 630 )
                this.textItemLevel.Foreground = Brushes.DodgerBlue;
            else if ( follower.ItemLevel >= 600 )
                this.textItemLevel.Foreground = Brushes.Lime;
            if ( !follower.IsActive )
                this.textIsFrozen.Text = "已冻结";

            int i=0;
            foreach ( Follower.Abilities ability in follower.AbilityCollection )
            {
                ( this.abilities.Children[ i ] as TinyImage ).SetUp( ability );
                i++;
            }
            i = 0;
            foreach ( Follower.Traits trait in follower.TraitCollection )
            {
                ( this.traits.Children[ i ] as TinyImage ).SetUp( trait );
                i++;
            }
            if ( follower.Quolaty < 4 )
            {
                List<Follower.Abilities> ablities = Follower.GetAbilityFromClass( follower.Class );
                ablities.Remove( follower.AbilityCollection[ 0 ] );
                for ( int j = 0; j < ablities.Count; j++ )
                {
                    ( this.possibleAblities.Children[ j ] as TinyImage ).SetUp( ablities[ j ] );
                }
            }
        }

        private void isFavorit_Checked( object sender, RoutedEventArgs e )
        {
            if ( !this.isFavoriteMode && !Globals.FavoriteFollowers.Contains( this.currentFollower ) )
                Globals.FavoriteFollowers.Add( this.currentFollower );
            if ( !this.isFavoriteMode && this.isIgnored.IsChecked == true )
                this.isIgnored.IsChecked = false;
        }

        private void isFavorit_Unchecked( object sender, RoutedEventArgs e )
        {
            if ( !this.isFavoriteMode )
            Globals.FavoriteFollowers.Remove( this.currentFollower );
        }

        private void isIgnored_Checked( object sender, RoutedEventArgs e )
        {
            if ( this.isFavorit.IsChecked == true )
                this.isFavorit.IsChecked = false;
            this.Opacity = 0.2;
            Globals.CurrentValidFollowers.Remove( this.currentFollower );
        }

        private void isIgnored_Unchecked( object sender, RoutedEventArgs e )
        {
            this.Opacity = 1;
            Globals.CurrentValidFollowers.Add( this.currentFollower );
        }

    }
}
