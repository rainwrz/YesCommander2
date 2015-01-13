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
using YesCommander.CustomControls.Components;

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for AbilityCombination.xaml
    /// </summary>
    public partial class AbilityCombination : UserControl
    {
        public AbilityCombination()
        {
            InitializeComponent();
        }

        private void headTitleBlock_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( sender is TextBlock && ( sender as TextBlock ).FontSize == 18 )
            {
                return;
            }

            this.existingFollower.FontSize = 15;
            //this.possibleFollower.FontSize = 15;

            ( sender as TextBlock ).FontSize = 18;
            switch ( ( sender as TextBlock ).Name )
            {
                case "existingFollower":
                    {
                    }
                    break;
                case "possibleFollower":
                    {
                    }
                    break;
                //case "raceAnalysis":
                //    {
                //    }
                //    break;
                //case "classAnalysis":
                //    {
                //    }
                //    break;
                default: break;
            }
        }

        private void titleBlock_MouseEnter( object sender, MouseEventArgs e )
        {
            if ( sender is TextBlock && ( sender as TextBlock ).FontSize != 22 )
            {
                ( sender as TextBlock ).Foreground = Brushes.White;
                this.Cursor = Cursors.Hand;
            }
        }


        private void titleBlock_MouseEnterOnImage( object sender, MouseEventArgs e )
        {
            this.Cursor = Cursors.Help;
        }

        private void titleBlock_MouseLeave( object sender, MouseEventArgs e )
        {
            if ( sender is TextBlock )
            {
                ( sender as TextBlock ).Foreground =
                    (Brush)new BrushConverter().ConvertFromString( "#ffe8ce" );
            }
            this.Cursor = Cursors.Arrow;
        }

        private void UserControl_Loaded( object sender, RoutedEventArgs e )
        {
            int i=1;
            foreach ( AbilityCombinationModel model in Globals.combinationModelList )
            {
                combinationRow row = new combinationRow();
                row.AssignCombinationModel( i, model );
                this.combinationPanel.Children.Add( row );
                i++;
            }
            this.missionWindowImage.ToolTip = new BaseToolTip( "注意", "此功能仅用于检阅和满足收藏爱好，并不代表推荐任何“N金刚”玩法。随从玩法花样繁多，喜欢的就是最好的。" );
            ToolTipService.SetInitialShowDelay( this.missionWindowImage, 0 );
        }

        public void PlaceFollowers()
        {
            int collected = 0;
            foreach ( combinationRow row in this.combinationPanel.Children )
            {
                row.AssignFollowers();
                if ( row.numberText.Foreground == Brushes.Lime )
                    collected++;
            }
            this.rate.Text = collected.ToString() + "/" + this.combinationPanel.Children.Count;
        }



        #region METHODS

        #endregion //METHODS
    }
}
