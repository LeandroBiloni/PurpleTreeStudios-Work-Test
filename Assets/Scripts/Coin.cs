using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _timeToDestroy;

    [SerializeField] private float _rotationSpeed;

    [Header("On Pick Animation")]
    [Min(0)]
    [SerializeField] private float _moveSpeed;
    [Min(0)]
    [SerializeField] private float _dissapearDistance;

    public Action OnCoinPicked = () => { };

    private void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hero"))
        {            
            StopAllCoroutines();

            StartCoroutine(MoveTowardsUI());
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(_timeToDestroy);

        Destroy(gameObject);
    }

    IEnumerator MoveTowardsUI()
    {
        var offset = new Vector3(-1, 0);

        var coinsUITransform = UIManager.Instance.GetCoinsUIRect().transform;

        var coinsUIWorld = Camera.main.ScreenToWorldPoint(coinsUITransform.position + offset);
        
        var endFrame = new WaitForEndOfFrame();
        
        while (Vector3.Distance(transform.position, coinsUIWorld) > _dissapearDistance)
        {
            transform.position = Vector3.Lerp(transform.position, coinsUIWorld, _moveSpeed * Time.deltaTime);

            coinsUIWorld = Camera.main.ScreenToWorldPoint(coinsUITransform.position + offset);

            yield return endFrame;
        }

        OnCoinPicked?.Invoke();

        Destroy(gameObject);
    }
}
