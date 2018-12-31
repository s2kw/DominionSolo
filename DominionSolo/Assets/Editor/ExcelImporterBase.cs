/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


ExcelImporterBase.cs

Date:
Description:

-------------------------------------------------*/

using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace Tools { 
public class ExcelImporterBase
{
    public static T GetMasterOrigin<T>( string filePath )
        where T : ScriptableObject
    {
        var master = AssetDatabase.LoadAssetAtPath<T>( filePath );
        Debug.AssertFormat( master != null, filePath + " could not found." );
        return master;
    }

    protected static readonly string excelDir = "Assets/QuizeRPG/Excel";
    protected static IWorkbook OpenWorkBook( string[] importedAssets, string filePath )
    {
        // filePathのファイルが更新されていた場合
        if( !importedAssets.Any( path => path == filePath ) )
        {
            return null;
        }

        IWorkbook book = null;

        using( FileStream stream = File.Open( filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite ) )
        {
            if( Path.GetExtension( filePath ) == ".xls" )
            {
                book = new HSSFWorkbook( stream );
            }
            else
            {
                book = new XSSFWorkbook( stream );
            }
        }

        return book;
    }

        // 指定のパスからアセットをロードし、なければ作成する
        public static T CreateOrLoadData<T>( string path ) where T : ScriptableObject
        {
            var data = ( T )AssetDatabase.LoadAssetAtPath( path, typeof( T ) );
            if( data == null )
            {
                data = ScriptableObject.CreateInstance<T>();
                Debug.Assert( data != null );
                AssetDatabase.CreateAsset( ( ScriptableObject )data, path );
                //data.hideFlags = HideFlags.NotEditable;
            }
            return data;
        }

    // ISheetから有効なIRowのリストを返す
    protected static List<IRow> GetRowList( ISheet sheet )
    {
        var list = new List<IRow>();
        for( int y = 0; y <= sheet.LastRowNum; ++y )
        {
            var row = sheet.GetRow( y );

            var cell = row.GetCell( 0 );
            if( cell != null )
            {
                // 終端行
                if( cell.StringCellValue.IndexOf( "#end" ) == 0 ) break;

                // コメント行の解析
                if( cell.StringCellValue.IndexOf( "#" ) == 0 ) continue;
            }

            list.Add( row );
        }

        return list;
    }

    // タイトル行を検索してインデックスリストを作成する
    protected static List<int> GetDataIndexList( IRow row, string[] header )
    {
        var dataIndexList = new List<int>();

        foreach( var str in header )
        {
            dataIndexList.Add( row.Cells.First( c => c.CellType == CellType.String && c.StringCellValue == str ).ColumnIndex );
        }
        return dataIndexList;
    }

    // タイトル行を検索してインデックス連想配列を作成する
    protected static Dictionary<string, int> GetDataIndexDict( IRow row, string[] header )
    {
        var dataIndexDict = new Dictionary<string, int>();
        foreach( var str in header )
        {
            // Headerのセルが空欄か`#`だった場合は無視する
            if( string.IsNullOrEmpty( str ) || str.Equals( "#" ) ) continue;
            dataIndexDict.Add( str, row.Cells.First( c => c.StringCellValue == str ).ColumnIndex );
        }
        return dataIndexDict;
    }

    protected static void ImportError( string assetPath, string errorMsg )
    {
        bool ans = UnityEditor.EditorUtility.DisplayDialog( "エクセルファイルの読み込みエラー！", assetPath + "\n" + errorMsg, "エラーメッセージをクリックしたらどのファイルかがわかるよ！" );
        if( ans )
        {
            var target = AssetDatabase.LoadAssetAtPath<Object>( assetPath );
            Debug.LogError( "エクセルのインポートエラー！！！！！！このエラーをクリックしてエラーの出たエクセルファイルを確認！\t" + errorMsg, target );

            // コンソールウィンドウをアクティブにする
            var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();
            foreach( var w in windows )
            {
                var t = w.GetType();
                // アクセスしちゃいけないクラスなので明示的にクラス指定できないからこうするしか…
                if( string.Equals( t.Name, "ConsoleWindow" ) )
                {
                    EditorWindow.GetWindow( t );
                }
            }
        }
    }
}

    public static class IRowExtensions
    {

        public static double GetCellNumeric( this IRow source, int cellNum )
        {
            var cell = source.GetCell( cellNum );

            if( cell == null )
            {
                return 0;
            }
            else if( cell.CellType == CellType.Numeric )
            {
                return cell.NumericCellValue;
            }
            else if( cell.CellType == CellType.Formula )
            {
                try
                {
                    return cell.NumericCellValue;
                }
                catch
                {
                }
            }

            Debug.LogWarning( "セルの数値が取得できませんでした " + cell + "\t@ "+ cellNum );
            return 0;
        }

        public static int GetCellInt( this IRow source, int cellnum )
        {
            return ( int )GetCellNumeric( source, cellnum );
        }


        public static float GetCellFloat( this IRow source, int cellnum )
        {
            return ( float )GetCellNumeric( source, cellnum );
        }

        public static string GetCellString( this IRow source, int cellnum )
        {
            var cell = source.GetCell( cellnum );

            if( cell == null || cell.CellType == CellType.Blank )
            {
                return string.Empty;

            }
            else if( cell.CellType == CellType.String )
            {
                return cell.StringCellValue;
            }
            else if( cell.CellType == CellType.Numeric )
            {
                return cell.NumericCellValue.ToString();
            }
            else if( cell.CellType == CellType.Formula )
            {
                try
                {
                    return cell.StringCellValue;
                }
                catch
                {
                    try
                    {
                        return cell.NumericCellValue.ToString();
                    }
                    catch
                    {
                    }
                }
            }

            Debug.LogError( "セルの文字列が取得できませんでした " + cell.ToString() );
            return string.Empty;
        }
    }

} // namespace