/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


GameMain.cs

Date:2019/01/03
Description:
ゲームの手番管理

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
public class GameMain : MonoBehaviour {

    public bool isEndGame;
    public Player currentPlayer;
    List<Player> players = new List<Player>();


    void OnStart()
    {

    }

    private void Update()
    {
        Debug.Assert( currentPlayer != null, "プレイヤー居ない");

        if( !currentPlayer.isFinish ) return;

        var nextPlayerIndex = players.IndexOf(currentPlayer) +1;

        if( players.Count >= nextPlayerIndex ) nextPlayerIndex = 0;

        currentPlayer = players[nextPlayerIndex];

        currentPlayer.OnStartTurn();
    }

#if UNITY_EDITOR
    [CustomEditor( typeof( GameMain ) )]
	public class GameMainInspector : Editor{
		public override void OnInspectorGUI()
		{
			var script = target as GameMain;


			DrawDefaultInspector();
		}
		GameMain Instance{
			get{return (GameMain)target; }
		}
	}
# endif
}

// } // namespace