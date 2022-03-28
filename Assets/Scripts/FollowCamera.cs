using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private Transform _leftLimit;
    [SerializeField] private Transform _rightLimit;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = _target.position + _offset;
    }

    void LateUpdate()
    {
        var distanceToLeft = Vector3.Distance(_leftLimit.position, _target.position);
        var distanceToRight = Vector3.Distance(_rightLimit.position, _target.position);

        if (distanceToLeft <= _stoppingDistance || distanceToRight <= _stoppingDistance) return;

        transform.position = _target.position + _offset;
    }
}
