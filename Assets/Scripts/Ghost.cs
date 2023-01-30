using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Animator))]
public class Ghost : MonoBehaviour
{
    [SerializeField] private float _range;

    private Animator _animator;
    private Vector3 _target;
    private Vector3 _position1;
    private Vector3 _position2;

    private static int _leftMove = Animator.StringToHash("LeftMove");
    private static int _rightMove = Animator.StringToHash("RightMove");


    private void Start()
    {
        _animator = GetComponent<Animator>();   
        _position1 = transform.position - new Vector3(_range, 0, 0);
        _position2 = transform.position - new Vector3(-_range, 0, 0);
        _target = _position1;
    }

    private void OnValidate()
    {
        int minRange = 2;

        if (_range < minRange)
        { 
            _range = minRange; 
        } 
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime);

        if (transform.position.x <= _position1.x+1)
        {
            _target = _position2;
            _animator.CrossFade(_rightMove, Time.deltaTime);
        }
        if (transform.position.x >= _position2.x-1)
        {
            _target = _position1;
            _animator.CrossFade(_leftMove, Time.deltaTime);
        }
    }
}
