using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour
{
    private float _verticalBounceMultiplier;
    private float _horizontalBounceMultiplier;
    private Vector3 _originalLocalPosition;

    private void Start()
    {
        _originalLocalPosition = transform.localPosition;
        StartCoroutine(Configure());
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

    IEnumerator Configure()
    {
        var difficulty = DifficultyMemory.Instance.GetDifficultySettings();

        while (!difficulty)
        {
            yield return new WaitForEndOfFrame();

            difficulty = DifficultyMemory.Instance.GetDifficultySettings();
        }

        _verticalBounceMultiplier = difficulty.boxVerticalMultiplier;

        _horizontalBounceMultiplier = difficulty.boxHorizontalMultiplier;
    }
}
