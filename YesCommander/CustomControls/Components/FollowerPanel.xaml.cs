using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for FollowerPanel.xaml
    /// </summary>
    public partial class FollowerPanel : UserControl
    {
        public FollowerPanel(Follower follower, string nameEn )
        {
            InitializeComponent();
            this.Clear();
            this.Populate( follower, nameEn );
        }

        private void UserControl_Loaded( object sender, RoutedEventArgs e )
        {

        }

        public void Populate( Follower follower, string nameEn )
        {
            this.name.Text = follower.Name;
            if ( follower.Quolaty == 5 )
                this.name.Foreground = Brushes.OrangeRed;
            else if ( follower.Quolaty == 4 )
                this.name.Foreground = Brushes.BlueViolet;
            else if ( follower.Quolaty == 3 )
                this.name.Foreground = Brushes.DodgerBlue;
            else if ( follower.Quolaty == 2 )
                this.name.Foreground = Brushes.Lime;

            this.race.Text = follower.Race.ToString();
            this.classText.Text = Follower.GetCNStringByClass( follower.Class );
            this.race.Foreground = this.classText.Foreground = this.name.Foreground;

            this.level.Text = "(" + ( follower.Level == 100 ? follower.ItemLevel.ToString() : follower.Level.ToString() ) + ")";
            if ( follower.ItemLevel >= 645 )
                this.level.Foreground = Brushes.BlueViolet;
            else if ( follower.ItemLevel >= 630 )
                this.level.Foreground = Brushes.DodgerBlue;
            else
                this.level.Foreground = Brushes.Lime;

            if ( !follower.IsActive )
                this.actived.Visibility = System.Windows.Visibility.Visible;
            foreach ( Follower.Abilities ability in follower.AbilityCollection )
            {
                Image image = new Image();
                image.Source = Globals.AbilityImageSource[ ability ];// Follower.GetImageFromAbility( ability );
                this.abilityPanel.Children.Add( image );
            }
            foreach ( Follower.Traits trait in follower.TraitCollection )
            {
                Image image = new Image();
                image.Source = Globals.TraitImageSource[ trait ]; //Follower.GetImageFromFromTrait( trait );
                this.tratiPanel.Children.Add( image );
            }

            string bigPath = "Images/" + ( Globals.IsAlliance ? "Ali/" : "Hrd/" );
            bigPath += this.GetPicName( nameEn );
            if ( File.Exists( bigPath ) )
            {
                this.bigImage.PopulateImage( bigPath );
                this.bigImage.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private string GetPicName( string originalName )
        {
            return originalName.Contains( "Schweitzer" ) ? "Doc_Schweitzer.png" :
                originalName.Contains( "Steelpaw" ) ? "Suna_Sunnie_Steelpaw.png" :
                originalName.Contains( "the Fox" ) ? "Claire_the_Fox.png" :
                   originalName.Replace( ' ', '_' ) + ".png";
        }

        public void Clear()
        {
            this.name.Text = string.Empty;
            this.race.Text = string.Empty;
            this.level.Text = string.Empty;
            this.classText.Text = string.Empty;
            this.actived.Visibility = System.Windows.Visibility.Collapsed;
            foreach ( Image image in this.abilityPanel.Children )
                image.Source = null;
            this.abilityPanel.Children.Clear();
            foreach ( Image image in this.tratiPanel.Children )
                image.Source = null;
            this.tratiPanel.Children.Clear();
            this.bigImage.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
