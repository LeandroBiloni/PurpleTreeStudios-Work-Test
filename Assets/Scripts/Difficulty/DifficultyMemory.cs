using UnityEngine;

public class DifficultyMemory : MonoBehaviour
{
    public static DifficultyMemory Instance;

    [SerializeField] private DifficultySO _difficultySettings;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDifficultySettings(DifficultySO settings)
    {
        _difficultySettings = settings;
    }

    public DifficultySO GetDifficultySettings()
    {
        return _difficultySettings;
    }
}
