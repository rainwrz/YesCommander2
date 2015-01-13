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
            foreach ( Follower follower in Globals.CurrentFollowers )
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
                            FollowerHead head = new FollowerHead();
                            head.Margin = new Thickness( 4, 0, 0, 0 );
                            head.PopulateFullImage( Globals.GetFollowerEnNameById( follower.ID, Globals.IsAlliance ), follower );
                            this.collectedPanel.Children.Add( head );
                        }
                    }
                }
                else
                {
                    if ( ( follower.AbilityCollection[ 0 ] == model.Ability1 && Follower.GetAbilityFromClass( follower.Class ).Contains( model.Ability2 ) )
                        || follower.AbilityCollection[ 0 ] == model.Ability2 && Follower.GetAbilityFromClass( follower.Class ).Contains( model.Ability1 ) )
                    {
                        FollowerHead head = new FollowerHead();
                        head.Margin = new Thickness( 4, 0, 0, 0 );
                        head.PopulateFullImage( Globals.GetFollowerEnNameById( follower.ID, Globals.IsAlliance ), follower );
                        this.possiblePanel.Children.Add( head );
                    }
                }
            }
            this.numberText.Foreground = this.collectedPanel.Children.Count > 0 ? Brushes.Lime : Brushes.Red;
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
