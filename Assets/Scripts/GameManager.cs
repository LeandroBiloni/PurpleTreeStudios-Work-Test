using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Physics")]
    [Tooltip("Positive value.")]
    [Min(0)]
    [SerializeField] private float _gravity;

    [Header("Gameplay")]
    [SerializeField] private Goal _goal;

    [SerializeField] private float _gameTime;

    [SerializeField] private Coin _coinPrefab;

    [SerializeField] private int _rocksNeededForCoin;

    private int _rocksInGoal;

    // Start is called before the first frame update
    void Start()
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

        if (_rocksInGoal == _rocksNeededForCoin)
        {
            SpawnCoin();
            _rocksInGoal = 0;
        }
    }

    private void SpawnCoin()
    {
        Debug.Log("spawn coin");
    }
}
