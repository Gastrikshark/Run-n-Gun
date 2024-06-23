using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealth(float health)
    {
        healthText.text = "Health: " + health.ToString();
    }

    public void UpdateCoins(float coins)
    {
        coinText.text = "Coins: " + coins.ToString();
    }
}
