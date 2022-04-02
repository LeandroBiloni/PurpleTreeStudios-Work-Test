using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Game Screen")]
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private TextMeshProUGUI _rocksCounter;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _coinsCounter;
    [SerializeField] private RectTransform _coinsIcon;

    [Header("End Screen")]
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private TextMeshProUGUI _endRocksCounter;
    [SerializeField] private TextMeshProUGUI _endCoinsCounter;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _timer.text = DifficultyMemory.Instance.GetDifficultySettings().gameTime.ToString();

        GameManager.Instance.OnRockAdded += UpdateRocksCounter;
        GameManager.Instance.OnTimeChange += UpdateTimer;
        GameManager.Instance.OnCoinAdded += UpdateCoinsCounter;
        GameManager.Instance.OnGameEnd += GameScreenOff;
        GameManager.Instance.OnGameEnd += EndScreenOn;
    }

    public void UpdateRocksCounter(int value)
    {
        _rocksCounter.text = "x " + value;
    }

    public void UpdateTimer(float value)
    {
        _timer.text = value.ToString();
    }

    public void UpdateCoinsCounter(int value)
    {
        _coinsCounter.text = "x " + value;
    }

    public RectTransform GetCoinsUIRect()
    {
        return _coinsIcon;
    }

    public void GameScreenOff()
    {
        _gameScreen.SetActive(false);
    }

    public void EndScreenOn()
    {
        _endRocksCounter.text = _rocksCounter.text;
        _endCoinsCounter.text = _coinsCounter.text;
        _endScreen.SetActive(true);
    }
}
