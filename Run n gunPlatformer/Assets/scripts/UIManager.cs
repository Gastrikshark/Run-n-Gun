using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI gunInfoText;
    public TextMeshProUGUI livesText;
    public GameObject dialogManagerUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void UpdateHealth(float health)
    {
        healthText.text = "Health: " + health.ToString();
    }

    public void UpdateCoins(float coins)
    {
        coinsText.text = "Coins: " + coins.ToString();
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void UpdateGunInfoText(bool isSecondaryGunUnlocked, int secondGunCost)
    {
        if (isSecondaryGunUnlocked)
        {
            gunInfoText.text = "Second Gun: Unlocked";
        }
        else
        {
            gunInfoText.text = "Second Gun: $" + secondGunCost;
        }
    }

}
