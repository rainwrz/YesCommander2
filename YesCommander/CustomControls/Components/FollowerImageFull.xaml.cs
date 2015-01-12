using System.Windows.Controls;
using YesCommander.Classes;

namespace YesCommander.CustomControls.Components
{
    /// <summary>
    /// Interaction logic for FollowerImageFull.xaml
    /// </summary>
    public partial class FollowerImageFull : UserControl
    {
        public FollowerImageFull( string path )
        {
            InitializeComponent();
            this.pic.Source = Globals.GetFollowerBody( path );
        }
    }
}
