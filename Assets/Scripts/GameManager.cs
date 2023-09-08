using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    public PlayerController PlayerController;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public async UniTask Respawn()
    {
        PlayerController.gameObject.SetActive(false);
        //wait for 2 seconds
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        Debug.Log(PlayerController.startTrasform);
        PlayerController.transform.position = PlayerController.startTrasform;
        PlayerController.gameObject.SetActive(true);
    }
    
}
