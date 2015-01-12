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
    /// Interaction logic for TinyImageToolTip.xaml
    /// </summary>
    public partial class TinyImageToolTip : UserControl
    {
        public TinyImageToolTip( Follower.Abilities ability )
        {
            InitializeComponent();
            this.SetUp( ability );
        }

        public TinyImageToolTip( Follower.Traits trait )
        {
            InitializeComponent();
            this.SetUp( trait );
        }

        public TinyImageToolTip( Follower.Traits trait, bool isEnvirment )
        {
            InitializeComponent();
            this.SetUp( trait, isEnvirment );
        }

        public void SetUp( Follower.Abilities ability )
        {
            this.image.Source = Globals.AbilityImageSource[ ability ]; //Follower.GetImageFromAbility( ability );
            this.abilityName.Text = Follower.AbilityToChinese( ability );
        }

        public void SetUp( Follower.Traits trait, bool isEnvirment = false )
        {
            this.image.Source = Globals.TraitImageSource[ trait ]; //Follower.GetImageFromFromTrait( trait );
            if ( !isEnvirment )
                this.abilityName.Text = Follower.TraitToChinese( trait );
            else
                this.abilityName.Text = Follower.EnvirementToChinese( trait );
            if ( !isEnvirment )
            {
                this.description.Text = Follower.TraitDescriptionToChinese( trait );
                this.description.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void ClearUp()
        {
            this.image.Source = null;
            this.abilityName.Text = string.Empty;
            this.description.Text = string.Empty;
            this.description.Visibility = System.Windows.Visibility.Collapsed;
        }

    }
}
