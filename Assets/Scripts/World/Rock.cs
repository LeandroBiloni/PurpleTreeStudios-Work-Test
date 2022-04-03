using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Guide _guidePrefab;
    
    private float _timeToDestroy;
    private bool _disableColliderOnFloor;
    private float _gravity;    
    private Vector2 _speed;
    private float _angle;
    private Vector3 _initialPos;
    
    private bool _move = false;

    private float _currentTime = 0;

    private Vector3 _goalPosition;

    private Guide _guide;
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
    public void SetAndInitialize(float speed, float angle, float gravity, Vector3 goalPosition, float duration, bool disableCollider)
    {
        _speed.x = speed;

        _speed.y = speed;        

        _angle = angle;

        _gravity = gravity;

        _goalPosition = goalPosition;

        _move = true;

        _timeToDestroy = duration;

        _disableColliderOnFloor = disableCollider;

        _guide = Instantiate(_guidePrefab);

        _guide.CalculatePosition(_speed, _angle, _gravity, transform.position);
    }

    #region Movement
    private void Move()
    {
        _currentTime += Time.deltaTime;
        var xPos = Utilities.CalculatePositionX(_initialPos.x, _speed.x, _angle, _currentTime);

        var yPos = Utilities.CalculatePositionY(_initialPos.y, _speed.y, _angle, _gravity, _currentTime);


        transform.position = new Vector3(xPos, yPos);
    }

    private void Stop()
    {
        _currentTime = 0;
        _move = false;
        transform.position = new Vector3(transform.position.x, 0);

        if (_disableColliderOnFloor)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    #endregion

    private bool IsLastBounce()
    {
        var time = Utilities.TimeForFinalPositionY(_speed.y, _angle, GameManager.Instance.GetGravity(), transform.position.y, _goalPosition.y);

        if (time <= 0) return false;

        var posAtTime = Utilities.CalculatePositionX(transform.position.x, _speed.x, _angle, time);

        return posAtTime >= _goalPosition.x;
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
                var time = Utilities.TimeForFinalPositionY(_speed.y, _angle, GameManager.Instance.GetGravity(), transform.position.y, _goalPosition.y);

                _speed.x = Utilities.CalculateSpeedX(_goalPosition.x, transform.position.x, _angle, time);

                _speed.y = Utilities.CalculateSpeedY(_goalPosition.y, transform.position.y, _gravity, _angle, time);
            }

            _guide.CalculatePosition(_speed, _angle, _gravity, transform.position);
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Stop();
            StartCoroutine(DestroyTimer());

            _guide.DestroyGuide();
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Goal"))
        {
            _guide.DestroyGuide();
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
