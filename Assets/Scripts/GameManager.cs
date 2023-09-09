using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private GameObject deathEffect;
    private int totalCoins;
    public Vector3 RespawnPos { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            RespawnPos = PlayerController.startTrasform;
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
        AddCoins(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public async UniTask Respawn()
    {
        PlayerController.gameObject.SetActive(false);
        CameraController.Instance.getCinemaBrain().enabled = false;
        UIManager.Instance.fadeToBlack = true;
        Instantiate(deathEffect, PlayerController.transform.position + new Vector3(0f,1f,0f), PlayerController.transform.rotation);
        //wait for 2 seconds
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        UIManager.Instance.fadeFromBlack = true;
        HealthManager.Instance.ResetHealth();
        Debug.Log(PlayerController.startTrasform);
        PlayerController.transform.position = RespawnPos;
        PlayerController.gameObject.SetActive(true);
        CameraController.Instance.getCinemaBrain().enabled = true;
    }

    public void AddCoins(int coinsToAdd)
    {
        totalCoins += coinsToAdd;
        UIManager.Instance.coinsText.text = totalCoins.ToString();
    }
}
