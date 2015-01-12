using System;
using System.Collections.Generic;
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
            //this.Populate( follower, nameEn );
        }

        private void UserControl_Loaded( object sender, RoutedEventArgs e )
        {

        }

        public void Populate( Follower follower, string nameEn )
        {
            this.bigImage.Source = Follower.GetImageFromPicName( "ImagesBig/" + nameEn.Replace( '"', '#' ).Replace( ' ', '_' ) + ".png" );
            this.smallImage.Source = Follower.GetImageFromPicName( "ImagesSmall/" + nameEn.Replace( '"', '#' ).Replace( ' ', '_' ) + ".png" );
            this.name.Text = follower.Name;
            this.race.Text = follower.Race.ToString();
            this.level.Text = follower.Level.ToString();
            this.ilevel.Text = follower.ItemLevel.ToString();
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
            };
        }

        public void Clear()
        {
            this.bigImage.Source = null;
            this.smallImage.Source = null;
            this.name.Text = string.Empty;
            this.race.Text = string.Empty;
            this.level.Text = string.Empty;
            this.ilevel.Text = string.Empty;
            foreach ( Image image in this.abilityPanel.Children )
                image.Source = null;
            this.abilityPanel.Children.Clear();
            foreach ( Image image in this.tratiPanel.Children )
                image.Source = null;
            this.tratiPanel.Children.Clear();
        }
    }
}
