using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;

    private float _gravity;    
    private Vector2 _speed;
    private float _angle;
    private Vector3 _initialPos;

    
    private bool _move = false;

    private float _currentTime = 0;

    private Vector3 _goalPosition;

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
    public void SetAndInitialize(float speed, float angle, float gravity, Vector3 goalPosition)
    {
        _speed.x = speed;

        _speed.y = speed;        

        _angle = angle;

        _gravity = gravity;

        _goalPosition = goalPosition;

        _move = true;
    }

    #region Movement
    private void Move()
    {
        _currentTime += Time.deltaTime;
        var xPos = _initialPos.x + _speed.x * Mathf.Cos(Mathf.Deg2Rad * _angle) * _currentTime;

        var yPos = _initialPos.y + _speed.y * Mathf.Sin(Mathf.Deg2Rad * _angle) * _currentTime + (-_gravity / 2) * Mathf.Pow(_currentTime, 2);


        transform.position = new Vector3(xPos, yPos);
    }

    private void Stop()
    {
        _currentTime = 0;
        _move = false;
        transform.position = new Vector3(transform.position.x, 0);
    }
    #endregion

    private bool IsLastBounce()
    {
        var time = CalculateTimeForGoal();

        if (time <= 0) return false;

        var posAtTime = transform.position.x + _speed.x * Mathf.Cos(Mathf.Deg2Rad * _angle) * time;

        return posAtTime >= _goalPosition.x;
    }

    /// <summary>
    /// Calculates the time it will take to reach goal.
    /// </summary>
    /// <returns>The time.</returns>
    private float CalculateTimeForGoal()
    {
        float a = -_gravity / 2;

        float b = _speed.y * Mathf.Sin(Mathf.Deg2Rad * _angle);

        //float c = _goalPosition.y - transform.position.y;

        float c = transform.position.y - _goalPosition.y;

        float det = Mathf.Pow(b, 2) - 4 * a * c;

        if (det <= 0) return 0;

        float time = (-b - Mathf.Sqrt(det)) / (2 * a);

        return time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            var box = collision.gameObject.GetComponent<Box>();
            _initialPos = transform.position;

            _currentTime = 0;

            _speed.y = _speed.y * box.GetVerticalBounceMultiplier();

            _speed.x = _speed.x * box.GetHorizontalBounceMultiplier();

            if (IsLastBounce())
            {
                var time = CalculateTimeForGoal();
                // Vxf = (Xf - Xi) / (Cos(ang) * t)
                var spx = (_goalPosition.x - transform.position.x) / (Mathf.Cos(Mathf.Deg2Rad * _angle) * time);
                _speed.x = spx;

                // Vyf = (Yf - Yi + 1/2 g * t^2) / (Sin(ang) * t)

                var spy = (_goalPosition.y - transform.position.y + (_gravity / 2) * Mathf.Pow(time, 2)) / (Mathf.Sin(Mathf.Deg2Rad * _angle) * time);
                _speed.y = spy;
            }
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Stop();
            StartCoroutine(DestroyTimer());
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Goal"))
        {
            Destroy(gameObject);
            return;
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(_timeToDestroy);

        Destroy(gameObject);
    }
}
