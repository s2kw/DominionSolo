/*-------------------------------------------------

	System Designed,
	Code Written,
	by Kunihiro Sasakawa as s2kw@jigax.jp


Singleton.cs

Date:2018/12/30
Description:シングルトンのベースクラス

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
public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if( instance == null )
            {
                System.Type t = typeof( T );

                instance = ( T )FindObjectOfType( t );
                if( instance == null )
                {
                    Debug.LogError( t + " をアタッチしているGameObjectはありません" );
                }
            }

            return instance;
        }
    }
    virtual protected void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        if( instance == null )
        {
            instance = this as T;
            return true;
        }
        else if( Instance == this )
        {
            return true;
        }
        Destroy( this );
        return false;
    }
}

// } // namespace