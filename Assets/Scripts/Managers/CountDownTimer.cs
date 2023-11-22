using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class CountDownTimer
{
    public ReactiveProperty<int> TimerValue;
    private TimerState _timerState = TimerState.Ready;

    public CountDownTimer(int s)
    {
        TimerValue = new ReactiveProperty<int>(s);
    }

    public async UniTaskVoid StartTimer()
    {
        if (_timerState != TimerState.Ready)
        {
            return;
        }

        _timerState = TimerState.Working;

        while (TimerValue.Value > 0)
        {
            await UniTask.Delay(1000);
            TimerValue.Value--;
        }

        _timerState = TimerState.Ready;
    }

    public enum TimerState
    {
        Ready,
        Working,
    }
}