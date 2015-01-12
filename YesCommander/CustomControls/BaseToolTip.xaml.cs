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

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for BaseToolTip.xaml
    /// </summary>
    public partial class BaseToolTip : UserControl
    {
        public BaseToolTip()
        {
            InitializeComponent();
        }

        public BaseToolTip( string title, string content )
        {
            InitializeComponent();
            this.Setup( title, content );
        }

        public void Setup( string title, string content )
        {
            ( this.stackPanel1.Children[ 0 ] as TextBlock ).Text = title;
            ( this.stackPanel1.Children[ 1 ] as TextBlock ).Text = content;
        }
    }
}
