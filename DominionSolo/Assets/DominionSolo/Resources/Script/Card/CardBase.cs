/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


CardBase.cs

Date:2018/12/30
Description:カードの継承元
全カードに共通するモノだけを保持する



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

# region すべてのカードのインターフェース
/// <summary>
/// アクションカードとしての振る舞いを定義
/// </summary>
public interface IActionCard
{
    // +2 カードとかの効果処理
    void TriggerPreaction();
    // アクションカードの効果を発動させる
    void TriggerAction();

    // アクションの操作が終わったかどうか
    bool isFinishAction { get; }

    /// <summary>
    /// アクション回数
    /// </summary>
    /// <value>The additional action count.</value>
    int addActionCount { get; }
    /// <summary>
    /// 購入回数
    /// </summary>
    /// <value>The additional purchase count.</value>
    int addPurchaseCount { get; }
    /// <summary>
    /// 財宝
    /// </summary>
    /// <value>The additional price count.</value>
    int addPriceCount { get; }
}
/// <summary>
/// 領土カードとしての振る舞いを定義
/// </summary>
public interface ITerritoryCard
{
    int victoryPoint { get; }
}

/// <summary>
/// 財宝カード
/// </summary>
public interface ITreasureCard
{
    // 支払い価値
    int price { get; }
}

/// <summary>
/// 呪いのカード
/// 呪いのカードはICurseとITerritoryの両方を持たせる。
/// </summary>
public interface ICurse
{
    // 呪い点
    int cursePoint { get; }
}
/// <summary>
/// 防衛カード。堀とか。
/// </summary>
public interface IDefenderCard
{

}
#endregion //すべてのカードのインターフェース

// namespace jigaX{
public abstract class CardBase : MonoBehaviour {

    // 元値
    int defaultCost;
    // 効果付与済みのコスト
    public int cost
    {
        get { return defaultCost + costAfect; }
    }
    /// <summary>
    /// 効果付与量
    /// </summary>
    public int costAfect;

    /// <summary>
    /// 山札から引いた時 => 手札に入る
    /// </summary>
    public abstract void Draw();
    /// <summary>
    /// 破棄時 => 廃棄上に入る
    /// </summary>
    public abstract void Revocation();
    /// <summary>
    /// 使用済み山札へ
    /// </summary>
    public abstract void GoToAlreadyUsePlace();
    /// <summary>
    /// カードが手札に入った時に呼ばれる。初期化用。
    /// </summary>
    public abstract void OnDraw();



#if UNITY_EDITOR
    [CustomEditor( typeof( CardBase ) )]
	public class CardBaseInspector : Editor{
		public override void OnInspectorGUI()
		{
			var script = target as CardBase;


			DrawDefaultInspector();
		}
		CardBase Instance{
			get{return (CardBase)target; }
		}
	}
# endif
}

// } // namespace