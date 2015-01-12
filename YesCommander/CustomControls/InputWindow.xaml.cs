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
using System.Windows.Shapes;
using YesCommander.Classes;

namespace YesCommander.CustomControls
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Elysium.Controls.Window
    {
        public InputWindow()
        {
            InitializeComponent();
        }

        private void okButton_Click( object sender, RoutedEventArgs e )
        {
            ( this.Owner as MainWindow ).TableFollowers = LoadData.LoadFollowerString( this.InputText );

            if ( ( this.Owner as MainWindow ).TableFollowers != null && ( this.Owner as MainWindow ).TableFollowers.Columns.Count == 22 )            
                this.DialogResult = true;
            else
                MessageBox.Show( "数据不正确，请使用压缩包内的FollowerExport插件导出的数据。", "error", MessageBoxButton.OK, MessageBoxImage.Warning );
        }

        private void inputTextBox_TextChanged( object sender, TextChangedEventArgs e )
        {
            this.okButton.IsEnabled = string.IsNullOrWhiteSpace( this.InputText ) ? false : true;
        }

        private string InputText
        {
            get { return this.inputTextBox.Text.Trim(); }
        }
    }
}
