using UniRx;
using UnityEngine;

public interface IInputEventProvider
{
    IReadOnlyReactiveProperty<bool> ActionButton { get; }
    IReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
}