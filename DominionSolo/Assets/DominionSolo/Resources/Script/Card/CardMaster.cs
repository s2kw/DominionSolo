/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


CardMaster.cs

Date:
Description:

-------------------------------------------------*/



using UnityEngine;
//using System.Linq;
using System.Collections;
using System.Collections.Generic;
//using UniRx;
//using UniRx.Triggers;
//using UnityEngine.UI;

# if UNITY_EDITOR
using UnityEditor;
# endif

// namespace jigaX{
public class CardMaster : ScriptableObject {
    [System.Serializable]
    public class CardData
    {
        public int id;
        public string name;
        /// <summary>
        /// 使用時に追加される各種
        /// </summary>
        public int addAction, addPurchase, addPrice;
        public int cost;
        public int cursePoint;
        public int victoryPoint;
        public CardDefine.ExpantionType expType;

        public CardData( int id, string name, int cost, int addAct, int addPurchase, int addPrice, 
        string description, int cursePoint, int victoryPoint, CardDefine.ExpantionType expType )
        {
            this.id = id;
            this.name = name;
            this.cost = cost;
            this.addAction = addAct;
            this.addPurchase = addPurchase;
            this.addPrice = addPrice;
            this.cursePoint = cursePoint;
            this.victoryPoint = victoryPoint;
            this.expType = expType;
        }

    }
    public List<CardData> dataList;




#if UNITY_EDITOR
    [CustomEditor( typeof( CardMaster ) )]
	public class CardMasterInspector : Editor{
		public override void OnInspectorGUI()
		{
			var script = target as CardMaster;


			DrawDefaultInspector();
		}
		CardMaster Instance{
			get{return (CardMaster)target; }
		}
    }
# endif
}
// } // namespace