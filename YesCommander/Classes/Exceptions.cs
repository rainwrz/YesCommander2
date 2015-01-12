using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Globalization;

namespace YesCommander.Classes
{
        [Serializable()]
    public class Exceptions : Exception, ISerializable
    {
        #region MEMBER VARIABLES
        private MessagePacket messagePacket;
        private string id;
        #endregion

        #region CONSTRUCTORS

        /// <summary></summary>
        /// <param name="messagePacket"></param>
        public Exceptions( MessagePacket messagePacket )
            : base( ExtractSummaryMessage( messagePacket ) )
        {
            if ( messagePacket == null )
                throw new ArgumentNullException( "messagePacket" );

            this.InitializeComponents();
            this.messagePacket = messagePacket;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ArchitectException"/> class.
        /// </summary>
        /// <param name="messagePacket">The message packet.</param>
        /// <param name="innerException">The inner exception.</param>
        public Exceptions( MessagePacket messagePacket, Exceptions innerException )
            : base( ExtractSummaryMessage( innerException ), innerException )
        {
            if ( messagePacket == null )
                throw new ArgumentNullException( "messagePacket" );

            if ( innerException == null )
                throw new ArgumentNullException( "innerException" );

            this.InitializeComponents();
            this.messagePacket = messagePacket;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ArchitectException"/> class.
        /// </summary>
        public Exceptions()
            : base( buildMessage( "" ) )
        {
            this.InitializeComponents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ArchitectException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public Exceptions( string message )
            : base( buildMessage( message ) )
        {
            if ( message == null )
                throw new ArgumentNullException( "message" );

            this.InitializeComponents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ArchitectException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public Exceptions( string message, Exception innerException )
            : base( buildMessage( message ), innerException )
        {
            if ( message == null )
                throw new ArgumentNullException( "message" );

            if ( innerException == null )
                throw new ArgumentNullException( "innerException" );

            this.InitializeComponents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ArchitectException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"></see> is zero (0). </exception>
        /// <exception cref="T:System.ArgumentNullException">The info parameter is null. </exception>
        protected Exceptions( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
            if ( info == null )
                throw new ArgumentNullException( "info" );

            this.InitializeComponents();
            this.messagePacket.AssemblyName = info.GetString( "AssemblyName" );
            this.messagePacket.ClassName = info.GetString( "ClassName" );
            this.messagePacket.AddDetailParameter( info.GetString( "DetailParameters" ) );
            this.messagePacket.MethodName = info.GetString( "MethodName" );
            this.messagePacket.Severity = (MessagePacket.ExceptionSeverity)info.GetValue( "Severity", typeof( MessagePacket.ExceptionSeverity ) );
            this.messagePacket.AddSummaryParameter( info.GetString( "SummaryParameters" ) );
        }

        #endregion

        #region METHODS

        /// <summary></summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        private static string buildMessage( string detail )
        {
            if ( detail == null )
                throw new ArgumentNullException( "detail" );

            string returnValue;
            if ( detail.Length == 0 )
            {
                returnValue = "Exception";
            }
            else
            {
                returnValue = detail;
            }
            return returnValue;
        }

        /// <summary>Extracts the summary message.</summary>
        /// <param name="messagePacket"></param>
        /// <returns></returns>
        private static string ExtractSummaryMessage( MessagePacket messagePacket )
        {
            if ( messagePacket == null )
                throw new ArgumentNullException( "messagePacket" );

            return messagePacket.GetSummaryMessage( CultureInfo.CurrentUICulture.ToString() );
        }

        /// <summary>Extracts the summary message.</summary>
        /// <param name="innerException"></param>
        /// <returns></returns>
        private static string ExtractSummaryMessage( Exception innerException )
        {
            if ( innerException == null )
                throw new ArgumentNullException( "innerException" );

            return innerException.Message;
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> 
        /// with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The info parameter is a null reference (Nothing in Visual Basic). </exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/></PermissionSet>
        [SecurityPermissionAttribute( System.Security.Permissions.SecurityAction.Demand, SerializationFormatter = true )]
        public override void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            if ( info == null )
                throw new ArgumentNullException( "info" );

            info.AddValue( "AssemblyName", this.messagePacket.AssemblyName, typeof( string ) );
            info.AddValue( "ClassName", this.messagePacket.ClassName, typeof( string ) );
            info.AddValue( "DetailParameters", this.messagePacket.DetailParameters, typeof( string ) );
            //info.AddValue( "Id", this.messagePacket.Id, typeof( string ) );
            info.AddValue( "MethodName", this.messagePacket.MethodName, typeof( string ) );
            info.AddValue( "Severity", this.messagePacket.Severity, typeof( MessagePacket.ExceptionSeverity ) );
            info.AddValue( "SummaryParameters", this.messagePacket.SummaryParameters, typeof( string ) );

            base.GetObjectData( info, context );
        }

        /// <summary>
        /// InitializeComponents
        /// Sets default values for member variables
        /// </summary>
        private void InitializeComponents()
        {
            this.messagePacket = null;
        }

        #endregion

        #region PROPERTIES

        /// <summary> Gets the exception's id </summary>
        /// <value>The id.</value>
        public string Id
        {
            get { return this.messagePacket.Id; }
        }

        /// <summary>
        /// MessagePacket containing Summary and Detail Parameters
        /// </summary>
        /// <value>The message packet.</value>
        public MessagePacket MessagePacket
        {
            get { return this.messagePacket; }
            set { this.messagePacket = value; }
        }

        /// <summary> Gets or sets the error item id. </summary>
        public string ErrorItemId
        {
            get { return this.id; }
            set { this.id = value; }
        }

        #endregion
    }
}