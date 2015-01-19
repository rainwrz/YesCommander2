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
    /// Interaction logic for SpecIcon.xaml
    /// </summary>
    public partial class SpecIcon : UserControl
    {
        public SpecIcon()
        {
            InitializeComponent();
        }

        public SpecIcon( int index )
        {
            InitializeComponent();
            this.IconIndex = index;
        }

        public int IconIndex
        {
            set
            {
                this.image.Source = Globals.specIcionList[ value ];
                TextBlock toolTip = new TextBlock();
                toolTip.Text = Follower.GetCNStringByClass( (Follower.Classes)value );
                this.ToolTip =  toolTip;
                ToolTipService.SetInitialShowDelay( this, 0 );
            }
        }

        public bool IsDisplayed
        {
            set
            {
                this.Opacity = value ? 1 : 0.1;
                this.IsEnabled = value;
            }
        }

        public void Clear()
        {
            //this.image.Source = null;
            //this.ToolTip = null;
        }
    }
}
