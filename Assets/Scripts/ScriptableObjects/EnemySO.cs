using UnityEngine;

[CreateAssetMenu]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    public float moveSpeed;
    public float damageToPlayer;
    public float dropChance;
    public int health;
}
