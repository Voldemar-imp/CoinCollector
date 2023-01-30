using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Thief _thief;
    [SerializeField] private GameOver _template;

    private float _positionX;
    private Vector3 _defaultPosition;

    private void Start()
    {
        _defaultPosition = transform.position;
        _positionX = transform.position.x - _thief.transform.position.x;
    }

    private void OnEnable()
    {
        _thief.Defeated += OnDefited;
    }

    private void OnDisable()
    {
        _thief.Defeated -= OnDefited;
    }

    private void Update()
    {
        transform.position = new Vector3(_thief.transform.position.x + _positionX, _defaultPosition.y, _defaultPosition.z);
    }

    private void OnDefited()
    {
        GameOver gameOver = Instantiate(_template, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);        
    }
}
