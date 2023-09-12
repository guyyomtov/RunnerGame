using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int coinsWorth;
    [SerializeField] private int soundToPlay;
    [SerializeField] private GameObject coinEffect;
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
            GameManager.Instance.AddCoins(coinsWorth);
            Destroy(gameObject);
            Instantiate(coinEffect, transform.position, transform.rotation);
            AudioManager.Instance.PlaySFX(soundToPlay);
        }
    }
}
