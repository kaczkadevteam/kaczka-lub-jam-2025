using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private TextMeshProUGUI gameLostText;

    private void Start()
    {
        healthBar.maxValue = GameManager.Instance.MaxBabyHealth;
        gameLostText.gameObject.SetActive(false);
    }

    void Update()
    {
        healthBar.value = GameManager.Instance.BabyHealth;
        if(GameManager.Instance.DeathReasonText != string.Empty)
        {
            gameLostText.gameObject.SetActive(true);
            gameLostText.text = GameManager.Instance.DeathReasonText;
        }
        
    }
}
