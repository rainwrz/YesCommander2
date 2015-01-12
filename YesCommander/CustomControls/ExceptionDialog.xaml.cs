using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for ExceptionDialog.xaml
    /// </summary>
    public partial class ExceptionDialog : Window
    {
        #region Variables
        private bool isResizing = false;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionDialog()
        {
            InitializeComponent();
            this.InitOwnedWindow();
        }

        #endregion

        #region Properties

        private static DependencyProperty exceptionMessageProperty = DependencyProperty.Register( "ExceptionMessage", typeof( string ), typeof( ExceptionDialog ) );
        private static DependencyProperty exceptionDetailsProperty = DependencyProperty.Register( "ExceptionDetail", typeof( string ), typeof( ExceptionDialog ) );
        private static DependencyProperty dialogTitleProperty = DependencyProperty.Register( "DialogTitle", typeof( string ), typeof( ExceptionDialog ) );
        private static DependencyProperty dialogImageSourceProperty = DependencyProperty.Register( "DialogImageSource", typeof( ImageSource ), typeof( ExceptionDialog ) );
        private static DependencyProperty expanderTitleProperty = DependencyProperty.Register( "ExpanderTitle", typeof( string ), typeof( ExceptionDialog ) );
        private static DependencyProperty expanderHeaderVisibilityProperty = DependencyProperty.Register( "ExpanderHeaderVisibility", typeof( Visibility ), typeof( ExceptionDialog ), new PropertyMetadata( Visibility.Visible ) );

        private string ExceptionMessage
        {
            get { return (string)GetValue( exceptionMessageProperty ); }
            set { SetValue( exceptionMessageProperty, value ); }
        }

        private string ExceptionDetail
        {
            get { return (string)GetValue( exceptionDetailsProperty ); }
            set { SetValue( exceptionDetailsProperty, value ); }
        }

        private string DialogTitle
        {
            get { return (string)GetValue( dialogTitleProperty ); }
            set { SetValue( dialogTitleProperty, value ); }
        }

        private ImageSource DialogImageSource
        {
            get { return (ImageSource)GetValue( dialogImageSourceProperty ); }
            set { SetValue( dialogImageSourceProperty, value ); }
        }

        private string ExpanderTitle
        {
            get { return (string)GetValue( expanderTitleProperty ); }
            set { SetValue( expanderTitleProperty, value ); }
        }

        private Visibility ExpanderHeaderVisibility
        {
            get { return (Visibility)GetValue( expanderHeaderVisibilityProperty ); }
            set { SetValue( expanderHeaderVisibilityProperty, value ); }
        }

        #endregion

        #region Events

        private void ExceptionDialog_Loaded( object sender, RoutedEventArgs e )
        {
            Button okButton = this.expander.Template.FindName( "PART_OKButton", this.expander ) as Button;
            if ( okButton != null )
            {
                okButton.Focus();
                okButton.Click += new RoutedEventHandler( buttonOK_Click );
            }
            this.MinHeight = this.ActualHeight;
            this.expander.Expanded += expander_Expanded;
            this.expander.Collapsed += expander_Collapsed;
        }

        private void buttonOK_Click( object sender, RoutedEventArgs e )
        {
            this.DialogResult = true;
        }

        private void expander_Expanded( object sender, RoutedEventArgs e )
        {
            this.ExpanderTitle = "Hide Details";
            this.expander.Margin = new Thickness( 10d, 0d, 10d, 10d );
            if ( this.SizeToContent == SizeToContent.Height )
            {
                this.SizeToContent = SizeToContent.Manual;
                this.Height = this.MinHeight + 200;
            }
            else
            {
                if ( this.detailPanel.ActualHeight == 0.0d && !this.isResizing )
                {
                    this.Height = this.MinHeight + 200;
                }
            }
        }

        private void expander_Collapsed( object sender, RoutedEventArgs e )
        {
            this.ExpanderTitle = "Show Details";
            this.expander.Margin = new Thickness( 10d, 0d, 10d, 0d );

            if ( this.WindowState == WindowState.Maximized )
                this.WindowState = WindowState.Normal;

            this.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Render,
                new Action( () => this.SizeToContent = SizeToContent.Height ) );
        }

        private void Window_SizeChanged( object sender, SizeChangedEventArgs e )
        {
            if ( !this.IsLoaded )
                return;

            if ( this.isResizing )
                return;

            if ( this.ActualHeight > this.MinHeight )
            {
                this.isResizing = true;
                this.expander.IsExpanded = true;
                if ( !double.IsNaN( this.detailPanel.Height ) )
                    this.detailPanel.Height = double.NaN;
                this.isResizing = false;
            }
            else if ( this.ActualHeight <= this.MinHeight )
            {
                this.isResizing = true;
                this.expander.IsExpanded = false;
                this.isResizing = false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set the owned Window for the showing instance.
        /// </summary>
        private void InitOwnedWindow()
        {
            Window activeWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault( window => window.IsActive );
            if ( activeWindow == null )
            {
                if ( Application.Current.MainWindow.IsVisible )
                {
                    this.Owner = Application.Current.MainWindow;
                }
            }
            else
            {
                this.Owner = activeWindow;
            }
        }

        /// <summary>
        /// AssignSeverityImage
        /// Determines which icon should be displayed on the exception dialog
        /// </summary>
        /// <param name="severity">The severity.</param>
        private void AssignSeverityImage( MessagePacket.ExceptionSeverity severity )
        {
            switch ( severity )
            {
                case MessagePacket.ExceptionSeverity.Critical:
                case MessagePacket.ExceptionSeverity.Error:
                    this.DialogImageSource = new BitmapImage( new Uri( "../Resources/Dialogs_Error.png", UriKind.Relative ) );
                    break;
                case MessagePacket.ExceptionSeverity.Warning:
                    this.DialogImageSource = new BitmapImage( new Uri( "../Resources/Dialogs_Warning.png", UriKind.Relative ) );
                    break;
                case MessagePacket.ExceptionSeverity.Informational:
                default:
                    this.DialogImageSource = new BitmapImage( new Uri( "../Resources/Dialogs_Infomation.png", UriKind.Relative ) );
                    break;
            }
        }

        /// <summary>
        /// Inits the specified error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public void Init( string errorMessage )
        {
            if ( string.IsNullOrEmpty( errorMessage ) )
            {
                this.ShowExceptionDetail( false );
                return;
            }

            this.AssignSeverityImage( MessagePacket.ExceptionSeverity.Error );

            this.DialogTitle = "YesCommander Exception";

            int lineBreakIndex = errorMessage.IndexOf( System.Environment.NewLine, 0, StringComparison.Ordinal );
            if ( lineBreakIndex > 0 )
            {
                this.ExceptionMessage = errorMessage.Substring( 0, lineBreakIndex );
                this.ExceptionDetail = errorMessage.Substring( lineBreakIndex + 2, ( errorMessage.Length - ( lineBreakIndex + 2 ) ) );
                this.ShowExceptionDetail( true );
            }
            else
            {
                if ( errorMessage.Length < 120 )
                {
                    this.ExceptionMessage = errorMessage;
                    this.ShowExceptionDetail( false );
                }
                else
                {
                    this.ExceptionMessage = "请点击下方按钮以显示更多内容。";
                    this.ExceptionDetail = errorMessage;
                    this.ShowExceptionDetail( true );
                }
            }
            this.DataContext = this;
        }

        /// <summary>
        /// Inits the specified error summary.
        /// </summary>
        /// <param name="errorSummary">The error summary.</param>
        /// <param name="errorDetail">The error detail.</param>
        public void Init( string errorSummary, string errorDetail )
        {
            this.DialogTitle = "YesCommander Exception";

            this.AssignSeverityImage( MessagePacket.ExceptionSeverity.Error );

            if ( errorSummary.Trim().Length > 0 && errorSummary.Trim().Length < 120 )
            {
                this.ExceptionMessage = errorSummary.Trim();
                this.ExceptionDetail = errorDetail.Trim();
            }
            else
            {
                if ( errorSummary.Trim().Length == 0 )
                {
                    this.ExceptionMessage = "请点击下方按钮以显示更多内容。";
                    this.ExceptionDetail = errorDetail;
                }
                else
                {
                    this.ExceptionMessage = "请点击下方按钮以显示更多内容。";
                    this.ExceptionDetail = errorSummary + System.Environment.NewLine + errorDetail;
                }
            }
            this.ShowExceptionDetail( true );

            this.DataContext = this;
        }

        /// <summary>
        /// Inits the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Init( Exceptions exception )
        {
            if ( exception == null )
            {
                return;
            }

            this.AssignSeverityImage( exception.MessagePacket.Severity );

            this.DialogTitle = exception.MessagePacket.GetTitle( CultureInfo.CurrentUICulture.ToString() );

            // Set the summary text
            this.ExceptionMessage = exception.MessagePacket.GetSummaryMessage( CultureInfo.CurrentUICulture.ToString() );

            //Set the detail text
            StringBuilder detailMessage = new StringBuilder();
            detailMessage.Append( exception.MessagePacket.GetDetailMessage( CultureInfo.CurrentUICulture.ToString() ) );
            if ( exception.InnerException != null )
            {
                detailMessage.Append( System.Environment.NewLine );
                detailMessage.Append( "Inner Exception" );
                detailMessage.Append( System.Environment.NewLine );
                detailMessage.Append( exception.InnerException.Message );
            }

            if ( detailMessage.ToString().Length > 0 )
            {
                this.ExceptionDetail = detailMessage.ToString();
                this.ShowExceptionDetail( true );
            }
            else
            {
                this.ShowExceptionDetail( false );
            }

            this.DataContext = this;
        }

        /// <summary>
        /// Inits the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Init( MessagePacket message )
        {
            if ( message == null )
            {
                return;
            }

            this.AssignSeverityImage( message.Severity );

            // Set the title text
            this.DialogTitle = message.GetTitle( CultureInfo.CurrentUICulture.ToString() );

            // Set the message text
            this.ExceptionMessage = message.GetSummaryMessage( CultureInfo.CurrentUICulture.ToString() );

            //Set the detail text
            StringBuilder detailMessage = new StringBuilder();
            detailMessage.Append( message.GetDetailMessage( CultureInfo.CurrentUICulture.ToString() ) );

            if ( detailMessage.ToString().Length > 0 )
            {
                this.ExceptionDetail = detailMessage.ToString();
                this.ShowExceptionDetail( true );
            }
            else
            {
                this.ShowExceptionDetail( false );
            }

            this.DataContext = this;
        }

        private void ShowExceptionDetail( bool isShowExceptionDetail )
        {
            if ( isShowExceptionDetail )
            {
                this.expander.Margin = new Thickness( 10d, 0d, 10d, 0d );
                this.ExpanderTitle = "Show Details";
                this.ResizeMode = ResizeMode.CanResizeWithGrip;

            }
            else
            {
                this.detailText.Visibility = Visibility.Collapsed;
                this.MinWidth = 350d;
                this.MaxWidth = 530d;
                this.ResizeMode = ResizeMode.NoResize;
                this.SizeToContent = SizeToContent = SizeToContent.WidthAndHeight;

                this.ExpanderHeaderVisibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Implement it as ShowDialog method for the exception dialog could only shown as a dialog window.
        /// </summary>
        public new void Show()
        {
            this.ShowDialog();
        }
        #endregion
    }
}
