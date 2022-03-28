using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Tooltip("Positive value.")]
    [Min(0)]
    [SerializeField] private float _gravity;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetGravity()
    {
        return _gravity;
    }
}
