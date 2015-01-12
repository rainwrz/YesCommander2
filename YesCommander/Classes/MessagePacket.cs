using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YesCommander.Classes
{
    public class MessagePacket
    {
        #region MEMBER VARIABLES

        private string assemblyName;
        //private string						resourceNamePrefix;  //TODO DELETE THIS
        private string className;
        private string detailMessage;
        private StringBuilder detailParameters;
        private string id;
        private string methodName;
        //private Assembly modelAssembly;  //TODO DELETE THIS
        private ExceptionSeverity severity;
        private string summaryMessage;
        private StringBuilder summaryParameters;
        private StringBuilder thrownBy;
        private string title;

        public enum ExceptionSeverity
        //protected internal enum ExceptionSeverity
        {
            /// <summary></summary>
            Critical,
            /// <summary></summary>
            Error,
            /// <summary></summary>
            Informational,
            /// <summary></summary>
            Warning
        }

        #endregion	//	MEMBER VARIABLES

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MessagePacket"/> class.
        /// </summary>
        public MessagePacket()
        {
            Clear();
        }
        #endregion	//	CONSTRUCTOR

        #region METHODS

        /// <summary>
        /// Sets the message value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="id">The id.</param>
        /// <param name="severity">The severity.</param>
        public void SetMessageValue( System.Type type, string methodName, string id, ExceptionSeverity severity )
        {
            this.assemblyName = type.Assembly.FullName;
            this.className = type.Name;
            this.severity = severity;
            this.methodName = methodName;
            this.id = id;
        }

        /// <summary>
        /// AddDetailParameter
        /// Adds a parameter to the exception's detail parameter list
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void AddDetailParameter( string parameter )
        {
            if ( parameter == null )
            {
                throw new ArgumentNullException( "parameter" );
            }
            //	If the parameter is an empty string, we want to make sure it
            //	doesn't get trimmed or left off when the string is split, so add "" around it.
            if ( parameter.Length == 0 )
            {
                this.detailParameters.Append( "\"\"" );
            }
            else
            {
                this.detailParameters.Append( parameter );
            }
            this.detailParameters.Append( '\t' );
        }

        /// <summary>
        /// AddSummaryParameter
        /// Adds a parameter to the exception's summary parameter list
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void AddSummaryParameter( string parameter )
        {
            this.summaryParameters.Append( parameter );
            this.summaryParameters.Append( '\t' );
        }

        /// <summary>
        /// Clear
        /// initializes or reinitializes all member variables.
        /// </summary>
        public void Clear()
        {
            this.assemblyName = String.Empty;
            //this.resourceNamePrefix = "HighJump.AdvantagePlatform.Tools.AdvArch.Model.Core.";  //TODO DELETE THIS
            this.className = String.Empty;
            this.detailMessage = String.Empty;
            this.detailParameters = new StringBuilder();
            this.id = String.Empty;
            this.methodName = String.Empty;
            //this.modelAssembly = Assembly.GetAssembly( typeof( MessagePacket ) );  //TODO DELETE THIS
            this.severity = ExceptionSeverity.Informational;
            this.summaryMessage = String.Empty;
            this.summaryParameters = new StringBuilder();
            this.thrownBy = new StringBuilder();
        }

        /// <summary>
        /// GetDetailMessage
        /// </summary>
        /// <param name="detailMsg">Detail message string.</param>
        /// <returns>Detail error message with parameters added</returns>
        public string GetDetailMessage( string detailMsg )
        {
            if ( this.DetailParameters != null )
            {
                if ( this.DetailParameters.Trim().Length > 0 )
                {
                    this.detailMessage = this.InsertParameters( this.detailMessage, this.DetailParameters );
                }
            }
            if ( ( this.detailMessage != null ) && ( this.detailMessage.Trim().Length > 0 ) )
            {
                this.BuildThrownByString();
                if ( this.thrownBy != null )
                {
                    if ( this.thrownBy.ToString().Trim().Length > 0 )
                    {
                        //if ( this.detailMessage != null )
                        //{
                        //if ( this.detailMessage.Trim().Length > 0 )
                        this.detailMessage = this.detailMessage + System.Environment.NewLine + this.thrownBy.ToString();
                        //else
                        //    this.detailMessage = this.thrownBy.ToString();
                        //}
                    }
                }
            }
            return this.detailMessage;
        }

        /// <summary>
        /// GetSummaryMessage
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>A summary error message</returns>
        public string GetSummaryMessage( string culture )
        {
            if ( this.summaryMessage.Length > 0 )
            {
                //if ( this.SummaryParameters.Trim().Length > 0 )
                //{
                this.summaryMessage = this.InsertParameters( this.summaryMessage, this.SummaryParameters );
                //}
            }
            return this.summaryMessage;
        }

        /// <summary>
        /// BuildThrownByString
        /// The 'thrown by' string has the assembly, class, and method that threw the exception.
        /// </summary>
        private void BuildThrownByString()
        {
            //// Pull Assembly, Class and Method names out of the exception
            //this.thrownBy.Append( resourceManager.GetString( "Exception thrown by" ) + " [ " );

            if ( ( this.assemblyName.Trim().Length > 0 ) ||
                ( this.className.Trim().Length > 0 ) ||
                ( this.methodName.Trim().Length > 0 ) )
            {
                this.thrownBy.Append( "[" );

                if ( ( this.assemblyName != null ) && ( this.assemblyName.Trim().Length > 0 ) )
                    this.thrownBy.Append( this.assemblyName.Trim() + "." );

                if ( ( this.className != null ) && ( this.className.Trim().Length > 0 ) )
                    this.thrownBy.Append( this.className.Trim() + "." );

                if ( ( this.methodName != null ) && ( this.methodName.Trim().Length > 0 ) )
                    this.thrownBy.Append( this.methodName.Trim() );

                this.thrownBy.Append( "]." );
            }
        }

        /// <summary>
        /// GetTitle
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>
        /// Title to display in an exception dialog.
        /// If no title is found in resources, Advantage Architect Error is returned.
        /// </returns>
        public string GetTitle( string culture )
        {
            if ( this.title == null || this.title.Length == 0 )
            {
                return "D3Simulator Exception";
            }
            else
            {
                return this.title;
            }
        }

        /// <summary>
        /// InsertParameters
        /// Takes a message containing place holders for parameters
        /// (Placeholders are of the format {#}, e.g. {0} or {1}.)
        /// Splits the parameters string and replaces the
        /// placeholders with the parameters.
        /// </summary>
        /// <param name="message">String containing text and placeholders for parameters. Placeholders are of the format {#}, e.g. {0} or {1}.</param>
        /// <param name="parameters">A tab delimited list of strings.</param>
        /// <returns></returns>
        private string InsertParameters( string message, string parameters )
        {
            if ( message == null )
            {
                throw new ArgumentNullException( "message" );
            }
            if ( parameters == null )
            {
                throw new ArgumentNullException( "parameters" );
            }
            string returnValue = message;
            string[] parameterList;
            if ( message.Contains( "{0}" ) == true )
            {
                parameterList = parameters.Split( '\t' );
                returnValue = string.Format( CultureInfo.InvariantCulture, message, parameterList );
            }
            return returnValue;
        }

        #endregion	//	METHODS

        #region PROPERTIES
        /// <summary>
        /// AssemblyName
        /// Gets or sets the name of the assembly
        /// from which the exception was thrown
        /// </summary>
        /// <value>The name of the assembly.</value>
        public string AssemblyName
        {
            get { return this.assemblyName; }
            set { this.assemblyName = value; }
        }

        /// <summary>
        /// ClassName
        /// Gets or sets the name of the class
        /// from which the exception was thrown
        /// </summary>
        /// <value>The name of the class.</value>
        public string ClassName
        {
            get { return this.className; }
            set { this.className = value; }
        }

        /// <summary>
        /// DetailParameters
        /// is a tab delimited string of values to be inserted
        /// into the exception's detail message.
        /// </summary>
        /// <value>The detail parameters.</value>
        public string DetailParameters
        {
            get { return this.detailParameters.ToString().TrimEnd( new char[] { '\t' } ); }
        }

        /// <summary>
        /// Id
        /// Gets or sets the id used to find
        /// resource strings
        /// </summary>
        /// <value>The id.</value>
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// MethodName
        /// Gets or sets the name of the method that threw the exception
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName
        {
            get { return this.methodName; }
            set { this.methodName = value; }
        }

        ///// <summary>
        ///// ModelAssembly
        ///// Gets the Model Assembly
        ///// (used when creating a ResourceManager)
        ///// </summary>
        ///// <value>The model assembly.</value>
        //public Assembly ModelAssembly
        //{
        //    //TODO DELETE THIS
        //    get { return this.modelAssembly; }
        //}

        /// <summary>
        /// Severity
        /// Gets or sets the exception's severity level - Globals.ExceptionSeverity
        /// </summary>
        /// <value>The severity.</value>
        public ExceptionSeverity Severity
        {
            get { return this.severity; }
            set { this.severity = value; }
        }
        /// <summary>
        /// SummaryParameters
        /// is a tab delimited string of values to be inserted
        /// into the exception's summary message.
        /// </summary>
        /// <value>The summary parameters.</value>
        public string SummaryParameters
        {
            get { return this.summaryParameters.ToString().TrimEnd( new char[] { '\t' } ); }
        }

        /// <summary>
        /// </summary>
        public string Summary
        {
            set { this.summaryMessage = value; }
            get { return this.summaryMessage; }
        }

        /// <summary>
        /// </summary>
        public string Detail
        {
            set { this.detailMessage = value; }
            get { return this.detailMessage; }
        }

        /// <summary>
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
        #endregion	//	PROPERTIES
    }
}
