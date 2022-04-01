using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Action OnCoinPicked = () => { };

    private float _duration;

    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _rotationSpeedOnPickup;

    [SerializeField] private float _moveSpeedToUI;
    [Min(0)]
    [SerializeField] private float _dissapearDistance;

    
    private void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    public void SetDuration(float duration)
    {
        _duration = duration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hero"))
        {
            _rotationSpeed = _rotationSpeedOnPickup;

            StopAllCoroutines();

            StartCoroutine(MoveTowardsUI());
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(_duration);

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
            transform.position = Vector3.Lerp(transform.position, coinsUIWorld, _moveSpeedToUI * Time.deltaTime);

            coinsUIWorld = Camera.main.ScreenToWorldPoint(coinsUITransform.position + offset);

            yield return endFrame;
        }

        OnCoinPicked?.Invoke();

        Destroy(gameObject);
    }
}
