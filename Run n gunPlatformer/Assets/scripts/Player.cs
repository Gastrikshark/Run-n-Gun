using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float health = 100f;
    public float maxHealth = 100f; 
    public float coins = 10;

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

        
        LoadCoins();
    }

    void Start()
    {
        UIManager.instance.UpdateHealth(health);
        UIManager.instance.UpdateCoins(coins);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UIManager.instance.UpdateHealth(health);

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth; 
        }
        UIManager.instance.UpdateHealth(health);
    }

    public void AddCoins(float amount)
    {
        coins += amount;
        UIManager.instance.UpdateCoins(coins);
        SaveCoins(); 
    }

    void Die()
    {
        Debug.Log("Player Died");
        Destroy(gameObject);
    }

    void OnApplicationQuit()
    {
        
        SaveCoins();
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetFloat("PlayerCoins", coins);
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        if (PlayerPrefs.HasKey("PlayerCoins"))
        {
            coins = PlayerPrefs.GetFloat("PlayerCoins");
        }
    }

}
