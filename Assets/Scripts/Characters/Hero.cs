using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _breakStrength;
    private float _moveSpeed;

    private Vector3 _lastDir;

    private Rigidbody2D _rigidBody;

    private Animator _animator;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        var dir = new Vector3(horizontal, 0);
        
        Move(dir);
    }


    private void Move(Vector3 direction)
    {
        //Accelerates
        if (direction.x != 0)
        {
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
            _lastDir = direction.normalized;
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