using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YesCommander.CustomControls.Components
{
    /// <summary>
    /// The class is for the ImageComboBox item binding.
    /// </summary>
    public class ImageComboBoxItem : DependencyObject
    {
        #region Constructor
        /// <summary>
        ///  Constructor
        /// </summary>
        public ImageComboBoxItem() { }
        #endregion

        #region DependencyProperty
        private static readonly DependencyProperty HighlightColorProperty = DependencyProperty.Register(
                                                                                "HighlightColor",
                                                                                typeof( Brush ),
                                                                                typeof( ImageComboBoxItem ),
                                                                                new PropertyMetadata( null ) );

        /// <summary>
        /// Sets or Gets the highlight color of the ComboBox item.
        /// </summary>
        public Brush HighlightColor
        {
            get { return this.GetValue( HighlightColorProperty ) as Brush; }
            set { this.SetValue( HighlightColorProperty, value ); }
        }
        #endregion

        #region Properties

        /// <summary>
        /// The text for showing in the ComboBox item.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///  The value for the ComboBox item.
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// The image for the ComboBox item.
        /// </summary>
        [DefaultValue( null )]
        public ImageSource Image
        {
            get;
            set;

        }

        ///// <summary>
        ///// ExNameIndexPair value.
        ///// </summary>
        //[DefaultValue( null )]
        //public EXNameIndexPair<string> ExNameIndexPairValue
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// DataRow value.
        /// </summary>
        [DefaultValue( null )]
        public DataRow DataRowValue
        {
            get;
            set;
        }

        /// <summary>
        /// DataRow value.
        /// </summary>
        [DefaultValue( null )]
        public object Tag
        {
            get;
            set;
        }
        #endregion
    }
}
