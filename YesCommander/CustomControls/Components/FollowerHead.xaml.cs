﻿using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for FollowerHead.xaml
    /// </summary>
    public partial class FollowerHead : UserControl
    {
        public FollowerHead()
        {
            InitializeComponent();
        }

        //public void Populate( Follower follower, string nameEn )
        //{
        //    this.head.Source = Follower.GetImageFromPicName( "ImagesSmall/" + nameEn.Replace( '"', '#' ).Replace( ' ', '_' ) + ".png" );
        //    this.head.ToolTip = new FollowerPanel( follower, nameEn );
        //    ToolTipService.SetShowDuration( this.head, 60000 );
        //    ToolTipService.SetInitialShowDelay( this.head, 0 );
        //}

        public void PopulateFullImage( string nameEn )
        {
            this.PushImage( nameEn );
            string bigPath = "Images/" + ( Globals.IsAlliance ? "Ali/" : "Hrd/" );
            bigPath += this.GetPicName( nameEn );
            if ( File.Exists( bigPath ) )
            {
                this.head.ToolTip = new FollowerImageFull( bigPath );
                ToolTipService.SetShowDuration( this.head, 60000 );
                ToolTipService.SetInitialShowDelay( this.head, 0 );
            }
        }

        public void PopulateFullImage( string nameEn, bool isAli )
        {
            this.PushImage( nameEn, isAli );
            string bigPath = "Images/" + ( isAli ? "Ali/" : "Hrd/" );
            bigPath += this.GetPicName( nameEn );
            if ( File.Exists( bigPath ) )
            {
                this.head.ToolTip = new FollowerImageFull( bigPath );
                ToolTipService.SetShowDuration( this.head, 60000 );
                ToolTipService.SetInitialShowDelay( this.head, 0 );
            }
        }

        private void PushImage( string nameEn )
        {
            string path = "Images/Small/" + ( Globals.IsAlliance ? "Ali/" : "Hrd/" );
            nameEn = this.GetPicName( nameEn );
            this.head.Source = Globals.GetFollowerHead( path + nameEn );
        }

        private void PushImage( string nameEn, bool isAli )
        {
            string path = "Images/Small/" + ( isAli ? "Ali/" : "Hrd/" );
            nameEn = this.GetPicName( nameEn );
            this.head.Source = Globals.GetFollowerHead( path + nameEn );
        }

        private string GetPicName( string originalName )
        {
            return originalName.Contains( "Schweitzer" ) ? "Doc_Schweitzer.png" :
                originalName.Contains( "Steelpaw" ) ? "Suna_Sunnie_Steelpaw.png" :
                originalName.Contains( "the Fox" ) ? "Claire_the_Fox.png" :
                   originalName.Replace( ' ', '_' ) + ".png";
        }

        public void Clear()
        {
            this.head.Source = null;
            this.head.ToolTip = null;
        }
    }
}
