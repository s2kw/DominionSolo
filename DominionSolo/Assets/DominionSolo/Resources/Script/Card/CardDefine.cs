/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


CardDefine.cs

Date:
Description:

-------------------------------------------------*/



//using System.Linq;
using System.Collections;
using System.Collections.Generic;
//using UniRx;
//using UniRx.Triggers;
//using UnityEngine.UI;

// namespace jigaX{
public static class CardDefine {

    public enum TerritoryPoint
    {
        屋敷 = 1,
        公領 = 3,
        属州 = 6,
        植民地 = 10,
    }
    public enum TreasurePoint
    {
        銅貨 = 1,
        銀貨 = 2,
        金貨 = 3,
    }


    /// <summary>
    /// 全カードの種類を記載
    /// </summary>
    public enum ActionType
    {
        // ドミニオン
        改築,
        玉座の間,
        密偵,
        鍛冶屋,
        民兵,
        金貸し,
        祝宴,
        泥棒,
        役人,
        工房,
        宰相,
        木こり,
        村,
        地下貯蔵庫,
        礼拝堂,
        堀,
        冒険者,
        議事堂,
        鉱山,
        市場,
        研究所,
        祝祭,
        魔女,
        書庫,
        // 陰謀
    }
    /// <summary>
    /// 拡張キットのバージョン
    /// </summary>
    public enum ExpantionType
    {
        ドミニオン = 0,
        陰謀,
    }

}

// } // namespace