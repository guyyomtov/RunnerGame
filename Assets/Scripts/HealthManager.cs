using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static HealthManager Instance { get; private set; }
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private float invincibleLength = 2f;
    [SerializeField] private float invincibleCounter;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int soundToPlay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentHealth = maxHealth;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter < 1f)
            {
                playerController.gameObject.SetActive(true);
            }
        }
    }

    public async UniTask Hurt()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.Instance.Respawn();
            }
            else
            {
                playerController.KnockBack();
                invincibleCounter = invincibleLength;
                playerController.gameObject.SetActive(false);
               // await playerController.FlashPlayer();
            }
            AudioManager.Instance.PlaySFX(soundToPlay);
            UpdateUIHealthText();
        }
       
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateUIHealthText();
    }
    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateUIHealthText();
    }

    public void UpdateUIHealthText()
    {
        UIManager.Instance.healthText.text = currentHealth.ToString();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public void IncreaseHealth()
    {
        currentHealth -= 1;
        UpdateUIHealthText();
    }
}
