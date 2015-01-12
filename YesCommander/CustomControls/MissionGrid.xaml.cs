using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for MissionGrid.xaml
    /// </summary>
    public partial class MissionGrid : Elysium.Controls.Window
    {
        public bool ReadyToClose = false;
        public MissionGrid( string title )
        {
            InitializeComponent();
            this.Title = title;
        }
        
        private void Window_Closing( object sender, System.ComponentModel.CancelEventArgs e )
        {
            if ( !this.ReadyToClose )
            {
                e.Cancel = true;
                this.Visibility = System.Windows.Visibility.Hidden;
                this.Owner.Activate();
            }
        }

        public void Populate(DataTable table)
        {
            this.missionGrid.DataContext = table;
        }
    }
}
