using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour, IControllable
{
    [SerializeField] private Rock _rockPrefab;
    [SerializeField] private Transform _spawnPosition;
    private float _throwSpeedMin;
    private float _throwSpeedMax;
    private float _throwAngleMin;
    private float _throwAngleMax;
    private float _throwSpacingMin;
    private float _throwSpacingMax;
    private float _timeForNextThrow;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        GameManager.Instance.AddControllable(this);
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

    public void EnableControl()
    {
        var difficulty = DifficultyMemory.Instance.GetDifficultySettings();

        _throwAngleMin = difficulty.launcherThrowAngleMin;
        _throwAngleMax = difficulty.launcherThrowAngleMax;

        _throwSpeedMin = difficulty.launcherThrowSpeedMin;
        _throwSpeedMax = difficulty.launcherThrowSpeedMax;

        _throwSpacingMin = difficulty.launcherThrowSpacingMin;
        _throwSpacingMax = difficulty.launcherThrowSpacingMax;

        _timeForNextThrow = Random.Range(_throwSpacingMin, _throwSpacingMax);

        StartCoroutine(RockThrowTimer());
    }

    public void DisableControl()
    {
        StopAllCoroutines();
    }
}
