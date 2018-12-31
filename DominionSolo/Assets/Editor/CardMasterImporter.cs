/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


CardMasterImporter.cs

Date:
Description:

-------------------------------------------------*/



using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
//using UniRx;
//using UniRx.Triggers;
//using UnityEngine.UI;

# if UNITY_EDITOR
using UnityEditor;
# endif

namespace Tools{
    public class CardMasterImporter : ExcelImporterBase
    {
        const string excelFilePath = "Assets/Editor/MasterData/CardMaster.xlsx";
        const string masterFilePath = "Assets/DominionSolo/Resources/MasterData/CardMaster.asset";
        const string sheetName = "Sheet1";
        [MenuItem("Assets/Import/CardMaster", false,0)]
        public static void Import()
        {
            foreach( var selected in Selection.objects )
            {
                var objPath = AssetDatabase.GetAssetPath( selected );
                OnPostprocessAllAssets( new string[] { objPath }, new string[] { }, new string[] { }, new string[] { } );
            }
        }
        // Assets更新処理
        static void OnPostprocessAllAssets( string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths )
        {
            Debug.Log( "Trying to open file : " + excelFilePath );
            var book = OpenWorkBook( importedAssets, excelFilePath );
            if( book == null )
            {
                Debug.LogError("File could not found:" + excelFilePath );
                return;
            }

            var data = CreateOrLoadData<CardMaster>( masterFilePath );
            data.dataList.Clear();

            var sheet = book.GetSheet( sheetName );
            if( sheet == null )
            {
                var msg = "[ExcelImporter] StageMaster not found sheet.";
                ImportError( excelFilePath, msg );
                return;
            }
            data.dataList = LoadData( sheet );
            EditorUtility.SetDirty( data );

            Debug.Log( "[ExcelImporter] StageMaster import complete." );
        }

        private static List<CardMaster.CardData> LoadData( ISheet sheet )
        {
            // データ
            var dataList = new List<CardMaster.CardData>();

            var rowList = GetRowList( sheet );

            // Header行
            var headerList = new List<string>();
            foreach( var h in rowList[0].ToList() )
            {
                Debug.Log(h.ToString());
                headerList.Add( h.ToString() );
            }
            var dataIndexDict = GetDataIndexDict( rowList[0], headerList.ToArray() );

            //var dataIndexList = GetDataIndexList( rowList[0], header );
            rowList.RemoveAt( 0 );

            foreach( var row in rowList )
            {
                CardDefine.ExpantionType expType = ( CardDefine.ExpantionType )System.Enum.Parse(typeof(CardDefine.ExpantionType), row.GetCellString( dataIndexDict["kind"] ));
                var data = new CardMaster.CardData(
                        id: row.GetCellInt( dataIndexDict["id"] ),
                        name: row.GetCellString( dataIndexDict["name"] ),
                        cost: row.GetCellInt( dataIndexDict["cost"] ),
                        addAct: row.GetCellInt( dataIndexDict["addAction"] ),
                        addPurchase: row.GetCellInt( dataIndexDict["addPurechase"] ),
                        addPrice: row.GetCellInt( dataIndexDict["addPrice"] ),
                        description: row.GetCellString( dataIndexDict["description"] ),
                        cursePoint: row.GetCellInt( dataIndexDict["cursePoint"] ),
                        victoryPoint: row.GetCellInt( dataIndexDict["victoryPoint"] ),
                        expType: expType
                    );
                dataList.Add( data );
            }

            return dataList;
        }

        static int[] ParseIntArray( string src )
        {
            string[] intString = src.Split( ',' );
            var ints = new int[intString.Length];
            for( int i = 0; i < ints.Length; i++ )
            {
                Debug.Assert( int.TryParse( intString[i].Replace( ",", "" ), out ints[i] ), "ステージマスターの条件設定部分 " + intString[i] + " にゴミが混じってるね@" + i );
            }
            return ints;
        }

    }
} // namespace