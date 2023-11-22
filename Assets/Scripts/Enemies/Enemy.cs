using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class Enemy : MonoBehaviour
{

    async UniTaskVoid Start()
    {
        //ランダム秒数待つ
        var waitTime = Random.Range(0f, 1f);
        await UniTask.Delay(System.TimeSpan.FromSeconds(waitTime));

        //エネミーを上下にゆらゆらさせておく
        transform.DOLocalMoveY(0.5f, 1f).SetRelative().SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
