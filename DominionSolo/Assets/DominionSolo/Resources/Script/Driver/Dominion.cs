/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


Dominion.cs

Date:2018/12/30
Description:ドミニオンの大まかなゲーム全体を回す

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
public class Dominion : MonoBehaviour {
    public enum State
    {
        Init,
        BuildDeck, // デッキ構築
        PreStart, // 最初の１ターン目の処理
        StartGame, // ゲーム開始演出など
        GameLoop, // ゲームループ
        Result, //集計と結果
    }


    void Awake(){}

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {}

# if UNITY_EDITOR
	[CustomEditor( typeof( Dominion ) )]
	public class DominionInspector : Editor{
		public override void OnInspectorGUI()
		{
			var script = target as Dominion;


			DrawDefaultInspector();
		}
		Dominion Instance{
			get{return (Dominion)target; }
		}
	}
# endif
}

// } // namespace