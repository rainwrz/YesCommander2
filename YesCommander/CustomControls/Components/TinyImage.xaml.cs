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
    /// Interaction logic for TinyImage.xaml
    /// </summary>
    public partial class TinyImage : UserControl
    {
        public TinyImage()
        {
            InitializeComponent();
        }

        public void SetUp( Follower.Abilities ability )
        {
            this.image.Source = Globals.AbilityImageSource[ ability ];
            this.ToolTip = new TinyImageToolTip( ability );
            ToolTipService.SetInitialShowDelay( this, 0 );
        }

        public void SetUp( Follower.Traits trait )
        {
            this.image.Source = Globals.TraitImageSource[ trait ];
            this.ToolTip = new TinyImageToolTip( trait );
            ToolTipService.SetInitialShowDelay( this, 0 );
        }

        public void SetUp( Follower.Traits trait, bool isEnvirment )
        {
            this.image.Source = Globals.TraitImageSource[ trait ];// Follower.GetImageFromFromTrait( trait );
            this.ToolTip = new TinyImageToolTip( trait, isEnvirment );
            ToolTipService.SetInitialShowDelay( this, 0 );
        }

        public void ClearUp()
        {
            this.image.Source = null;
            this.ToolTip = null;
        }
    }
}
