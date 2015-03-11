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
    /// Interaction logic for combinationRow.xaml
    /// </summary>
    public partial class combinationRow : UserControl
    {
        private AbilityCombinationModel model;
        public combinationRow()
        {
            InitializeComponent();
        }

        public void AssignCombinationModel(int number, AbilityCombinationModel model )
        {
            this.numberText.Text = number.ToString();

            this.ability1.SetUp( model.Ability1 );
            this.ability2.SetUp( model.Ability2 );
            this.numberOfClass.Text = model.ListOfClasses.Count.ToString();
            this.numberOfFollowersAli.Text = model.NumberOfFollowersForAli.ToString();
            this.numberOfFollowersHrd.Text = model.NumberOfFollowersForHrd.ToString();
            this.model = model;
        }

        public void AssignFollowers()
        {
            this.Clear();
            foreach ( Follower follower in Globals.CurrentValidFollowers )
            {
                if ( follower.Quolaty == 4 )
                {
                    List<Follower.Abilities> abilities = new List<Follower.Abilities>();
                    abilities.AddRange( follower.AbilityCollection );
                    if ( abilities.Contains( model.Ability1 ) )
                    {
                        abilities.Remove( model.Ability1 );
                        if ( abilities.Contains( model.Ability2 ) )
                        {
                            this.PlaceFollower( this.collectedPanel, follower );
                        }
                    }
                }
                else
                {
                    if ( follower.AbilityCollection[ 0 ] == model.Ability1 || follower.AbilityCollection[ 0 ] == model.Ability2)
                    {
                        List<Follower.Abilities> abilities = Follower.GetAbilityFromClass( follower.Class );

                        abilities.Remove( follower.AbilityCollection[ 0 ] );
                        if ( ( follower.AbilityCollection[ 0 ] == model.Ability1 && abilities.Contains( model.Ability2 ) ) ||
                            ( follower.AbilityCollection[ 0 ] == model.Ability2 && abilities.Contains( model.Ability1 ) ) )
                            this.PlaceFollower( this.possiblePanel, follower );
                    }
                }
            }
            this.numberText.Foreground = this.collectedPanel.Children.Count > 0 ? Brushes.Lime : Brushes.Red;
        }

        private void PlaceFollower( StackPanel panel, Follower follower )
        {
            FollowerHead head = new FollowerHead();
            head.Margin = new Thickness( 4, 0, 0, 0 );
            head.PopulateFullImage( Globals.GetFollowerEnNameById( follower.ID, Globals.IsAlliance ), follower );
            panel.Children.Add( head );
        }

        public void Clear()
        {
            foreach ( FollowerHead head in this.collectedPanel.Children )
                head.Clear();
            foreach ( FollowerHead head in this.possiblePanel.Children )
                head.Clear();
            this.collectedPanel.Children.Clear();
            this.possiblePanel.Children.Clear();
        }
    }
}
