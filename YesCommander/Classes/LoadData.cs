using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Microsoft.Win32;

namespace YesCommander.Classes
{
    public class LoadData
    {
        public static DataTable LoadFollowerFile( string filePath )
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream( filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read );
            StreamReader sr = new StreamReader( fs, Encoding.Default );
            string strLine = "";
            string[] aryLine = null;
            string[] tableHead = null;
            int columnCount = 0;
            bool IsFirst = true;
            while ( ( strLine = sr.ReadLine() ) != null )
            {
                if ( IsFirst == true )
                {
                    tableHead = strLine.Split( ',' );
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    for ( int i = 0; i < columnCount; i++ )
                    {
                        DataColumn dc = new DataColumn( tableHead[ i ] );
                        dt.Columns.Add( dc );
                    }
                }
                else
                {
                    aryLine = strLine.Split( ',' );
                    DataRow dr = dt.NewRow();
                    for ( int j = 0; j < columnCount; j++ )
                    {
                        if ( j >= aryLine.Count() )
                            dr[ j ] = "";
                        else
                            dr[ j ] = aryLine[ j ];
                    }
                    dt.Rows.Add( dr );
                }
            }
            if ( aryLine != null && aryLine.Length > 0 )
            {
                dt.DefaultView.Sort = tableHead[ 0 ] + " " + "asc";
            }
            sr.Close();
            fs.Close();
            return dt;
        }

        public static DataTable LoadFollowerString( string str )
        {
            DataTable dt = new DataTable();
            string[] lines = str.Split( '\n' );
            string strLine = "";
            string[] aryLine = null;
            string[] tableHead = null;
            int columnCount = 0;
            bool IsFirst = true;
            int linesCount = 0;
            while ( linesCount < lines.Length )
            {
                strLine = lines[ linesCount ];
                linesCount++;
                if ( IsFirst == true )
                {
                    tableHead = strLine.Split( ',' );
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    for ( int i = 0; i < columnCount; i++ )
                    {
                        DataColumn dc = new DataColumn( tableHead[ i ] );
                        dt.Columns.Add( dc );
                    }
                }
                else
                {
                    aryLine = strLine.Split( ',' );
                    DataRow dr = dt.NewRow();
                    for ( int j = 0; j < columnCount; j++ )
                    {
                        if ( j >= aryLine.Count() )
                            dr[ j ] = "";
                        else
                            dr[ j ] = aryLine[ j ];
                    }
                    dt.Rows.Add( dr );
                }
            }

            if ( aryLine != null && aryLine.Length > 0 )
            {
                dt.DefaultView.Sort = tableHead[ 0 ] + " " + "asc";
            }
            return dt;
        }

        public static DataTable LoadMissionFile( string filePath )
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream( filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read );
            StreamReader sr = new StreamReader( fs, Encoding.Unicode );
            string strLine = "";
            string[] aryLine = null;
            string[] tableHead = null;
            int columnCount = 0;
            bool IsFirst = true;
            while ( ( strLine = sr.ReadLine() ) != null )
            {
                if ( IsFirst == true )
                {
                    tableHead = strLine.Split( '\t' );
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    for ( int i = 0; i < columnCount; i++ )
                    {
                        DataColumn dc = new DataColumn( tableHead[ i ] );
                        dt.Columns.Add( dc );
                    }
                }
                else
                {
                    aryLine = strLine.Split( '\t' );
                    DataRow dr = dt.NewRow();
                    for ( int j = 0; j < columnCount; j++ )
                    {
                        if ( j >= aryLine.Count() )
                            dr[ j ] = "";
                        else
                            dr[ j ] = aryLine[ j ];
                    }
                    dt.Rows.Add( dr );
                }
            }
            if ( aryLine != null && aryLine.Length > 0 )
            {
                dt.DefaultView.Sort = tableHead[ 0 ] + " " + "asc";
            }
            sr.Close();
            fs.Close();
            return dt;
        }

        public static void OpenFile( ref DataTable table )
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Txt文件|*.txt|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;
                if ( openFileDialog.ShowDialog() == true )
                {
                    table = LoadData.LoadFollowerFile( openFileDialog.FileName );
                }
            }
            catch
            {
                throw;
            }
        }

        public static void GenerateRaceNameTable( DataTable table, string fileStr )
        {
            FileStream fs = new FileStream( fileStr, System.IO.FileMode.Open, System.IO.FileAccess.Read );
            StreamReader sr = new StreamReader( fs, Encoding.Unicode );
            string strLine = "";
            string[] aryLine = null;
            string[] tableHead = null;
            int columnCount = 0;
            bool IsFirst = true;
            while ( ( strLine = sr.ReadLine() ) != null )
            {
                if ( string.IsNullOrWhiteSpace( strLine ) )
                    continue;
                if ( IsFirst == true )
                {
                    tableHead = strLine.Split( '\t' );
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    for ( int i = 0; i < columnCount; i++ )
                    {
                        DataColumn dc = new DataColumn( tableHead[ i ] );
                        table.Columns.Add( dc );
                    }
                }
                else
                {
                    aryLine = strLine.Split( '\t' );
                    DataRow dr = table.NewRow();
                    int index = 0;
                    for ( int j = 0; j < columnCount; j++ )
                    {
                        while ( string.IsNullOrWhiteSpace( aryLine[ index ] ) )
                        {
                            index++;
                            if ( aryLine.Length <= index )
                                break;
                        }
                        if ( aryLine.Length <= index )
                            break;
                        dr[ j ] = aryLine[ index ];
                        index++;
                        if ( aryLine.Length <= index )
                            break;
                    }
                    table.Rows.Add( dr );
                }
            }
            if ( aryLine != null && aryLine.Length > 0 )
            {
                table.DefaultView.Sort = tableHead[ 0 ] + " " + "asc";
            }
            sr.Close();
            fs.Close();
        }
    }
}
