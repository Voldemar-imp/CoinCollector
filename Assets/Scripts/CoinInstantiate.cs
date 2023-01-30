using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinInstantiate : MonoBehaviour
{
    [SerializeField] private Coin _template;

    private CoinSpawn[] _coinSpawns;
    private Coroutine _coinInstantiate;
    private int _counter = 0;

    private void Start()
    {
        _coinSpawns = GetComponentsInChildren<CoinSpawn>();
        _coinInstantiate = StartCoroutine(Instantiate());
    }

    private IEnumerator Instantiate()
    {
        foreach (CoinSpawn coinSpawn in _coinSpawns)
        {
            Coin newCoin = Instantiate(_template, coinSpawn.transform.position, Quaternion.identity);
            yield return null;
        }
    }

    private void OnEnable()
    {
        Coin.Destroyed += OnDestroyed;
    }

    private void OnDisable()
    {
        Coin.Destroyed -= OnDestroyed;
    }

    private void OnDestroyed()
    {
        _counter++;

        if (_counter >= _coinSpawns.Length) 
        {
            StopCoroutine(_coinInstantiate);
            _coinInstantiate = StartCoroutine(Instantiate());
            _counter = 0;
        }
    }
}
