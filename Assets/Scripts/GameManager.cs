using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;
    [SerializeField] private List<AudioClip> babySounds;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        babyHealth = maxBabyHealth;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R) || Gamepad.current?.aButton.ReadValue() == 1 || Gamepad.current?.bButton.ReadValue() == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    [SerializeField]
    private int maxBabyHealth = 100;
    public int MaxBabyHealth => maxBabyHealth;


    private int babyHealth;
    public int BabyHealth => babyHealth;

    private readonly Dictionary<string, int> damageSourcesDictionary = new();

    public readonly Dictionary<string, int> enemyKillStatistics = new();

    private string deathReasonText = string.Empty;
    public string DeathReasonText => deathReasonText;

    private string extraDeathReasonText = string.Empty;
    public string ExtraDeathReasonText => extraDeathReasonText;

    public void SaveEnemyKillStatistic(EnemySO enemyScriptable)
    {
        if(!enemyKillStatistics.TryAdd(enemyScriptable.enemyName, 1))
        {
            enemyKillStatistics[enemyScriptable.enemyName]++;
        }
    }

    public void DecreaseHealth(EnemySO enemyScriptable)
    {
        if (babyHealth > 0) {
            PlaySound();
        }
        
        babyHealth--;

        if(!damageSourcesDictionary.TryAdd(enemyScriptable.enemyName, 1))
        {
            damageSourcesDictionary[enemyScriptable.enemyName]++;
        }

        if(babyHealth <= 0)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        var enemyTypeThatDealtMostDamage = damageSourcesDictionary.OrderByDescending(smth => smth.Value).First().Key;

        deathReasonText = "BOMBELEK DIED";

        switch (enemyTypeThatDealtMostDamage)
        {
            case "Bacteria":
                extraDeathReasonText = "DUE TO TUBERCOLOSIS";
                break;
            case "Dirt":
                extraDeathReasonText = "DUE TO CHOKING ON DIRT";
                break;
            case "Virus":
                extraDeathReasonText = "DUE TO COVID";
                break;
        }
    }

    private void PlaySound()
    {
        var sound = babySounds[Random.Range(0, babySounds.Count)];
        SoundManager.Instance.PlaySound(sound, transform, 1f);
    }
}