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

    public void SaveEnemyKillStatistic(EnemySO enemyScriptable)
    {
        if(!enemyKillStatistics.TryAdd(enemyScriptable.enemyName, 1))
        {
            enemyKillStatistics[enemyScriptable.enemyName]++;
        }
    }

    public void DecreaseHealth(EnemySO enemyScriptable)
    {
        babyHealth--;
        PlaySound();
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

        //switch (enemyTypeThatDealtMostDamage)
        //{
        //    case "Bacteria":
        //        deathReasonText = "B�belek umar� na gru�lic�";
        //        break;
        //    case "Dirt":
        //        deathReasonText = "B�belek umar� na alergi� od syfu";
        //        break;
        //    case "Virus":
        //        deathReasonText = "B�belek umar� na covid";
        //        break;
        //}
    }
    
    private void PlaySound()
    {
        var sound = babySounds[Random.Range(0, babySounds.Count)];
        SoundManager.Instance.PlaySound(sound, transform, 1f);
    }
}