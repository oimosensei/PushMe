using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startText;
    void Start()
    {
        _startText.rectTransform.DOAnchorPosY(20f, 0.6f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetRelative(true);

        //unirxでクリックしたらシーン遷移
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ =>
            {
                Initiate.Fade("GameScene", Color.black, 1.0f);
            })
            .AddTo(this);
    }
}
