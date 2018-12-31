/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


UIBase.cs

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
public abstract class UIBase : MonoBehaviour {

    RectTransform m_rectTransform;
    public RectTransform rectTransform
    {
        get {
            m_rectTransform = m_rectTransform ?? GetComponent<RectTransform>();
            return m_rectTransform;
        }
        set
        {
            m_rectTransform = value;
        }
    }

# if UNITY_EDITOR
	[CustomEditor( typeof( UIBase ) )]
	public class UIBaseInspector : Editor{
		public override void OnInspectorGUI()
		{
			var script = target as UIBase;


			DrawDefaultInspector();
		}
		UIBase Instance{
			get{return (UIBase)target; }
		}
	}
# endif
}

// } // namespace