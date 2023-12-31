using System;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
///  シーン遷移を実行する
/// </summary>
public static class SceneLoader
{
    // private static TrasitionComponent _component;

    // private static TrasitionComponent TrasitionComponent
    // {
    //     get
    //     {
    //         if (_component != null) return _component;
    //         var p = Resources.Load("TransitionManager");
    //         var go = GameObject.Instantiate(p) as GameObject;
    //         _component = go.GetComponent<TrasitionComponent>();
    //         return _component;
    //     }
    // }


    /// <summary>
    /// シーン遷移が完了した
    /// </summary>
    // public static IObservable<Unit> OnTransitionFinished
    // {
    //     get
    //     {
    //         if (!TrasitionComponent.IsTransition.Value)
    //         {
    //             return Observable.Return(Unit.Default);
    //         }
    //         else
    //         {
    //             return TrasitionComponent
    //                 .IsTransition
    //                 .FirstOrDefault(x => !x).AsUnitObservable();
    //         }
    //     }
    // }

    /// <summary>
    /// シーン遷移する
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="action"></param>
    public static void LoadScene(string nextScene, Action<DiContainer> action)
    {
        Initiate.Fade(nextScene, Color.black, 1.0f);
    }
}
