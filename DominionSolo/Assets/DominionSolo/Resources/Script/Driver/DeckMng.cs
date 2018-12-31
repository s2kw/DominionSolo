/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


DeckMng.cs

Date:2018/12/30
Description:全カード種類からデッキ１０種１０枚などを管理

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
public class DeckMng : MonoBehaviour {

    public class Deck
    {
        int left;

    }

    /// <summary>
    /// サプライカードの１種類あたりの量。通常二人プレイなら１０枚。
    /// </summary>
    public int supplyCardsCount;

    /// <summary>
    /// デッキ作成。１０枚ずつ１０セット作る
    /// </summary>
    public void CreateDeck()
    {

    }






#if UNITY_EDITOR
    [CustomEditor( typeof( DeckMng ) )]
	public class DeckMngInspector : Editor{
		public override void OnInspectorGUI()
		{
			var script = target as DeckMng;


			DrawDefaultInspector();
		}
		DeckMng Instance{
			get{return (DeckMng)target; }
		}
	}
# endif
}

// } // namespace