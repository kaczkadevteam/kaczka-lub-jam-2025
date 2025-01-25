using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

    [SerializeField]
    private int maxBabyHealth = 100;
    public int MaxBabyHealth => maxBabyHealth;


    private int babyHealth;
    public int BabyHealth => babyHealth;

    private readonly Dictionary<string, int> damageSourcesDictionary = new();

    private string deathReasonText = string.Empty;
    public string DeathReasonText => deathReasonText;

    public void DecreaseHealth(EnemySO enemyScriptable)
    {
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

        switch (enemyTypeThatDealtMostDamage)
        {
            case "Bacteria":
                deathReasonText = "B¹belek umar³ na gruŸlicê";
                break;
            case "Dirt":
                deathReasonText = "B¹belek umar³ na alergiê od syfu";
                break;
            case "Virus":
                deathReasonText = "B¹belek umar³ na covid";
                break;
        }

        StartCoroutine(SlowGameOnLose());
    }

    private IEnumerator SlowGameOnLose()
    {
        while (Time.timeScale > GlobalConfig.Instance.timeScaleValueToStopCoroutine)
        {
            Time.timeScale /= 2;
            yield return new WaitForSeconds(GlobalConfig.Instance.secondsBetweenTimeScaleDivision);
        }

        Time.timeScale = 0;
    }
}