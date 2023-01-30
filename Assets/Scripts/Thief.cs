using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(Animator))]
public class Thief : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _speed;
    [SerializeField] private float _jampForce;
    [SerializeField] private UnityEvent _defeated;

    private Animator _animator;
    private bool _isOnGround = true;
    private bool _isAlive = true;
    private static int _walkleft = Animator.StringToHash("WalkLeft");
    private static int _walkright = Animator.StringToHash("WalkRight");

    public event UnityAction Defeated
    {
        add => _defeated?.AddListener(value);
        remove => _defeated?.RemoveListener(value);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ghost>(out Ghost ground) && _isAlive)
        {
            _renderer.enabled = false;
            _isAlive = false;
            _defeated?.Invoke();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _isOnGround = collision.gameObject.TryGetComponent<Ground>(out Ground ground);
    }

    private void Update()
    {
        if (_isAlive)
        {
            PlayerController();
        }
    }

    private void PlayerController()
    {
        if (Input.GetKey(KeyCode.W) && _isOnGround)
        {
            _rigidbody2D.AddForce(new Vector2(0, _jampForce));
            _isOnGround = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            _animator.CrossFade(_walkright, Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);
            _animator.CrossFade(_walkleft, Time.deltaTime);
        }
    }
}
