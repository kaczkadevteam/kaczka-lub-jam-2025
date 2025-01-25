using UnityEngine;

[CreateAssetMenu]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    public float moveSpeed = 1f;
    public float damageToPlayer = 1f;
    public float dropChance = 1f;
    public int health =1;
    [Header("Rarity / Spawn Weight (higher means more frequent)")]
    public float spawnWeight = 1f;

}
