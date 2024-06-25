using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int secondGunCost = 50;
    public GameObject shopUI;
    public TextMeshProUGUI gunInfoText;

    private Player player;
    private Shooting shooting;

    void Start()
    {
        // pakt de insttance van de player en pakt de script shooting de op de player staat
        player = Player.instance;
        shooting = player.GetComponent<Shooting>();

        if (shooting == null)
        {
            Debug.LogError("Shooting component not found on Player!");
        }
        // zet de shop UI op inactive
        shopUI.SetActive(false);
        UpdateGunInfoText();
    }

    void OnTriggerEnter2D(Collider2D other)
    {// roopt de UpdateGunInfoText als de player in de boxcollider loopt Van de shopmanager
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(true);
            UpdateGunInfoText();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {// set de UpdateGunInfoText  uit als de player uit de boxcollider van de shopmanager loopt 
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(false);
        }
    }

    public void PurchaseSecondGun()
    {// kijkt of de player genoeg coins en aan de isSeconderyGunUNLOCKED voldoet en set
     // de 2de gun op active zo dat de player hem kan gebruiken de funtie zet dan ook
     // de shop ui uit zo dat die niet meer in de weg staat
        if (player.coins >= secondGunCost && !shooting.isSecondaryGunUnlocked)
        {
            player.coins -= secondGunCost;
            UIManager.instance.UpdateCoins(player.coins);

            shooting.isSecondaryGunUnlocked = true;
            shooting.secondaryGun.SetActive(true);
            UpdateGunInfoText();

            shopUI.SetActive(false);
        }
    }

    void UpdateGunInfoText()
    {// print deze text als de player de gun wilt kopen en aan de condzie is secondaryGunUnlocked voldoet
     // anders print het de prijs
        if (shooting.isSecondaryGunUnlocked)
        {
            gunInfoText.text = "Second Gun: Unlocked";
        }
        else
        {
            gunInfoText.text = "Second Gun: $" + secondGunCost;
        }
    }
}
