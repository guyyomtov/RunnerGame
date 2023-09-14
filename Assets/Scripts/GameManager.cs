using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private int totalCoins;
    [SerializeField] private bool isGameOver;
    private static int currentLevel = 1;
    [SerializeField] private int totalLevels = 3;


    public Vector3 RespawnPos { get; set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            RespawnPos = playerController.startTrasform;
            isGameOver = false;
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
        AudioManager.Instance.PlayMusic(currentLevel - 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public async UniTask Respawn()
    {
        playerController.gameObject.SetActive(false);
        CameraController.Instance.getCinemaBrain().enabled = false;
        UIManager.Instance.fadeToBlack = true;
        Instantiate(deathEffect, playerController.transform.position + new Vector3(0f,1f,0f), playerController.transform.rotation);
        //wait for 2 seconds
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        if (isGameOver || HealthManager.Instance.GetCurrentHealth() == 0)
        {
            await GameOverLose();
            return;
        }
        UIManager.Instance.fadeFromBlack = true;
        Debug.Log(RespawnPos);
        playerController.transform.position = RespawnPos;
        playerController.gameObject.SetActive(true);
        CameraController.Instance.getCinemaBrain().enabled = true;
    }
    public void AddCoins(int coinsToAdd)
    {
        totalCoins += coinsToAdd;
        UIManager.Instance.coinsText.text = totalCoins.ToString();
    }

    public void UpdateLevelText()
    {
        UIManager.Instance.coinsText.text = $"LEVEL {currentLevel.ToString()}";
    }
    //I did that function because I didnt want to make the PlayerController singleton so I expose what I need about the player from the GameManager
    public Transform PlayerCurrentTranform
    {
        get
        {
            return playerController.transformPlayer;
        }
    }
    public void BouncePlayer()
    {
        playerController.Bounce();
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    } 
    public async UniTask GameOverLose()
    {
        Debug.Log("starting game over processing");
        isGameOver = true;
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        Debug.Log($"current health: {HealthManager.Instance.GetCurrentHealth()}");
        if (HealthManager.Instance.GetCurrentHealth() == 0)
        {
            Debug.Log("Going to load main menu");
            SceneManager.LoadScene(0);
        }
    }
    public async UniTask GameOverVictory(int levelEndMusic)
    {
        Debug.Log("starting game over processing");
        AudioManager.Instance.PlayMusic(levelEndMusic);
        isGameOver = true;
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        Debug.Log($"current health: {HealthManager.Instance.GetCurrentHealth()}");
        currentLevel += 1;
        if (currentLevel > totalLevels)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log($" current level {currentLevel}");
            SceneManager.LoadScene(currentLevel);
        }
        
    }

}
