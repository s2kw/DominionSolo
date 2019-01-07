/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


Player.cs

Date:2019/01/02
Description:プレイヤー単体の概念

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
public class Player : MonoBehaviour {

    public bool isFinish { get; private set; }


    /// ターン開始
    public void OnStartTurn() {
        isFinish = false;
    }

    /// ターン終了
    public void OnFinishTurn()
    {


        isFinish = true;
    }

    /// <summary>
    /// 他プレイヤーに攻撃された時
    /// </summary>
    public void OnAttacked()
    {
        // 
    }

#if UNITY_EDITOR
    [CustomEditor( typeof( Player ) )]
	public class PlayerInspector : Editor{
		public override void OnInspectorGUI()
		{
			var script = target as Player;


			DrawDefaultInspector();
		}
		Player Instance{
			get{return (Player)target; }
		}
	}
# endif
}

// } // namespace