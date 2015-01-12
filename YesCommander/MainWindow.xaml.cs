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
using Elysium.Controls;
using Elysium.Parameters;
using Elysium;
using YesCommander.Model;
using YesCommander.Classes;
using System.Data;
using YesCommander.CustomControls;
using System.ComponentModel;
using System.Reflection;
using YesCommander.CustomControls.Components;

namespace YesCommander
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Controls.Window
    {
        public Missions Missions;
        private Mission CurrentMission;
        public DataTable TableFollowers;
        private List<Follower> currentFollowers;
        private List<Follower> favoriteFollowers;
        private Dictionary<int, Mission> currentMissions;
        private MissionGrid missionGrid;
        private MissionGrid followerGrid;
        private bool isCheckboxTriggeredByself = false;
        private int maxNumberOfTeam = 20;
        private bool isNeedToReShow = false;
        private bool isNeedReCalculateMission = false;

        public MainWindow()
        {
            InitializeComponent();
            Globals.Initialize();

            this.maxNumberOfPartyComboBox.ItemsSource = new List<string>() { "最多显示1只队伍", "最多显示10只队伍", "最多显示20只队伍", "最多显示50只队伍", "最多显示100只队伍(降低性能)" };
            this.maxNumberOfPartyComboBox.SelectedIndex = 2;
            this.Missions = new Missions();
            //this.FillInMissions( this.Missions.HighmaulMissions );
            this.missionsComboBox.ComboBoxImageList = Globals.missionIcionList;
            this.currentMissions = this.Missions.HighmaulMissions;
            this.FillInMissions( this.Missions.HighmaulMissionRows );
            this.radioFollowers.IsChecked = true;
            this.favoriteFollowers = new List<Follower>();
        }

        #region Events
        private void Loaded_1( object sender, RoutedEventArgs e )
        {
            this.missionWindowImage.ToolTip = new BaseToolTip( "任务列表", "打开一个新的窗口，查看所有任务详情。" );
            ToolTipService.SetInitialShowDelay( this.missionWindowImage, 0 );
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            this.about.ToolTip = new BaseToolTip( "YesCommander", "作者：梧桐哟梧桐，当前版本："
                + version.Major.ToString() + "." + version.Minor.ToString() + ( version.Build == 0 ? string.Empty : "." + version.Build.ToString() )
                + "\r\n\r\n注意：此软件仅发布在NGA。下载地址仅限百度网盘，其他途径下载均有风险。"
                + " \r\n\r\n熊猫酒仙联盟公会【月 神 之 怒】招募有识之士。共同迎战德拉诺之王。"
                + " \r\n\r\n广告位招租。" );
            ToolTipService.SetShowDuration( this.about, 60000 );
            ToolTipService.SetInitialShowDelay( this.about, 0 );
        }

        private void RadioButton_Checked( object sender, RoutedEventArgs e )
        {
            this.followerPanel.Visibility = System.Windows.Visibility.Hidden;
            this.questPanel.Visibility = System.Windows.Visibility.Hidden;
            this.analysisPanel.Visibility = System.Windows.Visibility.Hidden;

            if ( this.radioFollowers.IsChecked == true )
                this.followerPanel.Visibility = Visibility.Visible;
            else if ( this.radioMissions.IsChecked == true )
            {
                this.questPanel.Visibility = System.Windows.Visibility.Visible;
                if ( missionsComboBox.SelectedIndex == -1 )
                    missionsComboBox.SelectedIndex = 0;
                else if ( this.isNeedReCalculateMission )
                {
                    this.ComboBox_SelectionChanged( null, null );
                    this.isNeedReCalculateMission = false;
                }
            }
            else if ( this.radioAnalysis.IsChecked == true )
            {
                this.analysisPanel.Visibility = System.Windows.Visibility.Visible;
                if ( this.analysisPanel.titleMy.FontSize == 22 )
                {
                    if ( this.analysisPanel.abilityAnalysis.FontSize == 18 )
                        this.analysisPanel.CheckBox_Checked( null, null );
                    else if ( this.analysisPanel.raceAnalysis.FontSize == 18 )
                        this.analysisPanel.RunRaceFilter();
                    else if ( this.analysisPanel.traitAnalysis.FontSize == 18 )
                        this.analysisPanel.TraitCheckBox_Checked( null, null );
                }
            }
        }

        private void ComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if ( this.missionsComboBox.SelectedIndex == -1 )
                return;
            this.ClearImages();
            this.CurrentMission = this.currentMissions.First( x => x.Value.MissionId
                == ( this.missionsComboBox.SelectedItem as ImageComboBoxItem ).Id ).Value;
            this.textItemLevel.Text = this.CurrentMission.ItemLevelNeed.ToString();
            this.textMissionTime.Text = this.CurrentMission.MissionTimeStr;
            this.textReward.Text = this.CurrentMission.MissionReward;
            this.textBasicChance.Text = ( this.CurrentMission.BasicSucessChange * 100 ).ToString();
            this.textRemark.Text = string.IsNullOrEmpty( this.CurrentMission.Remark ) ? "无" : this.CurrentMission.Remark;
            int i = 0;
            int j=0;
            foreach ( KeyValuePair<Follower.Abilities, int> pair in this.CurrentMission.CounterAbilitiesCollection )
            {
                j=pair.Value;
                while ( j > 0 )
                {
                    ( this.abilityPanel.Children[ i ] as TinyImage ).SetUp( pair.Key );
                    ( this.abilityPanel.Children[ i ] as TinyImage ).Visibility = System.Windows.Visibility.Visible;
                    i++;
                    j--;
                }
            }
            this.trait.SetUp( this.CurrentMission.SlayerNeed, true );
            this.titleGrid.Visibility = System.Windows.Visibility.Visible;
            this.PlaceParties( this.checkboxUsingFavorite.IsChecked == true );
        }

        private void readButton_Click( object sender, RoutedEventArgs e )
        {
            LoadData.OpenFile( ref this.TableFollowers );
            if ( this.TableFollowers != null && this.TableFollowers.Rows.Count > 0 )
                this.FillInData();
        }

        private void titleBlock_MouseDown( object sender, MouseButtonEventArgs e )
        {
            this.titleHighMaul.FontSize = 15;
            this.titleRing.FontSize = 15;
            this.titleElse.FontSize = 15;

            ( sender as TextBlock ).FontSize = 18;
            switch ( ( sender as TextBlock ).Name )
            {
                case "titleHighMaul":
                    this.currentMissions = this.Missions.HighmaulMissions;
                    this.FillInMissions( this.Missions.HighmaulMissionRows );
                    break;
                case "titleRing":
                    this.currentMissions = this.Missions.RingMissions;
                    this.FillInMissions( this.Missions.RingMissionRows );
                    break;
                case "titleElse":
                    this.currentMissions = this.Missions.OtherThreeFollowersMissions;
                    this.FillInMissions( this.Missions.OtherThreeFollowersMissionRows );
                    break;
                default: break;
            }
            this.missionsComboBox.SelectedIndex = 0;
        }

        private void missionWindow_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( this.missionGrid == null )
            {
                this.missionGrid = new MissionGrid("所有任务");
                this.missionGrid.Populate( this.Missions.AllMissionsTable );
                this.missionGrid.Owner = this;
            }
            if ( this.missionGrid.Visibility == System.Windows.Visibility.Visible )
                this.missionGrid.Visibility = System.Windows.Visibility.Hidden;
            else
            {
                this.missionGrid.Show();
            }
         }

        private void followerWindow_MouseDown( object sender, MouseButtonEventArgs e )
        {
            if ( this.followerGrid == null )
            {
                this.followerGrid = new MissionGrid("随从列表");
                this.followerGrid.Populate( this.TableFollowers );
                this.followerGrid.Owner = this;
            }
            if ( this.followerGrid.Visibility == System.Windows.Visibility.Visible )
                this.followerGrid.Visibility = System.Windows.Visibility.Hidden;
            else
            {
                this.followerGrid.Show();
            }
         }

        private void titleBlock_MouseEnter( object sender, MouseEventArgs e )
        {
            if ( sender is TextBlock )
                ( sender as TextBlock ).Foreground = Brushes.White;
            this.Cursor = Cursors.Hand;
        }

        private void titleBlock_MouseLeave( object sender, MouseEventArgs e )
        {
            if ( sender is TextBlock )
                ( sender as TextBlock ).Foreground = (Brush)new BrushConverter().ConvertFromString( "#ffe8ce" );
            this.Cursor = Cursors.Arrow;
        }

        private void checkboxMaxiLevel_Checked( object sender, RoutedEventArgs e )
        {
            this.PlaceParties( this.checkboxUsingFavorite.IsChecked == true );
        }

        private void checkboxMaxiLevel_Unchecked( object sender, RoutedEventArgs e )
        {
            this.PlaceParties( this.checkboxUsingFavorite.IsChecked == true );
        }

        private void Window_Closing( object sender, CancelEventArgs e )
        {
            if ( this.missionGrid != null )
            {
                this.missionGrid.ReadyToClose = true;
                this.missionGrid.Close();
            }
            if ( this.followerGrid != null )
            {
                this.followerGrid.ReadyToClose = true;
                this.followerGrid.Close();
            }
        }

        private void tag_MouseEnter( object sender, MouseEventArgs e )
        {
            if ( ( sender as Image ).Name == this.about.Name )
                this.Cursor = Cursors.Help;
            else
                this.Cursor = Cursors.Hand;
        }

        private void tag_MouseLeave( object sender, MouseEventArgs e )
        {
            this.Cursor = Cursors.Arrow;
        }

        private void checkboxUsingFavorite_Checked( object sender, RoutedEventArgs e )
        {
            if ( this.isCheckboxTriggeredByself )
                return;

            if ( this.checkboxUsingFavorite.IsChecked == true )
            {
                if ( this.favoriteFollowers.Count < 3 )
                {
                    MessageBox.Show( "偏好随从少于3个，请选择至少3个偏好随从。", "错误", MessageBoxButton.OK, MessageBoxImage.Warning );
                    this.isCheckboxTriggeredByself = true;
                    this.checkboxUsingFavorite.IsChecked = false;
                    this.isCheckboxTriggeredByself = false;
                }
                else
                    this.PlaceParties( true );
            }
            else
            {
                this.PlaceParties( false );
            }

        }

        private void titleAllFavorite_MouseDown( object sender, MouseButtonEventArgs e )
        {
            this.titleFavorite.FontSize = 18;
            this.titleAll.FontSize = 18;
            this.titleShow.FontSize = 18;
            this.allScroll.Visibility = System.Windows.Visibility.Hidden;
            this.favoriteScroll.Visibility = System.Windows.Visibility.Hidden;
            this.scrollTextTitle.Visibility = System.Windows.Visibility.Hidden;
            this.showGrid.Visibility = System.Windows.Visibility.Hidden;
            this.suggestinoTitle.Visibility = System.Windows.Visibility.Collapsed;

            ( sender as TextBlock ).FontSize = 22;
            switch ( ( sender as TextBlock ).Name )
            {
                case "titleFavorite":
                    this.scrollTextTitle.Visibility = System.Windows.Visibility.Visible;
                    this.suggestinoTitle.Visibility = System.Windows.Visibility.Visible;
                    this.favoriteScroll.Visibility = System.Windows.Visibility.Visible;
                    this.favoriteRows.Children.Clear();
                    this.AddEpicFollowers( true );
                    this.AddFollowers( true );
                    break;
                case "titleAll":
                    this.scrollTextTitle.Visibility = System.Windows.Visibility.Visible;
                    this.allScroll.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "titleShow":
                    this.showGrid.Visibility = System.Windows.Visibility.Visible;
                    if ( this.isNeedToReShow )
                        this.CalculateShowPanel();
                    this.isNeedToReShow = false;
                    break;
                default: break;
            }
        }

        private void maxNumberOfPartyComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if ( !this.IsLoaded )
                return;
            switch ( this.maxNumberOfPartyComboBox.SelectedIndex )
            {
                case 0: this.maxNumberOfTeam = 1; break;
                case 1: this.maxNumberOfTeam = 10; break;
                default:
                case 2: this.maxNumberOfTeam = 20; break;
                case 3: this.maxNumberOfTeam = 50; break;
                case 4: this.maxNumberOfTeam = 100; break;
            }
            this.PlaceParties( this.checkboxUsingFavorite.IsChecked == true );
        }
        private void InputButton_Click( object sender, RoutedEventArgs e )
        {
            InputWindow window = new InputWindow();
            window.Owner = this;
            if ( window.ShowDialog() == true )
            {
                this.FillInData();
            }
        }

        private void simulateButton_Click( object sender, RoutedEventArgs e )
        {
            this.CalculateSuggestion();
            //this.solutionComboBox.Visibility = System.Windows.Visibility.Visible;
            this.favoriteRows.Children.Clear();
            this.AddEpicFollowers( true );
            this.AddFollowers( true );
        }

        private void simulateMyButton_Click( object sender, RoutedEventArgs e )
        {
            if ( this.favoriteFollowers.Count < 3 )
            {
                MessageBox.Show( "偏好随从少于3个，请选择至少3个偏好随从。", "错误", MessageBoxButton.OK, MessageBoxImage.Warning );
                return;
            }
            this.ReCalculateSuccessChance();
        }

        private void solutionComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {

        }

        private void highMaulQuestCheckBox_Click( object sender, RoutedEventArgs e )
        {
            bool hasCheck = false;
            if ( this.highMaulQuestCheckBox.IsChecked == true )
                hasCheck = true;
            else if ( this.blackFoundryQuestCheckBox.IsChecked == true )
                hasCheck = true;
            else if ( this.ringQuestStage1CheckBox.IsChecked == true )
                hasCheck = true;
            else if ( this.ringQuestStage2CheckBox.IsChecked == true )
                hasCheck = true;
            else if ( this.equipment645CheckBox.IsChecked == true )
                hasCheck = true;
            this.simulateButton.IsEnabled = hasCheck;
            this.simulateMyButton.IsEnabled = hasCheck;
        }

        #endregion //Events

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public void FillInData()
        {
            if ( this.TableFollowers != null && this.TableFollowers.Columns.Count == 22 )
            {
                this.titleAllFavorite_MouseDown( this.titleAll, null );
                this.GenerateFollowers();
                if ( this.followerGrid != null )
                    this.followerGrid.Populate( this.TableFollowers );
                this.radioFollowers.Visibility = System.Windows.Visibility.Visible;
                this.radioMissions.Visibility = System.Windows.Visibility.Visible;
                this.radioAnalysis.Visibility = System.Windows.Visibility.Visible;
                this.titleGrid2.Visibility = System.Windows.Visibility.Visible;
                this.followerImage.Visibility = System.Windows.Visibility.Visible;
            }
            else
                MessageBox.Show( "数据不正确，请使用压缩包内的FollowerExport插件导出的数据。", "error", MessageBoxButton.OK, MessageBoxImage.Warning );
        }
        private void FillInMissions( Dictionary<int, Mission> missions )
        {
            List<string> items = new List<string>();
            this.currentMissions = missions;
            foreach ( KeyValuePair<int, Mission> pair in missions )
            {
                items.Add( pair.Key + " " + ( string.IsNullOrWhiteSpace( pair.Value.MissionNameCN ) ? pair.Value.MissionName : pair.Value.MissionNameCN ) );
            }
            this.missionsComboBox.ItemsSource = items;
        }

        private void FillInMissions( List<DataRow> rows )
        {
            this.missionsComboBox.DataSource = rows;
        }

        private void ClearImages()
        {
            foreach ( TinyImage image in this.abilityPanel.Children )
            {
                image.ClearUp();
                image.Visibility = System.Windows.Visibility.Collapsed;
            }
            this.trait.ClearUp();
        }

        private void PlaceParties( bool isUsingFavorite )
        {
            List<Mission> missions;
            if ( this.CurrentMission.FollowersNeed == 3 )
                missions = this.AssignMissionForThreeFollowers( this.CurrentMission, isUsingFavorite );
            else if ( this.CurrentMission.FollowersNeed == 2 )
                missions = this.AssignMissionForTwoFollowers( this.CurrentMission );
            else if ( this.CurrentMission.FollowersNeed == 1 )
                missions = this.AssignMissionForOneFollowers( this.CurrentMission );
            else
                return;

            foreach ( Party party in this.partyPanel.Children )
                party.Collapsed();
            var data = from temp in missions.AsEnumerable()
                       //where temp.TotalSucessChance > 0.8
                       select temp;
            data = data.OrderByDescending( x => x.TotalSucessChance ).ThenBy( x => x.MissionTimeCaculated );

            int number = this.partyPanel.Children.Count;
            //this.partyPanel.Children.Clear();

            int i = 0;
            foreach ( Mission mission in data )
            {
                if ( i >= this.maxNumberOfTeam )
                    break;
                List<string> nameEns = new List<string>();
                if ( this.maxNumberOfTeam > number )
                    this.partyPanel.Children.Add( new Party( mission ) );
                else
                    ( this.partyPanel.Children[ i ] as Party ).Create( mission );
                i++;
            }
        }

        private void GenerateFollowers()
        {
            this.currentFollowers = new List<Follower>();
            this.favoriteFollowers = new List<Follower>();
            List<int> abilities;
            List<int> traits;
            foreach ( DataRow row in this.TableFollowers.Rows )
            {
                abilities = new List<int>();
                abilities.Add( Convert.ToInt16( row[ "技能ID1" ] ) );
                if ( Convert.ToInt16( row[ "品质" ] ) == 4 )
                    abilities.Add( Convert.ToInt16( row[ "技能ID2" ] ) );
                traits = new List<int>();
                traits.Add( Convert.ToInt16( row[ "特长ID1" ] ) );
                if ( Convert.ToInt16( row[ "品质" ] ) > 2 )
                    traits.Add( Convert.ToInt16( row[ "特长ID2" ] ) );
                if ( Convert.ToInt16( row[ "品质" ] ) > 3 )
                    traits.Add( Convert.ToInt16( row[ "特长ID3" ] ) );

                this.currentFollowers.Add( new Follower( row["ID"].ToString(), row[ "姓名" ].ToString(), Convert.ToInt16( row[ "品质" ] ), Convert.ToInt16( row[ "等级" ] ), Convert.ToInt16( row[ "装等" ] ),
                    row[ "种族" ].ToString(), Follower.GetClassBySpec( Convert.ToInt16( row[ "职业ID" ] ) ), row[ "职业" ].ToString(), Convert.ToInt16( row[ "激活" ] ), abilities, traits ) );
            }

            if ( this.currentFollowers.Exists( x => ( x.Name == "Qiana Moonshadow" || x.Name == "奇娅娜·月影" || x.Name == "琪安娜‧月影" ) ) ||
                this.currentFollowers.Exists( x => ( x.Race == Follower.Races.矮人 || x.Race == Follower.Races.暗夜精灵 || x.Race == Follower.Races.狼人 ) ) ||
                !this.currentFollowers.Exists( x => ( x.Race == Follower.Races.被遗忘者 || x.Race == Follower.Races.牛头人 ) ) )
                Globals.IsAlliance = true;
            else
                Globals.IsAlliance = false;

            this.analysisPanel.AssignFollowers( ref this.currentFollowers, ref this.favoriteFollowers );            
            this.followerRows.Children.Clear();
            this.AddEpicFollowers();
            this.AddFollowers();

            this.isNeedToReShow = true;
            this.isNeedReCalculateMission = true;
        }

        private void AddFollowers( bool isFavorite=false )
        {
            if ( isFavorite )
            {
                if ( this.favoriteFollowers.Count == 0 )
                    return;
                foreach ( Follower follower in this.favoriteFollowers.FindAll( x => x.Quolaty < 4 ).OrderBy( x => x.AbilityCollection[ 0 ] ) )
                {
                    this.favoriteRows.Children.Add( new FollowerRow( follower, ref this.favoriteFollowers, this.GetNameEnById( follower.ID ), true ) );
                }
            }
            else
            {
                foreach ( Follower follower in this.currentFollowers.FindAll( x => x.Quolaty < 4 ).OrderBy( x => x.AbilityCollection[ 0 ] ) )
                {
                    this.followerRows.Children.Add( new FollowerRow( follower, ref this.favoriteFollowers, this.GetNameEnById( follower.ID ) ) );
                }
            }
        }

        private void AddEpicFollowers( bool isFavorite = false )
        {
            if ( isFavorite )
            {
                if ( this.favoriteFollowers.Count == 0 )
                    return;
                for ( int i = 0; i < 9; i++ )
                {
                    Follower.Abilities ability = (Follower.Abilities)i;
                    foreach ( Follower follower in this.favoriteFollowers.FindAll( x => ( x.Quolaty == 4 && x.AbilityCollection[ 0 ] == ability ) ).OrderBy( x => x.AbilityCollection[ 1 ] ) )
                    {
                        this.favoriteRows.Children.Add( new FollowerRow( follower, ref this.favoriteFollowers, this.GetNameEnById( follower.ID ), true ) );
                    }
                }
            }
            else for ( int i = 0; i < 9; i++ )
            {
                Follower.Abilities ability = (Follower.Abilities)i;
                foreach ( Follower follower in this.currentFollowers.FindAll( x => ( x.Quolaty == 4 && x.AbilityCollection[ 0 ] == ability ) ).OrderBy( x => x.AbilityCollection[ 1 ] ) )
                {
                    this.followerRows.Children.Add( new FollowerRow( follower, ref this.favoriteFollowers, this.GetNameEnById( follower.ID ) ) );
                }
            }
        }

        private List<Mission> AssignMissionForThreeFollowers( Mission mission, bool isUsingFaverite = false )
        {
            List<Mission> missions = new List<Mission>();
            List<Follower> list = isUsingFaverite ? this.favoriteFollowers : this.currentFollowers;
            if ( list.Count >= 3 )
            {
                for ( int i = 0; i < list.Count; i++ )
                {
                    for ( int j = 0; j < list.Count; j++ )
                    {
                        if ( j <= i )
                            continue;
                        for ( int k = 0; k < list.Count; k++ )
                        {
                            if ( k <= j )
                                continue;
                            else
                            {
                                Mission newMission = mission.Copy();
                                newMission.IsUsingMaxiLevel = this.checkboxMaxiLevel.IsChecked == true;
                                newMission.AssignFollowers( new List<Follower>() { list[ i ], list[ j ], list[ k ] } );
                                missions.Add( newMission );
                            }
                        }
                    }
                }
            }
            return missions;
        }

        private List<Mission> AssignMissionForTwoFollowers( Mission mission )
        {
            List<Follower> followers;
            List<Mission> missions = new List<Mission>();
            for ( int j = 0; j < this.currentFollowers.Count; j++ )
            {
                for ( int k = 0; k < this.currentFollowers.Count; k++ )
                {
                    if ( k <= j )
                        continue;
                    else
                    {
                        Mission newMission = mission.Copy();
                        followers = new List<Follower>();
                        followers.Add( this.currentFollowers[ j ] );
                        followers.Add( this.currentFollowers[ k ] );
                        newMission.AssignFollowers( followers );
                        missions.Add( newMission );
                    }
                }
            }
            return missions;
        }

        private List<Mission> AssignMissionForOneFollowers( Mission mission )
        {
            List<Follower> followers;
            List<Mission> missions = new List<Mission>();
            for ( int k = 0; k < this.currentFollowers.Count; k++ )
            {
                Mission newMission = mission.Copy();
                followers = new List<Follower>();
                followers.Add( this.currentFollowers[ k ] );
                newMission.AssignFollowers( followers );
                missions.Add( newMission );
            }
            return missions;
        }

        public static void ShowDialog( string str )
        {
            MessageBox.Show( "str" );
        }

        public string GetNameEnById( string id )
        {
            if ( Globals.IsAlliance )
                return this.analysisPanel.AliFollowers.FirstOrDefault( x => x.ID == id ).NameEN;
            else
                return this.analysisPanel.HrdFollowers.FirstOrDefault( x => x.ID == id ).NameEN;
        }

        private void CalculateSuggestion()
        {
            bool isContainHighmaul = this.highMaulQuestCheckBox.IsChecked == true;
            bool isContainBlackFoundry = this.blackFoundryQuestCheckBox.IsChecked == true;
            bool isContainRingStage1 = this.ringQuestStage1CheckBox.IsChecked == true;
            bool isContainRingStage2 = this.ringQuestStage2CheckBox.IsChecked == true;
            bool isContainEquipment645 = this.equipment645CheckBox.IsChecked == true;

            Solution solution = new Solution( isContainHighmaul, isContainRingStage1, isContainRingStage2, isContainEquipment645, isContainBlackFoundry );
            solution.CalculateBasicData( this.Missions, this.currentFollowers );
            solution.ListAllPossiblities();
            solution.ReduceRedundency( this.Missions );

            foreach ( FollowerRow row in this.followerRows.Children )
            {
                if ( solution.suggestedFollowers.Exists( x => x.ID == row.currentFollower.ID ) )
                    row.isFavorit.IsChecked = true;
                else
                    row.isFavorit.IsChecked = false;
            }
            this.GenerateString( solution.uncompleteIDs );
        }

        private void ReCalculateSuccessChance()
        {
            bool isContainHighmaul = this.highMaulQuestCheckBox.IsChecked == true;
            bool isContainBlackFoundry = this.blackFoundryQuestCheckBox.IsChecked == true;
            bool isContainRingStage1 = this.ringQuestStage1CheckBox.IsChecked == true;
            bool isContainRingStage2 = this.ringQuestStage2CheckBox.IsChecked == true;
            bool isContainEquipment645 = this.equipment645CheckBox.IsChecked == true;

            Solution solution = new Solution( isContainHighmaul, isContainRingStage1, isContainRingStage2, isContainEquipment645, isContainBlackFoundry );
            solution.CalculateBasicDataSimple( this.Missions, this.favoriteFollowers );
            this.GenerateString( solution.uncompleteIDs );
        }

        public void GenerateString( List<int> uncompleteIDs )
        {
            if ( this.highMaulQuestCheckBox.IsChecked == true )
                this.GenerateText( this.textHighmaulQuest, uncompleteIDs, 313, 316 );
            else
                this.textHighmaulQuest.Text = string.Empty;

            if ( this.ringQuestStage1CheckBox.IsChecked == true )
                this.GenerateText( this.textQuestRing1, uncompleteIDs, 403, 407 );
            else
                this.textQuestRing1.Text = string.Empty;

            if ( this.ringQuestStage2CheckBox.IsChecked == true )
                this.GenerateText( this.textQuestRing2, uncompleteIDs, 408, 413 );
            else
                this.textQuestRing2.Text = string.Empty;

            if ( this.equipment645CheckBox.IsChecked == true )
                this.GenerateText( this.textEquipment645, uncompleteIDs, 290, 296 );
            else
                this.textEquipment645.Text = string.Empty;

            if ( this.blackFoundryQuestCheckBox.IsChecked == true )
                this.GenerateText( this.textBlackFoundryQuest, uncompleteIDs, 454, 457 );
            else
                this.textEquipment645.Text = string.Empty;
        }

        private void GenerateText( TextBlock block, List<int> uncompleteIDs, int bottomNumber, int topNumber )
        {
            int totalNumber = topNumber - bottomNumber + 1;
            int uncompleteNumber = 0;
            string textAppend = string.Empty;
            for ( int i = bottomNumber; i <= topNumber; i++ )
            {
                if ( uncompleteIDs.Contains( i ) )
                {
                    uncompleteNumber++;
                    textAppend += i.ToString() + " ";
                }
            }
            if ( uncompleteNumber > 0 )
            {
                block.Text = "完成度" + ( totalNumber - uncompleteNumber ).ToString() + "/" + totalNumber.ToString() + "，无法达标：" + textAppend;
                block.Foreground = Brushes.Red;
            }
            else
            {
                block.Text = "完成度" + totalNumber.ToString() + "/" + totalNumber.ToString();
                block.Foreground = Brushes.Lime;
            }
        }

        private void CalculateShowPanel()
        {
            Solution solution = new Solution( true, true, true, true, true );
            solution.CalculateBasicDataSimple( this.Missions, this.currentFollowers );
            this.GenerateText( this.showHighmaulText, solution.uncompleteIDs, 313, 316 );
            this.GenerateText( this.showRing1Text, solution.uncompleteIDs, 403, 407 );
            this.GenerateText( this.showRing2Text, solution.uncompleteIDs, 408, 413 );
            this.GenerateText( this.showBlackFoundryText, solution.uncompleteIDs, 454, 457 );

            int completeNumber = 0;
            if ( this.showHighmaulText.Foreground == Brushes.Lime )
                completeNumber++;
            if ( this.showRing1Text.Foreground == Brushes.Lime )
                completeNumber++;
            if ( this.showRing2Text.Foreground == Brushes.Lime )
                completeNumber++;

            this.totalFollowerText.Text = this.currentFollowers.Count.ToString();
            this.epicFollowerText.Text = this.currentFollowers.Count( x => x.Quolaty == 4 ).ToString();
            this.blueFollowerText.Text = this.currentFollowers.Count( x => x.Quolaty == 3 ).ToString();
            this.greenFollowerText.Text = this.currentFollowers.Count( x => x.Quolaty == 2 ).ToString();

            string prefix = string.Empty;
            string name = string.Empty;
            if ( completeNumber == 3 )
                prefix = "欧洲皇家";
            else if ( completeNumber == 2 )
                prefix = "欧洲";
            else if ( completeNumber == 1 )
                prefix = "亚洲";
            else if ( completeNumber == 0 )
                prefix = "非洲";

            if ( this.currentFollowers.Count( x => x.Quolaty == 4 ) >= 30 )
                name = "精锐";

            if ( this.currentFollowers.Count >= 60 )
                name += "德拉诺之王";
            else if ( this.currentFollowers.Count >= 50 )
                name += "大统帅";
            else if ( this.currentFollowers.Count >= 40 )
                name += "元帅";
            else if ( this.currentFollowers.Count >= 30 )
                name += "指挥官";
            else if ( this.currentFollowers.Count >= 20 )
                name += "士官";
            else if ( this.currentFollowers.Count >= 10 )
                name += "小队长";
            else
                name += "小兵";

            this.achievementText.Text = prefix + name;
        }
        #endregion //Methods



    }
}
