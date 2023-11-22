using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 250f;
    [SerializeField] private float _rotateSpeed = 150f;
    [SerializeField] private float _bouceForce = 900f;

    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;

    public float BounceForce => _bouceForce;
}