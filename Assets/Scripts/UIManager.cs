using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
        gameLostText.gameObject.SetActive(false);
        StartCoroutine(UIUpdate());
    }

    private IEnumerator UIUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            
            //health
            healthBar.value = GameManager.Instance.BabyHealth;

            //upgrades
            var enemyKillStatistics = GameManager.Instance.enemyKillStatistics;
            dirtCleanedText.text = $"{(enemyKillStatistics.TryGetValue("Dirt",out var dirtCount) ? dirtCount : 0)} Dirt";
            virusCleanedText.text = $"{(enemyKillStatistics.TryGetValue("Virus", out var virusCount) ? virusCount : 0)} Virus";
            bacteriaCleanedText.text = $"{(enemyKillStatistics.TryGetValue("Bacteria", out var bacteriaCount) ? bacteriaCount : 0)} Bacteria";

            //death message
            if (GameManager.Instance.DeathReasonText != string.Empty)
            {
                gameLostText.gameObject.SetActive(true);
                gameLostText.text = GameManager.Instance.DeathReasonText;
            }
        }
    }
}
