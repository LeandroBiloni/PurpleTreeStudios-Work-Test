using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action<int> OnRockAdded = (int v) => { };

    public Action<int> OnCoinAdded = (int v) => { };

    public Action<float> OnTimeChange = (float v) => { };

    [Header("Physics")]
    [Tooltip("Positive value.")]
    [Min(0)]
    [SerializeField] private float _gravity;

    [Header("Gameplay")]

    [SerializeField] private Transform _hero;

    [SerializeField] private Goal _goal;

    [SerializeField] private float _gameTime;

    [SerializeField] private Coin _coinPrefab;

    [SerializeField] private int _rocksNeededForCoin;

    private int _rocksInGoal;

    private int _coinsPicked;

    [Header("Level")]

    [SerializeField] private Transform _leftLimit;

    [SerializeField] private Transform _rightLimit;

    // Start is called before the first frame update
    private void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _goal.OnRockReachedGoal += AddRock;

        StartCoroutine(GameTimer());
    }

    public float GetGravity()
    {
        return _gravity;
    }

    public Vector3 GetGoalPosition()
    {
        return _goal.transform.position;
    }

    private void AddRock()
    {
        _rocksInGoal++;

        OnRockAdded?.Invoke(_rocksInGoal);

        if (_rocksInGoal%_rocksNeededForCoin == 0)
        {
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        var spawnPos = Vector3.zero;

        spawnPos.x = UnityEngine.Random.Range(_leftLimit.position.x + _leftLimit.localScale.x, _rightLimit.position.x - _rightLimit.localScale.x);
        spawnPos.y = _hero.position.y;

        Coin coin = null;
        if (Vector3.Distance(spawnPos, _hero.position) < 2)
        {
            while (Vector3.Distance(spawnPos, _hero.position) < 2)
            {
                spawnPos.x = UnityEngine.Random.Range(_leftLimit.position.x + _leftLimit.localScale.x, _rightLimit.position.x - _rightLimit.localScale.x);
            }
        }

        coin = Instantiate(_coinPrefab, spawnPos, Quaternion.identity);

        coin.OnCoinPicked += AddCoin;
    }

    private void AddCoin()
    {
        _coinsPicked++;

        OnCoinAdded?.Invoke(_coinsPicked);
    }

    IEnumerator GameTimer()
    {
        yield return new WaitForEndOfFrame();

        var wait = new WaitForSeconds(1);
        while(_gameTime >= 0)
        {
            OnTimeChange(_gameTime);
            yield return wait;
            _gameTime--;
        }

        Debug.Log("endgame");
    }
}
