using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;

    private float _gravity;    
    private float _verticalSpeed;
    private float _horizontalSpeed;
    private float _angle;
    private Vector3 _initialPos;

    
    private bool _move = false;

    private float _currentTime = 0;

    private void Start()
    {
        _initialPos = transform.position;
    }
    private void Update()
    {
        if (_move)
            Move();
    }

    /// <summary>
    /// Sets the values for movement and initializes it.
    /// </summary>
    /// <param name="speed">Rock speed.</param>
    /// <param name="angle">Throw angle.</param>
    /// <param name="gravity">Gravity force applied.</param>
    public void SetAndInitialize(float speed, float angle, float gravity)
    {
        _verticalSpeed = speed;

        _horizontalSpeed = speed;

        _angle = angle;

        _gravity = gravity;

        _move = true;
    }

    #region Movement
    void Move()
    {
        _currentTime += Time.deltaTime;
        var xPos = _initialPos.x + _horizontalSpeed * Mathf.Cos(Mathf.Deg2Rad * _angle) * _currentTime;

        var yPos = _initialPos.y + _verticalSpeed * Mathf.Sin(Mathf.Deg2Rad * _angle) * _currentTime + (-_gravity / 2) * Mathf.Pow(_currentTime, 2);


        transform.position = new Vector3(xPos, yPos);

        if (transform.position.y <= 0)
        {
            
        }
    }

    void Stop()
    {
        _currentTime = 0;
        _move = false;
        transform.position = new Vector3(transform.position.x, 0);
    }
    #endregion

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_timeToDestroy);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            var box = collision.gameObject.GetComponent<Box>();
            _initialPos = transform.position;

            _currentTime = 0;
            _verticalSpeed = _verticalSpeed * box.GetVerticalBounceMultiplier();

            _horizontalSpeed = _horizontalSpeed * box.GetHorizontalBounceMultiplier();
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Stop();
            StartCoroutine(Destroy());
        }
    }
}
