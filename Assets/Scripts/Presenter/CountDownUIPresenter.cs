using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using Zenject;
using Cysharp.Threading.Tasks;
using DG.Tweening;
public class CountDownUIPresenter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _countDownText;
    [Inject] private GameStateManager _gameStateManager;
    void Start()
    {
        _countDownText.gameObject.SetActive(false);
        _gameStateManager.readyTimer
        .TimerValue
        .TakeWhile(x => x > 0)
        .Subscribe(x =>
        {
            _countDownText.gameObject.SetActive(true);
            _countDownText.text = x.ToString();
            _countDownText.transform.localScale = 3f * Vector3.one;
            _countDownText.transform.DOScale(1.0f, 0.8f).SetEase(Ease.OutQuad);
        })
        .AddTo(this);

        _gameStateManager.readyTimer
        .TimerValue
        .Where(x => x == 0)
        .Subscribe(async _ =>
        {
            _countDownText.text = "START!";
            _countDownText.characterSpacing = -1f;
            // DOTweenを使って、文字の間隔をアニメーションする
            DOTween.To(() => _countDownText.characterSpacing, x => _countDownText.characterSpacing = x, 12f, 0.8f)
                .SetEase(Ease.OutQuad);

            await UniTask.Delay(900);

            _countDownText.text = "";
            _countDownText.gameObject.SetActive(false);
        })
        .AddTo(this);

    }
}
