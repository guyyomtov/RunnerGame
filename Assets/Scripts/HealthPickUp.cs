using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int healthAmount;
    [SerializeField] private bool isFullHeal;
    [SerializeField] private GameObject healthEffect;
    [SerializeField] private int soundToPlay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == eCharacters.Player.ToString())
        {
            Destroy(gameObject);
            Instantiate(healthEffect, transform.position, transform.rotation);
            if (isFullHeal)
            {
                HealthManager.Instance.ResetHealth();
            }
            else
            {
                HealthManager.Instance.AddHealth(healthAmount);
            }
            AudioManager.Instance.PlaySFX(soundToPlay);
        }
    }
}
