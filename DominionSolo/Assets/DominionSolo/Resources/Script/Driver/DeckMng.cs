/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


DeckMng.cs

Date:2018/12/30
Description:全カード種類からデッキ１０種１０枚などを管理
財宝カードや領地カード等の諸々のカード群も管理。

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

    /// <summary>
    /// デッキとか
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// 残り枚数
        /// </summary>
        public int left
        {
            get; private set;
        }
        public Deck( int _maxCount )
        {
            left = _maxCount;
        }
        public virtual void DrawCard()
        {
            left--;
        }

    }
    /// <summary>
    /// サプライカード山札
    /// </summary>
    public class SupplyDeck : Deck
    {
        /// <summary>
        /// サプライカードのタイプ
        /// </summary>
        public CardDefine.ActionType actionType;

        /// <summary>
        /// サプライカードがなくなった時にトリガーされる
        /// </summary>
        public System.Action OnFinishSupplyCard;
        public SupplyDeck(int _maxCount ) : base( _maxCount ) { }
    }
    /// <summary>
    /// 領土カード山札
    /// </summary>
    public class TerritoryDeck : Deck
    {
        readonly CardDefine.TerritoryPoint victoryPoint;
        public TerritoryDeck( int _maxCount, CardDefine.TerritoryPoint _victoryPoint ) : base( _maxCount ) {
            victoryPoint = _victoryPoint;
        }
    }

    public class TreasureDeck : Deck
    {
        readonly CardDefine.TreasurePoint treasurePoint;
        public TreasureDeck( int _maxCount, CardDefine.TreasurePoint _treasurePoint ) : base( _maxCount )
        {
            treasurePoint = _treasurePoint;
        }
    }

    /// <summary>
    /// サプライカードの１種類あたりの量。通常二人プレイなら１０枚。
    /// </summary>
    public int supplyCardsCount;

    public static readonly int TWO_PLYAERS_SUPPLYCARD_COUNT = 10;

    List<TerritoryDeck> territoryDecks;
    List<TreasureDeck> treasureDecks;
    /// <summary>
    /// デッキ作成。１０枚ずつ１０セット作る
    /// </summary>
    public void CreateDeck()
    {

    }

    public void CreateDeck( CardDefine.ActionType[] types, int playerCount = 2 )
    {
        Debug.Assert( types.Length == 10 );

        CreateBasicDeck();

        // Actionカードを引数のタイプに合わせて生成
        // TODO:プレイヤー人数に応じて枚数を分ける


    }
    void CreateBasicDeck()
    {
        // 財宝カード
        treasureDecks = new List<TreasureDeck>();
        treasureDecks.Add( new TreasureDeck( int.MaxValue, CardDefine.TreasurePoint.銅貨 ) );
        treasureDecks.Add( new TreasureDeck( int.MaxValue, CardDefine.TreasurePoint.銀貨 ) );
        treasureDecks.Add( new TreasureDeck( int.MaxValue, CardDefine.TreasurePoint.金貨 ) );

        // 領土カード
        territoryDecks = new List<TerritoryDeck>();
        territoryDecks.Add( new TerritoryDeck( 10, CardDefine.TerritoryPoint.屋敷 ) );
        territoryDecks.Add( new TerritoryDeck( 10, CardDefine.TerritoryPoint.公領 ) );
        territoryDecks.Add( new TerritoryDeck( 10, CardDefine.TerritoryPoint.属州 ) );


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