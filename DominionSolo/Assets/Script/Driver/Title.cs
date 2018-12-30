/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


Title.cs

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
public class Title : MonoBehaviour {

	void Awake(){}

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {}

# if UNITY_EDITOR
	[CustomEditor( typeof( Title ) )]
	public class TitleInspector : Editor{
		public override void OnInspectorGUI()
		{
			var script = target as Title;


			DrawDefaultInspector();
		}
		Title Instance{
			get{return (Title)target; }
		}
	}
# endif
}

// } // namespace