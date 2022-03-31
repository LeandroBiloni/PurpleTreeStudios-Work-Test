using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI _rocksCounter;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _coinsCounter;
    [SerializeField] private RectTransform _coinsIcon;
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

        StartCoroutine(StartDelay());
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

    IEnumerator StartDelay()
    {
        yield return new WaitForEndOfFrame();

        GameManager.Instance.OnRockAdded += UpdateRocksCounter;
        GameManager.Instance.OnTimeChange += UpdateTimer;
        GameManager.Instance.OnCoinAdded += UpdateCoinsCounter;
    }

    public RectTransform GetCoinsUIRect()
    {
        return _coinsIcon;
    }
}
