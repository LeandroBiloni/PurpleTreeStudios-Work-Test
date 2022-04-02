using System.Collections;
using UnityEngine;

public class Hero : MonoBehaviour, IControllable
{
    [SerializeField] private Transform _box;

    [Header("Stats")]
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _breakStrength;

    [Space]

    [SerializeField] private ParticleSystem _smoke;

    private float _moveSpeed;

    private Vector3 _lastDir;

    private Rigidbody2D _rigidBody;

    private Animator _animator;

    private SpriteRenderer _spriteRenderer;

    private bool _canMove;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        GameManager.Instance.AddControllable(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canMove) return;

        var horizontal = Input.GetAxis("Horizontal");

        var dir = new Vector3(horizontal, 0);
        
        Move(dir);
    }

    public Transform GetBoxTransform()
    {
        return _box;
    }

    private void Move(Vector3 direction)
    {
        //Accelerates
        if (direction.x != 0)
        {
            _lastDir = direction.normalized;
            if (_moveSpeed == 0)
            {
                var pos = new Vector3(transform.position.x - _lastDir.x , _smoke.transform.position.y);
                _smoke.transform.position = pos;
                _smoke.Play();
            }
            _moveSpeed += _acceleration;

            if (_moveSpeed >= _maxMoveSpeed)
            {
                _moveSpeed = _maxMoveSpeed;
            }

            if (direction.x > 0)
            {
                _spriteRenderer.flipX = false;
            }

            if (direction.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            
        }

        //Breaks
        if (direction.x == 0)
        {
            _moveSpeed -= _breakStrength;

            if (_moveSpeed < 0)
            {
                _moveSpeed = 0;
                _lastDir = Vector3.zero;
            }
        }

        _animator.SetFloat("Horizontal",_moveSpeed);

        direction += _moveSpeed * _lastDir;

        var newPos = transform.position + direction * Time.deltaTime;

        _rigidBody.MovePosition(newPos);
    }

    public void EnableControl()
    {
        var difficulty = DifficultyMemory.Instance.GetDifficultySettings();

        _maxMoveSpeed = difficulty.heroMaxMoveSpeed;
        _acceleration = difficulty.heroAcceleration;
        _breakStrength = difficulty.heroBreakStrength;

        _canMove = true;
    }

    public void DisableControl()
    {
        _canMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Limits"))
        {
            _lastDir = Vector3.zero;

            _moveSpeed = 0;

            _rigidBody.velocity = Vector2.zero;
        }
    }

    
}
