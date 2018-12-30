/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


ForceSave.cs

Date:2018/12/30
Description:いつもの保存するやつ

-------------------------------------------------*/



using UnityEngine;
//using System.Linq;
using System.Collections;
using System.Collections.Generic;
//using UniRx;
//using UniRx.Triggers;
//using UnityEngine.UI;

using UnityEditor;

// namespace jigaX{
public class ForceSave : Editor {
    [MenuItem("File/Force Save %#&s")]
    public static void ForceSaveAllResources()
    {
        AssetDatabase.SaveAssets();
    }

}

// } // namespace