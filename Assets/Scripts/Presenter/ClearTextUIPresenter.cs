using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using Zenject;
using Cysharp.Threading.Tasks;
using DG.Tweening;
public class ClearTextUIPresenter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _clearText;
    [Inject] private GameStateManager _gameStateManager;
    void Start()
    {
        _clearText.gameObject.SetActive(false);


        _gameStateManager.gameState
        .Where(x => x == GameState.Clear || x == GameState.GameOver)
        .Subscribe(async _ =>
        {
            await UniTask.Delay(200);
            _clearText.gameObject.SetActive(true);

            if (_gameStateManager.gameState.Value == GameState.Clear)
            {
                _clearText.text = "Clear!";
                _clearText.characterSpacing = -1f;
                // DOTweenを使って、文字の間隔をアニメーションする
                DOTween.To(() => _clearText.characterSpacing, x => _clearText.characterSpacing = x, 12f, 0.8f)
                    .SetEase(Ease.OutQuad);

                _clearText.text = "";
                _clearText.gameObject.SetActive(false);
            }
            else
            {
                _clearText.text = "Game Over";
                //透明度を上げていく
                _clearText.DOFade(0f, 0f).SetEase(Ease.InQuint);
                _clearText.DOFade(1f, 0.8f).SetEase(Ease.OutQuad);

            }


            await UniTask.Delay(2500);

        })
        .AddTo(this);

    }
}
