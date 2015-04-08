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
using System.ComponentModel;
using System.Collections;
using System.Data;
using YesCommander.Classes;
using System.Text.RegularExpressions;

namespace YesCommander.CustomControls.Components
{
    /// <summary>
    /// Interaction logic for ImageComboBox.xaml
    /// </summary>
    public partial class ImageComboBox : ComboBox
    {
        #region Variables

        private List<ImageSource> imageSourceList;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ImageComboBox()
        {
            InitializeComponent();
            //this.PreviewTextInput += this.ComboBoxPreviewTextInput;
        }

        #endregion

        #region Properties
        /// <summary>
        /// The ImageList [Winform compatiblility]
        /// </summary>
        [Browsable( false )]
        public List<ImageSource> ComboBoxImageList
        {
            set
            {
                if ( value != null && value.Count > 0 )
                {

                    this.imageSourceList = value;
                }
                else
                {
                    this.imageSourceList = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable DataSource
        {
            get { return base.ItemsSource; }
            set
            {
                if ( value == null )
                {
                    base.ItemsSource = null;
                    return;
                }

                if ( !( value is List<DataRow> ) && !(value is List<ImageComboBoxItem>) )//&& !( value is TreeNodeObject[] ) && !( value is EXNameIndexPair<string>[] ) && !( value is List<TreeNodeObject> ) )
                {
                    throw new NotSupportedException();
                }

                this.BeginInit();

                if ( value is List<DataRow> )
                {
                    List<ImageComboBoxItem> items = new List<ImageComboBoxItem>();

                    if ( value is List<DataRow> )
                    {
                        List<DataRow> collection = value as List<DataRow>;

                        if ( collection == null )
                        {
                            this.ItemsSource = null;
                            return;
                        }

                        foreach ( DataRow row in collection )
                        {
                            items.Add( GenerateItem( row ) );
                        }

                        this.ItemsSource = items;
                    }
                }
                else if ( value is List<ImageComboBoxItem> )
                {
                    this.ItemsSource = value;
                }
                this.EndInit();

            }
        }

        #endregion


        /// <summary>
        /// Generate an ImageComboBox item for binding.
        /// </summary>
        /// <param name="row">The DataRow.</param>
        /// <returns></returns>
        private ImageComboBoxItem GenerateItem( DataRow row )
        {
            ImageComboBoxItem item = new ImageComboBoxItem()
            {
                Name = string.IsNullOrEmpty( row[ "任务中文名" ].ToString() ) ? row[ "任务名" ].ToString() : row[ "任务中文名" ].ToString(),
                Id = row[ "任务ID" ].ToString(),
                Image = GenerateItemImage( row ),
                DataRowValue = row
            };

            if ( item.Image == this.imageSourceList[ 4 ] )
            {
                string reward = Convert.ToInt32( Regex.Replace( row[ "奖励" ].ToString(), @"[^\d.\d]", "" ) ).ToString();
                item.Name = "(" + reward + ") " + item.Name;
            }
            else if ( item.Image == this.imageSourceList[ 5 ] )
            {
                string reward = Globals.GetGoldRewardFromString( row[ "奖励" ].ToString() ).ToString();
                item.Name = "(" + reward + ") " + item.Name;
            }
            return item;
        }

        /// <summary>
        /// Generate a BitmapImage for the ImageComboBoxItem.
        /// </summary>
        /// <param name="row">The DataRow.</param>
        /// <returns></returns>
        private ImageSource GenerateItemImage( DataRow row )
        {
            if ( row == null )
            {
                throw new NotSupportedException();
            }

            if ( this.imageSourceList == null || this.imageSourceList.Count == 0 )
            {
                return null;
            }

            int id = -1;
            id = Globals.FetchDatabaseInteger( row, "任务ID", -1 );
            if ( id >= 313 && id <= 324 )
                return this.imageSourceList[ 0 ];
            else if ( id >= 427 && id <= 430 )
                return this.imageSourceList[ 3 ];
            else if ( id >= 403 && id <= 407 )
                return this.imageSourceList[ 1 ];
            else if ( id >= 408 && id <= 413 )
                return this.imageSourceList[ 2 ];
            else if ( row.Field<string>( "奖励" ).Contains( "要塞物资" ) )
                return this.imageSourceList[ 4 ];
            else if ( row.Field<string>( "奖励" ).Contains( "G" ) )
                return this.imageSourceList[ 5 ];
            else
            {
                return null;
            }
        }

        private ImageSource GenerateItemImage( int index )
        {

            if ( this.imageSourceList == null || this.imageSourceList.Count == 0 )
            {
                return null;
            }

            return this.imageSourceList[ index ];
        }
    }
}
