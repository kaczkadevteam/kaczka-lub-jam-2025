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
                deathReasonText = "B�belek umar� na gru�lic�";
                break;
            case "Dirt":
                deathReasonText = "B�belek umar� na alergi� od syfu";
                break;
            case "Virus":
                deathReasonText = "B�belek umar� na covid";
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