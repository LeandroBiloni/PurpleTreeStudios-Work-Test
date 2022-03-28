using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private float _verticalBounceMultiplier;
    [SerializeField] private float _horizontalBounceMultiplier;
    private Transform _parent;
    private Vector3 _originalLocalPosition;

    private void Start()
    {
        _parent = transform.parent;
        _originalLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = _originalLocalPosition;
    }
    public float GetVerticalBounceMultiplier()
    {
        return _verticalBounceMultiplier;
    }

    public float GetHorizontalBounceMultiplier()
    {
        return _horizontalBounceMultiplier;
    }
}
