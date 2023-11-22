using UnityEngine;
using UniRx;
using Zenject;
using KanKikuchi.AudioManager;
using DG.Tweening;

public class FingerPlayer : MonoBehaviour
{
    public ReactiveProperty<bool> isPushed = new ReactiveProperty<bool>(false);

    public ReactiveProperty<int> health = new ReactiveProperty<int>(3);

    [Inject] private GameStateManager _gameStateManager;

    void Start()
    {
        isPushed.Subscribe(x =>
        {
            if (x)
            {
                Debug.Log("Button Pushed!");
                _gameStateManager.gameState.Value = GameState.Clear;

                //名前でボタンのgameobjectを探す
                var buttonUe = GameObject.Find("push-button-ue");
                if (buttonUe != null)
                {
                    // Tweenを作成して実行する
                    buttonUe.transform.DOLocalMoveY(0.2795f, 0.4f);
                    buttonUe.transform.DOScaleY(0.1830724f, 0.4f);
                }

            }
        });
        health.Subscribe(x =>
        {
            Debug.Log("Health: " + x);
            if (x <= 0)
            {
                Debug.Log("Game Over");
            }
        });
    }
}