using UnityEngine;

public class GlobalConfig : MonoBehaviour
{
    private static GlobalConfig _instance;

    public static GlobalConfig Instance => _instance;

    public Transform blower;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Example configuration fields
    public float blowerOrbitingRadius = 10f;
    public float bubbleSpawnOffsetFraction = 0.4f;
}