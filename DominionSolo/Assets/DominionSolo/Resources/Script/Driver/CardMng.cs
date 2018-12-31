/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


CardMng.cs

Date:2018/12/30
Description:
全プレイヤーの手札や山札などを管理する


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
public class CardMng : MonoBehaviour {

	void Awake(){}

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {}

# if UNITY_EDITOR
	[CustomEditor( typeof( CardMng ) )]
	public class CardMngInspector : Editor{
		public override void OnInspectorGUI()
		{
			var script = target as CardMng;


			DrawDefaultInspector();
		}
		CardMng Instance{
			get{return (CardMng)target; }
		}
	}
# endif
}

// } // namespace