using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action<int> OnRockAdded = (int v) => { };

    public Action<int> OnCoinAdded = (int v) => { };

    public Action<float> OnTimeChange = (float v) => { };

   

    [Header("Gameplay")]

    [SerializeField] private Hero _hero;

    [SerializeField] private Goal _goal;

    [SerializeField] private Coin _coinPrefab;

    private float _gameTime;

    private int _rocksNeededForCoin;

    private int _rocksInGoal;

    private int _coinsPicked;

    private float _gravity;

    [Header("Level")]

    [SerializeField] private Transform _leftLimit;

    [SerializeField] private Transform _rightLimit;

    [SerializeField] private Transform _floor;

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

        StartCoroutine(StartGame());
    }

    public float GetGravity()
    {
        return _gravity;
    }

    public Vector3 GetGoalPosition()
    {
        return _goal.transform.position;
    }

    public Transform GetHeroBoxTransform()
    {
        return _hero.GetBoxTransform();
    }

    public float GetFloorHeight()
    {
        return _floor.position.y + (_floor.localScale.y / 2);
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
        spawnPos.y = _hero.transform.position.y;

        Coin coin = null;
        if (Vector3.Distance(spawnPos, _hero.transform.position) < 2)
        {
            while (Vector3.Distance(spawnPos, _hero.transform.position) < 2)
            {
                spawnPos.x = UnityEngine.Random.Range(_leftLimit.position.x + _leftLimit.localScale.x, _rightLimit.position.x - _rightLimit.localScale.x);
            }
        }

        coin = Instantiate(_coinPrefab, spawnPos, Quaternion.identity);

        var difficulty = DifficultyMemory.Instance.GetDifficultySettings();

        coin.SetDuration(difficulty.coinsDuration);

        coin.OnCoinPicked += AddCoin;
    }

    private void AddCoin()
    {
        _coinsPicked++;

        OnCoinAdded?.Invoke(_coinsPicked);
    }

    IEnumerator StartGame()
    {
        var difficulty = DifficultyMemory.Instance.GetDifficultySettings();

        while (!difficulty)
        {
            yield return new WaitForEndOfFrame();

            difficulty = DifficultyMemory.Instance.GetDifficultySettings();
        }

        _gravity = difficulty.gravity;

        _gameTime = difficulty.gameTime;

        _rocksNeededForCoin = difficulty.rocksNeededForCoin;

        var wait = new WaitForSeconds(1);
        while (_gameTime >= 0)
        {
            OnTimeChange(_gameTime);
            yield return wait;
            _gameTime--;
        }

        Debug.Log("endgame");
    }
}
