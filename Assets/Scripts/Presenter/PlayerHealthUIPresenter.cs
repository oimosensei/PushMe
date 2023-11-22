using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Zenject;
public class PlayerHealthUIPresenter : MonoBehaviour
{
    [SerializeField] List<Image> _healthImages;
    [Inject] private FingerPlayer _player;
    void Start()
    {
        _player.health.Subscribe(health =>
        {
            for (int i = 0; i < _healthImages.Count; i++)
            {
                _healthImages[i].enabled = i < health;
            }
        });
    }
}
