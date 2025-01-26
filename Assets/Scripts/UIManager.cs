using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private bool menuOpen = false;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject howToOpenMenu;
    [SerializeField]
    private GameObject howToCloseMenu;
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private TextMeshProUGUI gameLostText;
    [SerializeField]
    private TextMeshProUGUI dirtCleanedText;
    [SerializeField]
    private TextMeshProUGUI virusCleanedText;
    [SerializeField]
    private TextMeshProUGUI bacteriaCleanedText;

    private void Start()
    {
        healthBar.maxValue = GameManager.Instance.MaxBabyHealth;
        gameLostText.transform.parent.gameObject.SetActive(false);
        UpdateMenuState();
        StartCoroutine(UIUpdate());
    }

    private IEnumerator UIUpdate()
    {
        while (true)
        {
            //health
            healthBar.value = GameManager.Instance.BabyHealth;

            //upgrades
            var enemyKillStatistics = GameManager.Instance.enemyKillStatistics;
            dirtCleanedText.text = $"{(enemyKillStatistics.TryGetValue("Dirt", out var dirtCount) ? dirtCount : 0)} Dirt";
            virusCleanedText.text = $"{(enemyKillStatistics.TryGetValue("Virus", out var virusCount) ? virusCount : 0)} Virus";
            bacteriaCleanedText.text = $"{(enemyKillStatistics.TryGetValue("Bacteria", out var bacteriaCount) ? bacteriaCount : 0)} Bacteria";

            //death message
            if (GameManager.Instance.DeathReasonText != string.Empty)
            {
                gameLostText.transform.parent.gameObject.SetActive(true);
                gameLostText.text = GameManager.Instance.DeathReasonText;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Gamepad.current?.leftStick.ReadValue()[1] > 0.1)
        {
            CloseMenu();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Gamepad.current?.leftStick.ReadValue()[1] < -0.1)
        {
            OpenMenu();
        }
    }

    private void OpenMenu()
    {
        menuOpen = true;
        UpdateMenuState();

    }

    private void CloseMenu()
    {
        menuOpen = false;
        UpdateMenuState();
    }

    private void UpdateMenuState()
    {
        menu.SetActive(menuOpen);
        howToOpenMenu.SetActive(!menuOpen);
        howToCloseMenu.SetActive(menuOpen);
    }
}
