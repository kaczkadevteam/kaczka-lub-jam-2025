using UnityEngine;

public class VirusEvolutionManager : MonoBehaviour
{
    private static VirusEvolutionManager _instance;

    public static VirusEvolutionManager Instance => _instance;
    [SerializeField] public EnemySO enemySO;
    public float moveSpeed;
    public float damageToPlayer;
    public float spawnWeight;
    public float size;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            InitStats(enemySO);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void InitStats(EnemySO enemySO)
    {
        moveSpeed = enemySO.moveSpeed;
        damageToPlayer = enemySO.damageToPlayer;
        spawnWeight = enemySO.spawnWeight;
        size = enemySO.size;
    }
}
