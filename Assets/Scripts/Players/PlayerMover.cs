using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float rotateSpeed = 50f;
    [SerializeField] private float velocity = 1.0f;
    [SerializeField] private bool isInputKeyFD = true;
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private PlayerSettings _playerSettings;

    [Inject] private FingerPlayer _player;
    [Inject] private GameStateManager _gameStateManager;
    private Rigidbody2D rb;

    private bool isBounceable { get { return bounceCooldown <= 0f; } }
    private float bounceCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _gameStateManager.gameState
            .Subscribe(x =>
            {
                rb.bodyType = x == GameState.Playing ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
            })
            .AddTo(this);

        //unirx update
        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                if (bounceCooldown > 0f)
                {
                    bounceCooldown -= Time.deltaTime;
                }
                else
                {
                    bounceCooldown = 0f;
                }

            })
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStateManager.gameState.Value != GameState.Playing) return;
        float horizontalInput = 0f;
        float verticalInput = 0f;


        if (isInputKeyFD)
        {
            if (Input.GetKey(KeyCode.F))
            {
                horizontalInput = 1f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalInput = -1f;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.K))
            {
                horizontalInput = 1f;
            }
            else if (Input.GetKey(KeyCode.J))
            {
                horizontalInput = -1f;
            }
        }

        if (rb.velocity.y < velocity)
        {
            Vector2 movement = _playerSettings.MoveSpeed * transform.up * Time.deltaTime;
            rb.AddForce(movement);
        }


        // rb.velocity = velocity * transform.up;
        float rotation = horizontalInput * _playerSettings.RotateSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBounceable ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        Vector2 normal = collision.contacts[0].normal;
        Vector2 direction = Vector2.Reflect(rb.velocity.normalized, normal);
        rb.AddForce(normal * _playerSettings.BounceForce, ForceMode2D.Impulse);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _player.health.Value--;
            SEManager.Instance.Play(SEPath.DAMAGE);
            Debug.Log("Damage");
            if (_player.health.Value <= 0)
            {
                _gameStateManager.gameState.Value = GameState.GameOver;
            }
        }
        else
        {
            Debug.Log("Bounce");
            bounceCooldown = 0.5f;
            SEManager.Instance.Play(SEPath.BOUND);
        }
    }

}