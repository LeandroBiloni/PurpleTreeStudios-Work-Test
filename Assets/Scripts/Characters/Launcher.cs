using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private Rock _rockPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _throwSpeedMin;
    [SerializeField] private float _throwSpeedMax;
    [SerializeField] private float _throwAngleMin;
    [SerializeField] private float _throwAngleMax;

    [Min(0)]
    [SerializeField] private float _throwSpacingMin;
    [SerializeField] private float _throwSpacingMax;
    private float _timeForNextThrow;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _timeForNextThrow = Random.Range(_throwSpacingMin, _throwSpacingMax);

        StartCoroutine(RockThrowTimer());
    }

    //Called by animation event
    private void ThrowRock()
    {
        var rock = Instantiate(_rockPrefab, _spawnPosition.position, Quaternion.identity);

        var randomSpeed = Random.Range(_throwSpeedMin, _throwSpeedMax);

        var randomAngle = Random.Range(_throwAngleMin, _throwAngleMax);

        rock.SetAndInitialize(randomSpeed, randomAngle, GameManager.Instance.GetGravity(), GameManager.Instance.GetGoalPosition());
    }


    IEnumerator RockThrowTimer()
    {
        yield return new WaitForSeconds(_timeForNextThrow);

        _animator.SetTrigger("Throw");

        _timeForNextThrow = Random.Range(_throwSpacingMin, _throwSpacingMax);

        StartCoroutine(RockThrowTimer());
    }
}
