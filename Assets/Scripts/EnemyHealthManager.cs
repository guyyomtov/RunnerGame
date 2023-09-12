using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    private int currentHealth;

    [SerializeField] private int deadSound;
    [SerializeField] private GameObject deathEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            AudioManager.Instance.PlaySFX(deadSound);
            Destroy(gameObject);
        }
        GameManager.Instance.BouncePlayer();
        Instantiate(deathEffect, transform.position + new Vector3(0f, 1.5f,0f), transform.rotation);
    }
}
