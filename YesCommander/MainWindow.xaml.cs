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
        private Dictionary<int, Mission> currentMissions;
        private MissionGrid missionGrid;
        private MissionGrid followerGrid;
        private bool isCheckboxTriggeredByself = false;
        private int maxNumberOfTeam = 20;
        private bool isNeedToReShow = false;
        private bool isNeedReCalculateMission = false;
        private bool isIgnoreAllInativatedFollowers = false;

        public MainWindow()
        {
            InitializeComponent();
            this.maxNumberOfPartyComboBox.ItemsSource = new List<string>() { "最多显示1只队伍", "最多显示10只队伍", "最多显示20只队伍", "最多显示50只队伍", "最多显示100只队伍(降低性能)" };
            this.maxNumberOfPartyComboBox.SelectedIndex = 2;
        }

        #region Events
        private void Loaded_1( object sender, RoutedEventArgs e )
        {
            try
            {
                Globals.Initialize();

                this.Missions = new Missions();
                //this.FillInMissions( this.Missions.HighmaulMissions );
                this.missionsComboBox.ComboBoxImageList = Globals.missionIcionList;
                this.currentMissions = this.Missions.RaidAndRingMissions;
                this.FillInMissions( this.Missions.RaidAndRingMissionRows );
                this.radioFollowers.IsChecked = true;
                Globals.FavoriteFollowers = new List<Follower>();

                Version version = Assembly.GetEntryAssembly().GetName().Version;
                this.about.ToolTip = new BaseToolTip( "YesCommander", "作者：梧桐哟梧桐，当前版本："
                    + version.Major.ToString() + "." + version.Minor.ToString() + ( version.Build == 0 ? string.Empty : "." + version.Build.ToString() + ( version.Revision == 0 ? string.Empty : "." + version.Revision.ToString() ) )
                    + "\r\n\r\n注意：此软件仅发布在NGA。下载地址仅限百度网盘，其他途径下载均有风险。"
                    + " \r\n\r\n熊猫酒仙联盟公会【月 神 之 怒】招募有识之士。共同迎战德拉诺之王。"
                    + " \r\n\r\n广告位招租。" );
                ToolTipService.SetShowDuration( this.about, 60000 );
                ToolTipService.SetInitialShowDelay( this.about, 0 );
                this.missionWindowImage.ToolTip = new BaseToolTip( "任务列表", "打开一个新的窗口，查看所有任务详情。" );
                ToolTipService.SetInitialShowDelay( this.missionWindowImage, 0 );
                this.simulateButton.ToolTip = new BaseToolTip( "推荐偏好(675)", "在模拟中默认使用675装等。此外，推荐只是提供一种可能的选取方案，并非一定为最少随从方案。请酌情参考。" );
                ToolTipService.SetInitialShowDelay( this.simulateButton, 0 );
                this.simulateButtonForCurrentIlevel.ToolTip = new BaseToolTip( "推荐偏好(当前)", "在模拟中默认使用当前装等。低于任务要求装等的随从将使用任务要求装等。此推荐为1.6.5以前版本默认推荐。" );
                ToolTipService.SetInitialShowDelay( this.simulateButtonForCurrentIlevel, 0 );
                this.simulateMyButton.ToolTip = new BaseToolTip( "计算概率", "重新计算概率。推荐偏好仅供参考，建议自行斟酌后使用此功能查看概率，默认使用675装等。" );
                ToolTipService.SetInitialShowDelay( this.simulateMyButton, 0 );
                this.ignoreTextBlock.ToolTip = new BaseToolTip( "忽略随从", "被忽略的随从将不参与任何模拟（展示除外）。右键点击忽略所有冻结的随从，再次点击启用所有冻结随从。" );
                ToolTipService.SetInitialShowDelay( this.ignoreTextBlock, 0 );
            }
            catch
            {
                throw;
            }
        }

        private void RadioButton_Checked( object sender, RoutedEventArgs e )
        {
            this.followerPanel.Visibility = System.Windows.Visibility.Hidden;
            this.questPanel.Visibility = System.Windows.Visibility.Hidden;
            this.analysisPanel.Visibility = System.Windows.Visibility.Hidden;
            this.combinationPanel.Visibility = System.Windows.Visibility.Hidden;

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
            else if ( this.radioCombination.IsChecked == true )
            {
                this.combinationPanel.Visibility = System.Windows.Visibility.Visible;
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
            float j = 0;
            foreach ( KeyValuePair<Follower.Abilities, float> pair in this.CurrentMission.CounterAbilitiesCollection )
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
            this.titleResource.FontSize = 15;
            this.titleElse.FontSize = 15;

            this.successOrEarn.Text = "成功率";

            ( sender as TextBlock ).FontSize = 18;
            switch ( ( sender as TextBlock ).Name )
            {
                case "titleHighMaul":
                    this.currentMissions = this.Missions.RaidAndRingMissions;
                    this.FillInMissions( this.Missions.RaidAndRingMissionRows );
                    break;
                case "titleResource":
                    this.successOrEarn.Text = "资源(成功率)";
                    this.currentMissions = this.Missions.GarrisonResourceMissions;
                    this.FillInMissions( this.Missions.GarrisonResourceMissionRows );
                    break;
                case "titleTwoFollowerMission":
                    this.currentMissions = this.Missions.TwoFollowerMissions;
                    this.FillInMissions( this.Missions.TwoFollowerMissionRows );
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
                if ( Globals.FavoriteFollowers.Count < 3 )
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
                    this.AddFollowers( string.Empty, true );
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
            Globals.IsUsingMaxILevelOnSimulateAll = true;
            this.CalculateSuggestion();
            //this.solutionComboBox.Visibility = System.Windows.Visibility.Visible;
            this.favoriteRows.Children.Clear();
            this.AddFollowers( string.Empty, true );
        }
        private void simulateButtonForCurrentIlevel_Click( object sender, RoutedEventArgs e )
        {
            Globals.IsUsingMaxILevelOnSimulateAll = false;
            this.CalculateSuggestion();
            //this.solutionComboBox.Visibility = System.Windows.Visibility.Visible;
            this.favoriteRows.Children.Clear();
            this.AddFollowers( string.Empty, true );
            Globals.IsUsingMaxILevelOnSimulateAll = true;
        }


        private void simulateMyButton_Click( object sender, RoutedEventArgs e )
        {
            if ( Globals.FavoriteFollowers.Count < 3 )
            {
                MessageBox.Show( "偏好随从少于3个，请选择至少3个偏好随从。", "错误", MessageBoxButton.OK, MessageBoxImage.Warning );
                return;
            }
            Globals.IsUsingMaxILevelOnSimulateAll = true;
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
            else if ( this.twoFollowerQuestCheckBox.IsChecked == true )
                hasCheck = true;
            this.simulateButton.IsEnabled = hasCheck;
            this.simulateButtonForCurrentIlevel.IsEnabled = hasCheck;
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
                this.radioCombination.Visibility = System.Windows.Visibility.Visible;
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
                missions = this.AssignMissionForTwoFollowers( this.CurrentMission, isUsingFavorite );
            else if ( this.CurrentMission.FollowersNeed == 1 )
                missions = this.AssignMissionForOneFollowers( this.CurrentMission );
            else
                return;

            foreach ( Party party in this.partyPanel.Children )
                party.Collapsed();
            var data = from temp in missions.AsEnumerable()
                       //where temp.TotalSucessChance > 0.8
                       select temp;
            if ( this.CurrentMission.MissionReward.Contains( "要塞物资" ) && this.CurrentMission.FollowersNeed > 1 )
            {
                data = from temp in missions.AsEnumerable()
                       where temp.TotalSucessChance >= 0.70
                       select temp;
                data = data.OrderByDescending( x => x.CurrencyReward ).ThenByDescending( x => x.TotalSucessChance );
            }
            else
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
            Globals.CurrentValidFollowers = new List<Follower>();
            Globals.AllFollowers = new List<Follower>();
            Globals.FavoriteFollowers = new List<Follower>();
            List<int> abilities;
            List<int> traits;
            foreach ( DataRow row in this.TableFollowers.Rows )
            {
                abilities = new List<int>();
                abilities.Add( Convert.ToInt16( row[ "技能ID1" ] ) );
                if ( Convert.ToInt16( row[ "品质" ] ) >= 4 )
                    abilities.Add( Convert.ToInt16( row[ "技能ID2" ] ) );
                traits = new List<int>();
                traits.Add( Convert.ToInt16( row[ "特长ID1" ] ) );
                if ( Convert.ToInt16( row[ "品质" ] ) > 2 )
                    traits.Add( Convert.ToInt16( row[ "特长ID2" ] ) );
                if ( Convert.ToInt16( row[ "品质" ] ) > 3 )
                    traits.Add( Convert.ToInt16( row[ "特长ID3" ] ) );

                Globals.AllFollowers.Add( new Follower( row[ "ID" ].ToString(), row[ "姓名" ].ToString(), Convert.ToInt16( row[ "品质" ] ), Convert.ToInt16( row[ "等级" ] ), Convert.ToInt16( row[ "装等" ] ),
                    row[ "种族" ].ToString(), Follower.GetClassBySpec( Convert.ToInt16( row[ "职业ID" ] ) ), row[ "职业" ].ToString(), Convert.ToInt16( row[ "激活" ] ), abilities, traits ) );
            }
            Globals.CurrentValidFollowers.AddRange( Globals.AllFollowers );

            if ( Globals.AllFollowers.Exists( x => ( x.Name == "Qiana Moonshadow" || x.Name == "奇娅娜·月影" || x.Name == "琪安娜‧月影" ) ) ||
                Globals.AllFollowers.Exists( x => ( x.Race == Follower.Races.矮人 || x.Race == Follower.Races.暗夜精灵 || x.Race == Follower.Races.狼人 ) ) ||
                !Globals.AllFollowers.Exists( x => ( x.Race == Follower.Races.被遗忘者 || x.Race == Follower.Races.牛头人 ) ) )
                Globals.IsAlliance = true;
            else
                Globals.IsAlliance = false;

            this.analysisPanel.AssignFollowers( ref Globals.AllFollowers, ref Globals.FavoriteFollowers );
            this.analysisPanel.AddTraitCheckBoxes();
            this.followerRows.Children.Clear();
            this.AddFollowers( string.Empty );

            this.isNeedToReShow = true;
            this.isNeedReCalculateMission = true;
            this.combinationPanel.PlaceFollowers();
        }

        private void AddFollowers( string orderByStr, bool isFavorite = false )
        {
            StackPanel panel = isFavorite ? this.favoriteRows : this.followerRows;
            List<Follower> followerList = isFavorite ? Globals.FavoriteFollowers : Globals.AllFollowers;
            if ( followerList.Count == 0 )
                return;


            List<Follower> orderedFollowers = new List<Follower>();

            switch ( orderByStr )
            {
                default:
                case "技能":
                    for ( int i = 0; i < 9; i++ )
                    {
                        Follower.Abilities ability = (Follower.Abilities)i;
                        foreach ( Follower follower in followerList.FindAll( x => ( x.Quolaty >= 4 && x.AbilityCollection[ 0 ] == ability ) ).OrderBy( x => x.AbilityCollection[ 1 ] ) )
                        {
                            orderedFollowers.Add( follower );
                        }
                    }
                    foreach ( Follower follower in followerList.FindAll( x => x.Quolaty < 4 ).OrderBy( x => x.AbilityCollection[ 0 ] ) )
                    {
                        orderedFollowers.Add( follower );
                    }
                    if ( isFavorite )
                        Globals.FavoriteFollowers = orderedFollowers;
                    else
                        Globals.AllFollowers = orderedFollowers;
                    followerList = isFavorite ? Globals.FavoriteFollowers : Globals.AllFollowers;
                    break;
                case "偏好":
                    followerList = followerList.OrderBy( x => !Globals.FavoriteFollowers.Contains( x ) ).ToList();
                    break;
                case "姓名":
                    followerList = followerList.OrderBy( x => x.Name ).ToList();
                    break;
                case "种族":
                    followerList = followerList.OrderBy( x => x.Race ).ToList();
                    break;
                case "职业":
                    followerList = followerList.OrderBy( x => x.Class ).ToList();
                    break;
                case "等级":
                    followerList = followerList.OrderByDescending( x => x.Level ).ToList();
                    break;
                case "装等":
                    followerList = followerList.OrderByDescending( x => x.ItemLevel ).ToList();
                    break;
                case "冻结":
                    followerList = followerList.OrderBy( x => !x.IsActive ).ToList();
                    break;
                case "忽略":
                case "特长":
                case "可能习得":
                    break;
            }

            if ( !isFavorite )
                followerList = followerList.OrderBy( x => !Globals.CurrentValidFollowers.Contains( x ) ).ToList();

            if ( isFavorite )
                Globals.FavoriteFollowers = followerList;
            else
                Globals.AllFollowers = followerList;

            followerList = isFavorite ? Globals.FavoriteFollowers : Globals.AllFollowers;

            foreach ( Follower follower in followerList )
            {
                panel.Children.Add( new FollowerRow( follower, Globals.FavoriteFollowers, this.GetNameEnById( follower.ID ), isFavorite ) );
            }
        }

        private List<Mission> AssignMissionForThreeFollowers( Mission mission, bool isUsingFaverite = false )
        {
            List<Mission> missions = new List<Mission>();
            List<Follower> list = isUsingFaverite ? Globals.FavoriteFollowers : Globals.CurrentValidFollowers;
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

        private List<Mission> AssignMissionForTwoFollowers( Mission mission, bool isUsingFaverite = false )
        {
            List<Mission> missions = new List<Mission>();
            List<Follower> list = isUsingFaverite ? Globals.FavoriteFollowers : Globals.CurrentValidFollowers;
            for ( int j = 0; j < list.Count; j++ )
            {
                for ( int k = 0; k < list.Count; k++ )
                {
                    if ( k <= j )
                        continue;
                    else
                    {
                        Mission newMission = mission.Copy();
                        newMission.IsUsingMaxiLevel = this.checkboxMaxiLevel.IsChecked == true;
                        newMission.AssignFollowers( new List<Follower>() { list[ j ], list[ k ] } );
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
            for ( int k = 0; k < Globals.CurrentValidFollowers.Count; k++ )
            {
                Mission newMission = mission.Copy();
                followers = new List<Follower>();
                followers.Add( Globals.CurrentValidFollowers[ k ] );
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
            bool isContainTwoFollowerMissions = this.twoFollowerQuestCheckBox.IsChecked == true;

            Solution solution = new Solution( isContainHighmaul, isContainRingStage1, isContainRingStage2, isContainEquipment645, isContainBlackFoundry, isContainTwoFollowerMissions );
            solution.CalculateBasicData( this.Missions, Globals.CurrentValidFollowers );
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
            bool isContainTwoFollowerMissions = this.twoFollowerQuestCheckBox.IsChecked == true;

            Solution solution = new Solution( isContainHighmaul, isContainRingStage1, isContainRingStage2, isContainEquipment645, isContainBlackFoundry, isContainTwoFollowerMissions );
            solution.CalculateBasicDataSimple( this.Missions, Globals.FavoriteFollowers );
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
                this.textBlackFoundryQuest.Text = string.Empty;

            if ( this.twoFollowerQuestCheckBox.IsChecked == true )
            {
                List<int> missionIDs = new List<int>();
                missionIDs.Add( 379 );
                missionIDs.Add( 381 );
                missionIDs.Add( 391 );
                for ( int i = 394; i <= 399; i++ )
                    missionIDs.Add( i );
                missionIDs.Add( 401 );
                missionIDs.Add( 402 );
                missionIDs.Add( 444 );
                missionIDs.Add( 445 );
                missionIDs.Add( 495 );
                missionIDs.Add( 496 );
                missionIDs.Add( 503 );
                this.GenerateText( this.textTwoFollowerQuest, uncompleteIDs, missionIDs );
            }
            else
                this.textTwoFollowerQuest.Text = string.Empty;

            this.textFollowerCount.Text = "偏好随从数：" + Globals.FavoriteFollowers.Count;
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

        private void GenerateText( TextBlock block, List<int> uncompleteIDs, List<int> missionIds )
        {
            int totalNumber = missionIds.Count;
            int uncompleteNumber = 0;
            string textAppend = string.Empty;
            for ( int i = 0; i < missionIds.Count; i++ )
            {
                if ( uncompleteIDs.Contains( missionIds[ i ] ) )
                {
                    uncompleteNumber++;
                    textAppend += missionIds[ i ].ToString() + " ";
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
            Globals.IsUsingMaxILevelOnSimulateAll = true;
            Solution solution = new Solution( true, true, true, true, true, true );
            solution.CalculateBasicDataSimple( this.Missions, Globals.AllFollowers );
            this.GenerateText( this.showHighmaulText, solution.uncompleteIDs, 313, 316 );
            this.GenerateText( this.showRing1Text, solution.uncompleteIDs, 403, 407 );
            this.GenerateText( this.showRing2Text, solution.uncompleteIDs, 408, 413 );
            this.GenerateText( this.showBlackFoundryText, solution.uncompleteIDs, 454, 457 );

            List<int> missionIDs = new List<int>();
            missionIDs.Add( 379 );
            missionIDs.Add( 381 );
            missionIDs.Add( 391 );
            for ( int i = 394; i <= 399; i++ )
                missionIDs.Add( i );
            missionIDs.Add( 401 );
            missionIDs.Add( 402 );
            missionIDs.Add( 444 );
            missionIDs.Add( 445 );
            missionIDs.Add( 495 );
            missionIDs.Add( 496 );
            missionIDs.Add( 503 );
            this.GenerateText( this.showTwoFollowerMissionText, solution.uncompleteIDs, missionIDs );

            int completeNumber = 0;
            if ( this.showBlackFoundryText.Foreground == Brushes.Lime )
                completeNumber++;
            if ( this.showHighmaulText.Foreground == Brushes.Lime )
                completeNumber++;
            if ( this.showRing1Text.Foreground == Brushes.Lime )
                completeNumber++;
            if ( this.showRing2Text.Foreground == Brushes.Lime )
                completeNumber++;
            if ( this.showTwoFollowerMissionText.Foreground == Brushes.Lime )
                completeNumber++;

            this.totalFollowerText.Text = Globals.AllFollowers.Count.ToString();
            this.epicFollowerText.Text = Globals.AllFollowers.Count( x => x.Quolaty == 4 ).ToString();
            this.blueFollowerText.Text = Globals.AllFollowers.Count( x => x.Quolaty == 3 ).ToString();
            this.greenFollowerText.Text = Globals.AllFollowers.Count( x => x.Quolaty == 2 ).ToString();

            string prefix = string.Empty;
            string name = string.Empty;
            if ( completeNumber == 5 )
                prefix = "被幸运女神偏爱的";
            else if ( completeNumber == 4 )
                prefix = "被神眷顾的";
            else if ( completeNumber == 3 )
                prefix = "欧洲皇家";
            else if ( completeNumber == 2 )
                prefix = "欧洲";
            else if ( completeNumber == 1 )
                prefix = "亚洲";
            else if ( completeNumber == 0 )
                prefix = "非洲";

            if ( Globals.AllFollowers.Count( x => x.Quolaty == 4 ) >= 70 )
                name = "至尊";
            else if ( Globals.AllFollowers.Count( x => x.Quolaty == 4 ) >= 50 )
                name = "残酷";
            else if ( Globals.AllFollowers.Count( x => x.Quolaty == 4 ) >= 30 )
                name = "精锐";


            if ( Globals.AllFollowers.Count >= 70 )
                name += "德拉诺之神";
            else if ( Globals.AllFollowers.Count >= 60 )
                name += "德拉诺之王";
            else if ( Globals.AllFollowers.Count >= 50 )
                name += "大统帅";
            else if ( Globals.AllFollowers.Count >= 40 )
                name += "元帅";
            else if ( Globals.AllFollowers.Count >= 30 )
                name += "指挥官";
            else if ( Globals.AllFollowers.Count >= 20 )
                name += "士官";
            else if ( Globals.AllFollowers.Count >= 10 )
                name += "小队长";
            else
                name += "小兵";

            if ( completeNumber >= 4 && Globals.AllFollowers.Count <= 10 )
            {
                prefix = "你丫作弊吧？";
                name = string.Empty;
            }

            this.achievementText.Text = prefix + name;
        }
        #endregion //Methods


        private void TextBlock_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            this.favoriteRows.Children.Clear();
            this.AddFollowers( ( sender as TextBlock ).Text, true );

            this.followerRows.Children.Clear();
            this.AddFollowers( ( sender as TextBlock ).Text );
        }

        private void ignoreTextBlock_MouseRightButtonDown_1( object sender, MouseButtonEventArgs e )
        {
            if (   this.titleAll.FontSize == 22 )
            {
                this.isIgnoreAllInativatedFollowers = !this.isIgnoreAllInativatedFollowers;
                foreach ( FollowerRow row in this.followerRows.Children )
                {
                    if ( !row.currentFollower.IsActive )
                        row.isIgnored.IsChecked = this.isIgnoreAllInativatedFollowers;
                }
            }
        }



    }
}
