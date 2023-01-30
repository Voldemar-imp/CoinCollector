using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;


public class Coin : MonoBehaviour
{   
    [SerializeField] private UnityEvent _destroyed;
 
    public static event UnityAction Destroyed;
    private Coroutine _destroyCoroutine;
    private Vector3 targetPosition;
    private float _speed = 5.0f;
    private bool _isPickedUp = false;   

    private void Start()
    {
        float jampRangeY = 2f;
        targetPosition = transform.position + new Vector3(0, jampRangeY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief) && _isPickedUp == false)
        {
            _isPickedUp = true;
            _destroyCoroutine = StartCoroutine(DestroyCoroutine());
        }
    }
   
    private IEnumerator DestroyCoroutine() 
    {        
        _destroyed?.Invoke();
        Destroyed?.Invoke();

        while (targetPosition.y != transform.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed*Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        StopCoroutine(_destroyCoroutine);
    }
}
