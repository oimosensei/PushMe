using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using KanKikuchi.AudioManager;
public class PushCollider : MonoBehaviour
{
    // Start is called before the first frame update
    FingerPlayer finger;
    [Inject] private GameStateManager _gameStateManager;
    void Start()
    {
        //親要素
        finger = transform.parent.GetComponent<FingerPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        //layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Button"))
        {
            finger.isPushed.Value = true;
            KanKikuchi.AudioManager.SEManager.Instance.Play(SEPath.BUTTONPUSH);
        }
    }
}
