using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float health = 100f;
    public float maxHealth = 100f;
    public float coins = 10;
    public int lives = 3;
    private Vector3 spawnPoint;
    SoundManager audio;
    
    

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
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        LoadCoins();
        
    }

    void Start()
    {
        SetSpawnPoint();
        UIManager.instance.UpdateHealth(health);
        UIManager.instance.UpdateCoins(coins);
        UIManager.instance.UpdateLives(lives);
    }

    void SetSpawnPoint()
    {
        GameObject spawnObject = GameObject.Find("SpawnPoint");
        if (spawnObject != null)
        {
            spawnPoint = spawnObject.transform.position;
            transform.position = spawnPoint;
        }
        else
        {
            Debug.LogWarning("SpawnPoint not found in scene");
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UIManager.instance.UpdateHealth(health);
        // hij speelt hier de sound effect af
        audio.Playsoundeffect(audio.hit);

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
        lives--; 
        UIManager.instance.UpdateLives(lives); 

        if (lives > 0)
        {
            
            Respawn();
        }
        else
        {
            
            SceneManager.LoadScene("LevelSelect");
        }
    }

    void Respawn()
    {
        health = maxHealth;
        UIManager.instance.UpdateHealth(health);
        transform.position = spawnPoint;
        Debug.Log("Player Respawned");
    }


    void OnApplicationQuit()
    {
        SaveCoins();
    }

    public void DeductCoins(float amount)
    {
        coins -= amount;
        UIManager.instance.UpdateCoins(coins);
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
            Debug.Log("Loaded coins: " + coins);
        }
    }
}
