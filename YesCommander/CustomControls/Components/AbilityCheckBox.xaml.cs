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

namespace YesCommander.CustomControls.Components
{
    /// <summary>
    /// Interaction logic for AbilityCheckBox.xaml
    /// </summary>
    public partial class AbilityCheckBox : UserControl
    {
        public AbilityCheckBox()
        {
            InitializeComponent();
        }

        public event EventHandler CheckBoxClicked;

        private void check_Checked_1( object sender, RoutedEventArgs e )
        {
            if ( this.CheckBoxClicked != null )
            {
                this.CheckBoxClicked( this, RoutedEventArgs.Empty );
            }
        }

        private void check_Unchecked_1( object sender, RoutedEventArgs e )
        {
            if ( this.CheckBoxClicked != null )
            {
                this.CheckBoxClicked( this, RoutedEventArgs.Empty );
            }
        }

        public bool IsChecked
        {
            get
            {
                if ( this.check.IsChecked == true )
                    return true;
                else
                    return false;
            }
        }

        public bool IsDisPlayed
        {
            set
            {
                if ( value )
                {
                    this.IsEnabled = true;
                    this.Opacity = 1;
                }
                else
                {
                    this.IsEnabled = false;
                    this.Opacity = 0.3;
                }
            }
        }

    }
}
