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

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for followerFromDatabasexaml.xaml
    /// </summary>
    public partial class followerFromDatabasexaml : UserControl
    {
        public followerFromDatabasexaml( Follower follower, int ownedColor, bool isAli )
        {
            InitializeComponent();
            this.Clear();
            this.CN.Text = follower.NameCN;
            this.EN.Text = follower.NameEN;
            this.TranCN.Text = follower.NameTCN;
            if ( (int)follower.Race >= 0 && (int)follower.Race <= 12 )
            {
                this.raceImage.Visibility = System.Windows.Visibility.Visible;
                this.raceImage.Source = Globals.TraitImageSource[ Follower.GetTraitOfLoverByRace( follower.Race ) ];
                TextBlock toolTip = new TextBlock();
                toolTip.Text = follower.Race.ToString();
                this.raceImage.ToolTip = toolTip;
                ToolTipService.SetInitialShowDelay( this.raceImage, 0 );
            }
            else
            {
                this.race.Text = follower.Race.ToString();
            }
            this.level.Text = follower.Level.ToString();
            if ( ownedColor > 1 )
            {
                this.owned.Text = "有";
                switch ( ownedColor )
                {
                    case 4: this.owned.Foreground = Brushes.BlueViolet; break;
                    case 3: this.owned.Foreground = Brushes.DodgerBlue; break;
                    case 2: this.owned.Foreground = Brushes.Lime; break;
                }
            }

            if ( follower.AbilityCollection.Count > 0 )
                this.abilityImage.SetUp( follower.AbilityCollection[ 0 ] );
            if ( follower.TraitCollection.Count > 0 )
                this.traitImage.SetUp( follower.TraitCollection[ 0 ] );

            Brush color;
            switch ( follower.Quolaty )
            {
                case 4: color = Brushes.BlueViolet; break;
                case 3: color = Brushes.DodgerBlue; break;
                case 2:
                default:
                    color = Brushes.Lime; break;
            }
            this.CN.Foreground = this.EN.Foreground = this.TranCN.Foreground = this.race.Foreground = this.level.Foreground = color;
            //this.className.Text = Follower.GetCNStringByClass( follower.Class );
            this.specIcon.IconIndex = (int)follower.Class;
            this.followerHead.PopulateFullImage( follower.NameEN, isAli );
        }
        public void Clear()
        {
            this.CN.Text = string.Empty;
            this.EN.Text = string.Empty;
            this.TranCN.Text = string.Empty;
            this.race.Text = string.Empty;
            this.raceImage.Source = null;
            this.raceImage.Visibility = System.Windows.Visibility.Collapsed;
            this.level.Text = string.Empty;
            this.owned.Text = string.Empty;
            this.abilityImage.ClearUp();
            this.traitImage.ClearUp();
            this.followerHead.Clear();
        }
    }
}
