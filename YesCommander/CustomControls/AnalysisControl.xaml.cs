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
using YesCommander.CustomControls.Components;
using System.Data;

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for AnalysisControl.xaml
    /// </summary>
    public partial class AnalysisControl : UserControl
    {

        private List<Follower> allFollowers;
        private List<Follower> favorites;
        private List<Follower.Classes> currentClasses;

        public List<Follower> AliFollowers;
        public List<Follower> HrdFollowers;

        public AnalysisControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded( object sender, RoutedEventArgs e )
        {
            for ( int i = 0; i < this.abilityCheckBoxPanel.Children.Count; i++ )
            {
                AbilityCheckBox checkBox = this.abilityCheckBoxPanel.Children[ i ] as AbilityCheckBox;
                checkBox.CheckBoxClicked += new EventHandler( CheckBox_Checked );

                if ( i == 9 )
                    checkBox.image.SetUp( Follower.Abilities.TimedBattle );
                else
                    checkBox.image.SetUp( (Follower.Abilities)i );
            }


            //List<string> itemSource = new List<string>();
            //for ( int i = 0; i <= (int)Follower.Classes.ProtectionWarrior; i++ )
            //{
            //    itemSource.Add( Follower.GetCNStringByClass( (Follower.Classes)i ) );
            //}

            List<ImageComboBoxItem> itemSource = new List<ImageComboBoxItem>();
            for ( int i = 0; i <= (int)Follower.Classes.ProtectionWarrior; i++ )
            {
                ImageComboBoxItem item = new ImageComboBoxItem()
                {
                    Name = Follower.GetCNStringByClass( (Follower.Classes)i ),
                    Id = string.Empty,
                    Image = Globals.specIcionList[ i ],
                    DataRowValue = null
                };
                itemSource.Add( item );

                this.specIconPanel.Children.Add( new SpecIcon( i ) );
            }
            this.classComboBox.DataSource = itemSource;

            // radiobutton
            string raceString = string.Empty;
            for ( int i = 0; i < this.raceRadioPanel.Children.Count / 2; i++ )
            {
                raceString = ( this.raceRadioPanel.Children[ i * 2 ] as RadioButton ).Name.ToString();
                if ( raceString != "其他" )
                {
                    ( this.raceRadioPanel.Children[ i * 2 + 1 ] as Image ).Source = Globals.TraitImageSource[ Follower.GetTraitOfLoverByRace( Follower.GetRaceByName( raceString ) ) ];
                    TextBlock toolTip = new TextBlock();
                    toolTip.Text = raceString;
                    ( this.raceRadioPanel.Children[ i * 2 + 1 ] as Image ).ToolTip = toolTip;
                    ToolTipService.SetInitialShowDelay( ( this.raceRadioPanel.Children[ i * 2 + 1 ] as Image ), 0 );
                }
            }
        }

        public void AddTraitCheckBoxes()
        {
            this.traitCheckBoxPanel.Children.Clear();
            //Add trait checkboxes into the trait panel
            int bot = Globals.IsAlliance ? 13 : 20;
            int top = Globals.IsAlliance ? 18 : 25;
            for ( int i = bot; i <= top; i++ )
                this.AddCheckboxToTraitPanel( i );

            this.AddCheckboxToTraitPanel( 19 );
            for ( int i = 1; i <= 2; i++ )
                this.AddCheckboxToTraitPanel( i );
            this.AddCheckboxToTraitPanel( 37 );
            this.AddCheckboxToTraitPanel( 38 );
            this.AddCheckboxToTraitPanel( 39 );
            this.AddCheckboxToTraitPanel( 41 );
            this.AddCheckboxToTraitPanel( 40 );
            this.AddCheckboxToTraitPanel( 55 );
        }

        public void CheckBox_Checked( object sender, EventArgs e )
        {
            List<Follower.Abilities> abilities = this.GetCheckedAbilites();
            this.EnableOrDisableCheckBoxes( abilities );
            this.RespondeCheckBoxChange( abilities );
        }

        public void TraitCheckBox_Checked( object sender, EventArgs e )
        {
            List<Follower.Traits> list = this.GetCheckedTrait();
            this.EnableOrDisbaleCheckBoxes( list );
            this.RespondeCheckBoxChange( list );
        }

        private void titleBlock_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( sender is TextBlock && ( sender as TextBlock ).FontSize == 22 )
            {
                return;
            }

            this.titleAli.FontSize = 18;
            this.titleHrd.FontSize = 18;
            this.titleMy.FontSize = 18;
            this.aliScroll.Visibility = System.Windows.Visibility.Hidden;
            this.hrdScroll.Visibility = System.Windows.Visibility.Hidden;
            this.myScroll.Visibility = System.Windows.Visibility.Hidden;

            ( sender as TextBlock ).FontSize = 22;
            switch ( ( sender as TextBlock ).Name )
            {
                case "titleAli":
                    this.aliScroll.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "titleHrd":
                    this.hrdScroll.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "titleMy":
                    this.myScroll.Visibility = System.Windows.Visibility.Visible;
                    break;
                default: break;
            }
            if ( this.abilityAnalysis.FontSize == 18 )
            {
                this.CheckBox_Checked( null, null );
            }
            else if ( this.traitAnalysis.FontSize == 18 )
            {
            }
            else if ( this.raceAnalysis.FontSize == 18 )
            {
                this.RunRaceFilter();
            }
        }

        private void headTitleBlock_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( sender is TextBlock && ( sender as TextBlock ).FontSize == 18 )
            {
                return;
            }

            this.abilityAnalysis.FontSize = 15;
            this.traitAnalysis.FontSize = 15;
            this.raceAnalysis.FontSize = 15;
            this.classAnalysis.FontSize = 15;
            this.abilityCheckBoxPanel.Visibility = System.Windows.Visibility.Hidden;
            this.traitCheckBoxPanel.Visibility = System.Windows.Visibility.Hidden;
            this.raceRadioPanel.Visibility = System.Windows.Visibility.Hidden;
            this.classPanel.Visibility = System.Windows.Visibility.Hidden;
            this.classString.Visibility = System.Windows.Visibility.Collapsed;
            this.specIconPanel.Visibility = System.Windows.Visibility.Collapsed;

            ( sender as TextBlock ).FontSize = 18;
            switch ( ( sender as TextBlock ).Name )
            {
                case "abilityAnalysis":
                    {
                        this.abilityCheckBoxPanel.Visibility = System.Windows.Visibility.Visible;
                        this.specIconPanel.Visibility = System.Windows.Visibility.Visible;
                        this.CheckBox_Checked( null, null );
                    }
                    break;
                case "traitAnalysis":
                    {
                        this.traitCheckBoxPanel.Visibility = System.Windows.Visibility.Visible;
                        this.classString.Visibility = System.Windows.Visibility.Visible;
                        this.classString.Text = "选择最多三个特长，查看拥有这些特长的随从。";
                        this.TraitCheckBox_Checked( null, null );
                    }
                    break;
                case "raceAnalysis":
                    {
                        this.raceRadioPanel.Visibility = System.Windows.Visibility.Visible;
                        this.RunRaceFilter();
                    }
                    break;
                case "classAnalysis":
                    {
                        this.classPanel.Visibility = System.Windows.Visibility.Visible;
                        this.classString.Text = string.Empty;
                        this.ComboBox_SelectionChanged( null, null );
                    }
                    break;
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

        private void titleBlock_MouseLeave( object sender, MouseEventArgs e )
        {
            if ( sender is TextBlock )
            {
                ( sender as TextBlock ).Foreground = ( sender as TextBlock ).Name == "titleAli" ? Brushes.DodgerBlue :
                    ( sender as TextBlock ).Name == "titleHrd" ? Brushes.Red :
                    ( sender as TextBlock ).Name == "titleMy" ? Brushes.BlueViolet:
                    (Brush)new BrushConverter().ConvertFromString( "#ffe8ce" );
            }
            this.Cursor = Cursors.Arrow;
        }

        private void RadioButton_Checked( object sender, RoutedEventArgs e )
        {
            this.RunRaceFilter();
        }

        private void ComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            Follower.Classes currentClass = Follower.GetClassBySingleStr( ( this.classComboBox.SelectedItem as ImageComboBoxItem ).Name );
            int followerColor = 0;
            this.aliPanel.Children.Clear();
            foreach ( Follower follower in this.AliFollowers.FindAll( x => x.Class == currentClass ).OrderByDescending( x => x.Quolaty ) )
            {
                followerColor = 0;
                if ( Globals.IsAlliance && this.allFollowers.Exists( x => x.ID == follower.ID ) )
                    followerColor = this.allFollowers.First( x => x.ID == follower.ID ).Quolaty;
                this.aliPanel.Children.Add( new followerFromDatabasexaml( follower, followerColor, true ) );
            }

            this.hrdPanel.Children.Clear();
            foreach ( Follower follower in this.HrdFollowers.FindAll( x => x.Class == currentClass ).OrderByDescending( x => x.Quolaty ) )
            {
                followerColor = 0;
                if ( !Globals.IsAlliance && this.allFollowers.Exists( x => x.ID == follower.ID ) )
                    followerColor = this.allFollowers.First( x => x.ID == follower.ID ).Quolaty;
                this.hrdPanel.Children.Add( new followerFromDatabasexaml( follower, followerColor, false ) );
            }

            this.myPanel.Children.Clear();
            foreach ( Follower follower in this.allFollowers.FindAll( x => x.Class == currentClass ).OrderByDescending( x => x.Quolaty ) )
            {
                this.AddFollowerForMyScroll( follower, this.favorites.Contains( follower ) );
            }

            foreach ( TinyImage image in this.abilityPanel.Children )
            {
                image.ClearUp();
            }
            for ( int i = 0; i < 5; i++ )
            {
                ( this.abilityPanel.Children[ i ] as TinyImage ).SetUp( Follower.GetAbilityFromClass( currentClass )[ i ] );
            }
        }

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="all"></param>
        /// <param name="favorite"></param>
        public void AssignFollowers( ref List<Follower> all, ref List<Follower> favorite )
        {
            this.allFollowers = all;
            this.favorites = favorite;
            this.LoadFile();
            this.classComboBox.SelectedIndex = 0;
        }

        #region Abilities
        private void AddFollowerForMyScroll( Follower follower, bool isFaverite )
        {
            this.myPanel.Children.Add( new FollowerRow( follower, this.GetNameEnById( follower.ID ), isFaverite ) );
        }

        private List<Follower.Abilities> GetCheckedAbilites()
        {
            List<Follower.Abilities> abilities = new List<Follower.Abilities>();
            for ( int i = 0; i < this.abilityCheckBoxPanel.Children.Count; i++ )
            {
                AbilityCheckBox checkBox = this.abilityCheckBoxPanel.Children[ i ] as AbilityCheckBox;
                if ( checkBox.IsChecked )
                {
                    if ( i == 9 )
                        abilities.Add( Follower.Abilities.TimedBattle );
                    else
                        abilities.Add( (Follower.Abilities)i );
                }
            }
            return abilities;
        }

        private List<Follower.Traits> GetCheckedTrait()
        {
            List<Follower.Traits> list = new List<Follower.Traits>();
            foreach ( AbilityCheckBox checkBox in this.traitCheckBoxPanel.Children )
            {
                if ( checkBox.IsChecked == true )
                    list.Add( Follower.GetTratByString( checkBox.Name ) );
            }
            return list;
        }

        private void EnableOrDisableCheckBoxes( List<Follower.Abilities> abilities )
        {
            if ( abilities.Count == 2 )
            {
                foreach ( AbilityCheckBox checkBox in this.abilityCheckBoxPanel.Children )
                {
                    if ( !checkBox.IsChecked )
                        checkBox.IsDisPlayed = false;
                }
            }
            else
            {
                foreach ( AbilityCheckBox checkBox in this.abilityCheckBoxPanel.Children )
                    checkBox.IsDisPlayed = true;
            }

            this.currentClasses = new List<Follower.Classes>();
            if ( abilities.Count > 0 )
            {
                string classString = string.Empty;
                for ( int j = 0; j < 34; j++ )
                {
                    List<Follower.Abilities> classAbilities = Follower.GetAbilityFromClass( (Follower.Classes)j );
                    bool isContain = true;
                    foreach ( Follower.Abilities ability in abilities )
                    {
                        if ( !classAbilities.Contains( ability ) )
                        {
                            isContain = false;
                            break;
                        }
                        else
                            classAbilities.Remove( ability );
                    }

                    if ( isContain )
                    {
                        this.currentClasses.Add( (Follower.Classes)j );
                        ( this.specIconPanel.Children[ j ] as SpecIcon ).IsDisplayed = true;
                    }
                    else
                        ( this.specIconPanel.Children[ j ] as SpecIcon ).IsDisplayed = false;
                }
            }
            else
                foreach ( SpecIcon icon in this.specIconPanel.Children )
                    icon.IsDisplayed = false;
        }

        private void EnableOrDisbaleCheckBoxes( List<Follower.Traits> trait )
        {
            if ( trait.Count == 3 )
            {
                foreach ( AbilityCheckBox checkBox in this.traitCheckBoxPanel.Children )
                {
                    if ( checkBox.IsChecked == false )
                    {
                        checkBox.IsDisPlayed = false;
                    }
                }
            }
            else
            {
                foreach ( AbilityCheckBox checkBox in this.traitCheckBoxPanel.Children )
                {
                    checkBox.IsDisPlayed = true;
                }
            }
        }

        private void RespondeCheckBoxChange( List<Follower.Abilities> abilities )
        {
            this.myPanel.Children.Clear();
            this.aliPanel.Children.Clear();
            this.hrdPanel.Children.Clear();
            if ( abilities.Count == 0 )
                return;

            if ( this.titleAli.FontSize == 22 )
            {
                this.AddFollowersToPanelForOwend( this.AliFollowers, this.aliPanel, abilities, true );
                this.AddFollowersToPanelForHasAbility( this.AliFollowers, this.aliPanel, abilities, true );
                this.AddFollowersToPanel( this.AliFollowers, this.aliPanel, abilities, true );
            }
            else if ( this.titleHrd.FontSize == 22 )
            {
                this.AddFollowersToPanelForOwend( this.HrdFollowers, this.hrdPanel, abilities, false );
                this.AddFollowersToPanelForHasAbility( this.HrdFollowers, this.hrdPanel, abilities, false );
                this.AddFollowersToPanel( this.HrdFollowers, this.hrdPanel, abilities, false );
            }
            else if ( this.titleMy.FontSize == 22 )
            {
                this.AddFollowersToScrollByQuolaty( abilities, 4 );
                this.AddFollowersToScrollByQuolaty( abilities, 3 );
                this.AddFollowersToScrollByQuolaty( abilities, 2 );
            }
        }

        private void RespondeCheckBoxChange( List<Follower.Traits> trait )
        {
            this.myPanel.Children.Clear();
            this.aliPanel.Children.Clear();
            this.hrdPanel.Children.Clear();
            if ( trait.Count == 0 )
                return;

            this.AddFollowersToScrollByQuolaty( trait );
            
            if ( trait.Count == 1 && trait[ 0 ] == Follower.Traits.Bodyguard )
            {
                this.AddFollowersToPanelForOwend( this.AliFollowers, this.aliPanel, trait, true );
                this.AddFollowersToPanel( this.AliFollowers, this.aliPanel, trait, true );
                this.AddFollowersToPanelForOwend( this.HrdFollowers, this.hrdPanel, trait, false );
                this.AddFollowersToPanel( this.HrdFollowers, this.hrdPanel, trait, false );
            }

            if ( trait.Count >= 1 && trait[ 0 ] == Follower.Traits.TreasureHunter )
            {
                this.AddFollowersToPanelForOwend( this.AliFollowers, this.aliPanel, trait, true );
                this.AddFollowersToPanel( this.AliFollowers, this.aliPanel, trait, true );
                this.AddFollowersToPanelForOwend( this.HrdFollowers, this.hrdPanel, trait, false );
                this.AddFollowersToPanel( this.HrdFollowers, this.hrdPanel, trait, false );
            }
        }

        private void AddFollowersToScrollByQuolaty( List<Follower.Abilities> abilities, int quolaty )
        {
            if ( abilities.Count == 1 )
            {
                foreach ( Follower follower in this.allFollowers )
                {
                    if ( follower.Quolaty != quolaty )
                        continue;

                    if ( follower.AbilityCollection.Contains( abilities[ 0 ] ) )
                        this.AddFollowerForMyScroll( follower, this.favorites.Contains( follower ) );
                }
            }
            else
            {
                foreach ( Follower follower in this.allFollowers )
                {
                    if ( follower.Quolaty != quolaty )
                        continue;
                    List<Follower.Abilities> abilityCollection = new List<Follower.Abilities>();
                    abilityCollection.AddRange( follower.AbilityCollection );
                    if ( abilityCollection.Contains( abilities[ 0 ] ) )
                    {
                        abilityCollection.Remove( abilities[ 0 ] );
                        if ( abilityCollection.Contains( abilities[ 1 ] ) )
                            this.AddFollowerForMyScroll( follower, this.favorites.Contains( follower ) );
                    }
                }
                // add possible followers
                if ( quolaty != 4 )
                {
                    foreach ( Follower follower in this.allFollowers )
                    {
                        if ( follower.Quolaty != quolaty )
                            continue;
                        if ( follower.AbilityCollection.Contains( abilities[ 0 ] ) || follower.AbilityCollection.Contains( abilities[ 1 ] ) )
                        {
                            Follower.Abilities theOtherAbility = follower.AbilityCollection.Contains( abilities[ 0 ] ) ? abilities[ 1 ] : abilities[ 0 ];
                            List<Follower.Abilities> possibleAbilites = Follower.GetAbilityFromClass( follower.Class );
                            if ( possibleAbilites.Contains( follower.AbilityCollection[ 0 ] ) )
                                possibleAbilites.Remove( follower.AbilityCollection[ 0 ] );
                            if ( possibleAbilites.Contains( theOtherAbility ) )
                                this.AddFollowerForMyScroll( follower, this.favorites.Contains( follower ) );
                        }
                    }
                }
            }
        }

        private void AddFollowersToScrollByQuolaty( List<Follower.Traits> trait )
        {
            var data = from temp in this.allFollowers
                       where ( temp.TraitCollection.Contains( trait[ 0 ] ) && trait.Count == 1 )
                       || ( trait.Count == 2 && temp.TraitCollection.Contains( trait[ 0 ] ) && temp.TraitCollection.Contains( trait[ 1 ] ) )
                       || ( trait.Count == 3 && temp.TraitCollection.Contains( trait[ 0 ] ) && temp.TraitCollection.Contains( trait[ 1 ] ) && temp.TraitCollection.Contains( trait[ 2 ] ) )
                       orderby temp.Quolaty descending
                       select temp;
            foreach ( Follower follower in data )
            {
                this.AddFollowerForMyScroll( follower, this.favorites.Contains( follower ) );
            }
        }
        /// <summary>
        /// For those who have ability that does not belong its class
        /// </summary>
        /// <param name="follower"></param>
        /// <param name="ability1"></param>
        /// <param name="ability2"></param>
        /// <returns></returns>
        private bool IsContainAbility( Follower follower, Follower.Abilities ability1, Follower.Abilities ability2 )
        {
            List<Follower.Abilities> list = Follower.GetAbilityFromClass( follower.Class );
            if ( !follower.AbilityCollection.Contains( ability1 ) )
                return false;
            else
            {
                if ( ability2 == Follower.Abilities.Error )
                    return true;
                else
                    return list.Contains( ability2 );
            }
        }

        private void AddFollowersToPanelForOwend( List<Follower> list, StackPanel panel, List<Follower.Abilities> abilities, bool isAli )
        {
            var data = from temp in list
                       where ( this.currentClasses.Contains( temp.Class ) || ( temp.AbilityCollection.Count > 0 && this.IsContainAbility( temp, abilities[ 0 ], ( abilities.Count > 1 ) ? abilities[ 1 ] : Follower.Abilities.Error ) ) )
                       && this.allFollowers.Exists( x => x.ID == temp.ID )
                       orderby temp.Quolaty descending
                       select temp;

            foreach ( Follower follower in data )
            {
                panel.Children.Add( new followerFromDatabasexaml( follower, follower.Quolaty, isAli ) );
            }
        }

        private void AddFollowersToPanelForOwend( List<Follower> list, StackPanel panel, List<Follower.Traits> trait, bool isAli )
        {
            var data = from temp in list
                       where temp.TraitCollection.Count > 0 && temp.TraitCollection.Contains( trait[ 0 ] )
                       && this.allFollowers.Exists( x => x.ID == temp.ID )
                       orderby temp.Quolaty descending
                       select temp;

            foreach ( Follower follower in data )
            {
                panel.Children.Add( new followerFromDatabasexaml( follower, follower.Quolaty, isAli ) );
            }
        }

        private void AddFollowersToPanel( List<Follower> list, StackPanel panel, List<Follower.Abilities> abilities, bool isAli )
        {
            var data = from temp in list
                       where ( temp.AbilityCollection.Count == 0 || !abilities.Contains( temp.AbilityCollection[ 0 ] ) ) &&
                       ( this.currentClasses.Contains( temp.Class ) || ( temp.AbilityCollection.Count > 0 && this.IsContainAbility( temp, abilities[ 0 ], ( abilities.Count > 1 ) ? abilities[ 1 ] : Follower.Abilities.Error ) ) )
                       && !this.allFollowers.Exists( x => x.ID == temp.ID )
                       orderby temp.Quolaty descending
                       select temp;

            foreach ( Follower follower in data )
            {
                panel.Children.Add( new followerFromDatabasexaml( follower, 0, isAli ) );
            }
        }

        private void AddFollowersToPanel( List<Follower> list, StackPanel panel, List<Follower.Traits> trait, bool isAli )
        {
            var data = from temp in list
                       where temp.TraitCollection.Count > 0 && temp.TraitCollection.Contains( trait[ 0 ] )
                       && !this.allFollowers.Exists( x => x.ID == temp.ID )
                       orderby temp.Quolaty descending
                       select temp;

            foreach ( Follower follower in data )
            {
                panel.Children.Add( new followerFromDatabasexaml( follower, 0, isAli ) );
            }
        }

        private void AddFollowersToPanelForHasAbility( List<Follower> list, StackPanel panel, List<Follower.Abilities> abilities, bool isAli )
        {
            var data = from temp in list
                       where temp.AbilityCollection.Count > 0 && abilities.Contains( temp.AbilityCollection[ 0 ] )
                       && ( this.currentClasses.Contains( temp.Class ) || ( temp.AbilityCollection.Count > 0 && this.IsContainAbility( temp, abilities[ 0 ], ( abilities.Count > 1 ) ? abilities[ 1 ] : Follower.Abilities.Error ) ) )
                       && !this.allFollowers.Exists( x => x.ID == temp.ID )
                       orderby temp.Quolaty descending
                       select temp;

            foreach ( Follower follower in data )
            {
                panel.Children.Add( new followerFromDatabasexaml( follower, 0, isAli ) );
            }
        }
        #endregion //Abilities
        
        #region Traits
        private void AddCheckboxToTraitPanel( int i )
        {
            Follower.Traits trait = (Follower.Traits)i;
            AbilityCheckBox checkBox = new AbilityCheckBox();
            checkBox.Name = trait.ToString();
            checkBox.CheckBoxClicked += new EventHandler( TraitCheckBox_Checked );
            checkBox.image.SetUp( trait );
            this.traitCheckBoxPanel.Children.Add( checkBox ); 
        }

        #endregion //Traits

        #region Races
        public void RunRaceFilter()
        {
            string raceString = string.Empty;
            for ( int i = 0; i < this.raceRadioPanel.Children.Count/2; i++ )
            {
                if ( ( this.raceRadioPanel.Children[ i*2 ] as RadioButton ).IsChecked == true )
                {
                    raceString = ( this.raceRadioPanel.Children[ i*2 ] as RadioButton ).Name.ToString();
                    break;
                }
            }
            if ( raceString == string.Empty )
                return;

            this.AddFollowersByRace( Follower.GetRaceByName( raceString ) );
        }

        private void AddFollowersByRace( Follower.Races followerRace )
        {
            if ( this.titleMy.FontSize == 22 )
            {
                this.myPanel.Children.Clear();

                var data = from temp in this.allFollowers
                           where this.MatchRace( temp, followerRace )
                           orderby temp.Quolaty descending
                           select temp;

                foreach ( Follower follower in data )
                {
                    this.myPanel.Children.Add( new FollowerRow( follower, this.GetNameEnById( follower.ID ), this.favorites.Contains( follower ) ) );
                }
            }
            else if ( this.titleAli.FontSize == 22 )
            {
                this.AddFollowerIntoPanel( this.AliFollowers, this.aliPanel, followerRace, true );
            }
            else if ( this.titleHrd.FontSize == 22 )
            {
                this.AddFollowerIntoPanel( this.HrdFollowers, this.hrdPanel, followerRace, false );
            }
        }

        private void AddFollowerIntoPanel( List<Follower> list, StackPanel panel, Follower.Races race, bool isAli )
        {
            panel.Children.Clear();
            var data = from temp in list
                       where this.MatchRace( temp, race )
                       && this.allFollowers.Exists( x => x.ID == temp.ID )
                       orderby temp.Quolaty descending
                       select temp;
            foreach ( Follower follower in data )
                panel.Children.Add( new followerFromDatabasexaml( follower, follower.Quolaty, isAli ) );

            data = from temp in list
                   where this.MatchRace( temp, race )
                   && !this.allFollowers.Exists( x => x.ID == temp.ID )
                   orderby temp.Quolaty descending
                   select temp;
            foreach ( Follower follower in data )
                panel.Children.Add( new followerFromDatabasexaml( follower, 0, isAli ) );
        }

        private bool MatchRace( Follower follower, Follower.Races race )
        {
            if ( Convert.ToInt16( race ) > 12 )
            {
                if ( follower.Race == Follower.Races.error )
                {
                    return false;
                }
                else
                    return Convert.ToInt16( follower.Race ) > 12;
            }
            else
                return follower.Race == race;
        }
        #endregion //Races

        private string GetNameEnById( string id )
        {
            if ( Globals.IsAlliance )
                return this.AliFollowers.FirstOrDefault( x => x.ID == id ).NameEN;
            else
                return this.HrdFollowers.FirstOrDefault( x => x.ID == id ).NameEN;
        }

        public string GetNameEnByNameString( string name )
        {
            Follower f = this.AliFollowers.FirstOrDefault( x => ( x.NameCN == name || x.NameEN == name || x.NameTCN == name ) );
            if ( f == null )
            {
                f = this.HrdFollowers.FirstOrDefault( x => ( x.NameCN == name || x.NameEN == name || x.NameTCN == name ) );
            }
            return f.NameEN;
        }

        public void LoadFile()
        {
            DataRow currentRow;
            List<int> abilityList = new List<int>();
            List<int> traitList = new List<int>();
            // Ali
            this.AliFollowers = new List<Follower>();
            foreach ( DataRow row in Globals.AllFollowersAli.Rows )
            {
                int quolaty;
                switch ( row[ "初始品质" ].ToString() )
                {
                    case "传奇": quolaty = 5; break;
                    case "史诗": quolaty = 4; break;
                    case "精良": quolaty = 3; break;
                    case "优秀": quolaty = 2; break;
                    default: quolaty = 2; break;
                }
                abilityList = new List<int>();
                currentRow = Globals.AliFollowerSkills.Rows.OfType<DataRow>().First( x => x[ "ID" ].ToString() == row[ "ID" ].ToString() );
                if ( !string.IsNullOrEmpty( currentRow[ "应对ID" ].ToString() ) )
                    abilityList.Add( Convert.ToInt16( currentRow[ "应对ID" ] ) );
                traitList = new List<int>();
                if ( !string.IsNullOrEmpty( currentRow[ "特长ID" ].ToString() ) )
                    traitList.Add( Convert.ToInt16( currentRow[ "特长ID" ] ) );
                if ( !string.IsNullOrEmpty( currentRow[ "特长ID2" ].ToString() ) )
                    traitList.Add( Convert.ToInt16( currentRow[ "特长ID2" ] ) );

                this.AliFollowers.Add( new Follower( row[ "ID" ].ToString(), row[ "英文名字" ].ToString(), quolaty, Convert.ToInt16( row[ "初始等级" ] ), 600, row[ "种族" ].ToString(),
                    Follower.GetClassByStr( row[ "职业" ].ToString(), row[ "专精" ].ToString() ), string.Empty, 1, abilityList, traitList,
                    row[ "英文名字" ].ToString(), row[ "简体名字" ].ToString(), row[ "繁体名字" ].ToString() ) );
            }
            //Hrd
            this.HrdFollowers = new List<Follower>();
            foreach ( DataRow row in Globals.AllFollowersHrd.Rows )
            {
                int quolaty;
                switch ( row[ "初始品质" ].ToString() )
                {
                    case "传奇": quolaty = 5; break;
                    case "史诗": quolaty = 4; break;
                    case "精良": quolaty = 3; break;
                    case "优秀": quolaty = 2; break;
                    default: quolaty = 2; break;
                }
                abilityList = new List<int>();
                currentRow = Globals.HrdFollowerSkills.Rows.OfType<DataRow>().First( x => x[ "ID" ].ToString() == row[ "ID" ].ToString() );
                if ( !string.IsNullOrEmpty( currentRow[ "应对ID" ].ToString() ) )
                    abilityList.Add( Convert.ToInt16( currentRow[ "应对ID" ] ) );
                traitList = new List<int>();
                if ( !string.IsNullOrEmpty( currentRow[ "特长ID" ].ToString() ) )
                    traitList.Add( Convert.ToInt16( currentRow[ "特长ID" ] ) );

                this.HrdFollowers.Add( new Follower( row[ "ID" ].ToString(), row[ "英文名字" ].ToString(), quolaty, Convert.ToInt16( row[ "初始等级" ] ), 600, row[ "种族" ].ToString(),
                    Follower.GetClassByStr( row[ "职业" ].ToString(), row[ "专精" ].ToString() ), string.Empty, 1, abilityList, traitList,
                    row[ "英文名字" ].ToString(), row[ "简体名字" ].ToString(), row[ "繁体名字" ].ToString() ) );
            }
        }
        #endregion //Methods
    }
}
