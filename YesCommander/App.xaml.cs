using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YesCommander.Classes;
using YesCommander.CustomControls;

namespace YesCommander
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup( object sender, StartupEventArgs e )
        {
            //Thread newWindowThread = new Thread( this.RunSplashWindow );
            MainWindow mainWindow = null;
            //newWindowThread.SetApartmentState( ApartmentState.STA );
            //newWindowThread.IsBackground = true;
            //newWindowThread.Start();

            //Create main window and show it
            mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            mainWindow.Show();

            //Close the splash window.
            //if ( this.splashWindow != null )
            //{
            //    this.splashWindow.Dispatcher.BeginInvoke( DispatcherPriority.Normal, (Action)( () =>
            //    { this.splashWindow.Close(); } ) );
            //}
        }

        private void Application_DispatcherUnhandledException( object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e )
        {
            Exceptions advArchException = e.Exception as Exceptions;
            if ( advArchException != null && advArchException.MessagePacket != null )
            {
                ShowExceptionDialog( advArchException.MessagePacket );
            }
            else
            {
                ShowExceptionDialog( e.Exception.Message, e.Exception.StackTrace );
            }
            e.Handled = true;
        }

        /// <summary>
        /// ShowExceptionDialog
        /// Displays a message to the user
        /// </summary>
        /// <param name="message"></param>
        public static void ShowExceptionDialog( MessagePacket message )
        {
            ExceptionDialog exceptionDialog = new ExceptionDialog();
            exceptionDialog.Init( message );
            exceptionDialog.Topmost = true;
            exceptionDialog.Show();
        }
        /// <summary>
        /// ShowExceptionDialog
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="summary">Short summary of the error.</param>
        /// <param name="detail">Detailed explanation of the error.</param>
        public static void ShowExceptionDialog( string summary, string detail )
        {
            if ( summary == null || summary.Length == 0 )
            {
                if ( summary == null )
                {
                    summary = "null";
                }
                MessagePacket invalidMessage = new MessagePacket();
                invalidMessage.Title = "Invalid Argument Exception.";
                invalidMessage.Summary = "The {0} method was called with an invalid argument.";
                invalidMessage.Detail = "The value [{0}] was passed in for the parameter [{1}].";
                invalidMessage.Severity = MessagePacket.ExceptionSeverity.Error;
                invalidMessage.AddSummaryParameter( "ShowExceptionDialog" );
                invalidMessage.AddDetailParameter( summary );
                invalidMessage.AddDetailParameter( "summary" );
                throw new Exceptions( invalidMessage );
            }
            if ( detail == null || detail.Length == 0 )
            {
                if ( detail == null )
                {
                    detail = "null";
                }
                MessagePacket invalidMessage = new MessagePacket();
                invalidMessage.Title = "Invalid Argument Exception.";
                invalidMessage.Summary = "The {0} method was called with an invalid argument.";
                invalidMessage.Detail = "The value [{0}] was passed in for the parameter [{1}].";
                invalidMessage.Severity = MessagePacket.ExceptionSeverity.Error;
                invalidMessage.AddSummaryParameter( "ShowExceptionDialog" );
                invalidMessage.AddDetailParameter( detail );
                invalidMessage.AddDetailParameter( "detail" );
                throw new Exceptions( invalidMessage );
            }
            ExceptionDialog exceptionDialog = new ExceptionDialog();
            exceptionDialog.Init( summary, detail );
            exceptionDialog.Show();
        }
    }
}
