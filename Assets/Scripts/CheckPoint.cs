using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject cpOn, cpOff;
    [SerializeField] public CheckPoint[] checkPoints;
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
            GameManager.Instance.RespawnPos = transform.position;
            checkPoints = FindObjectsOfType<CheckPoint>();
            checkPoints.ToList().ForEach(checkpoint =>
            {
                checkpoint.cpOff.SetActive(true);
                checkpoint.cpOn.SetActive(false);
            });
            cpOn.SetActive(true);
            cpOff.SetActive(false);
            AudioManager.Instance.PlaySFX(soundToPlay);
        }
    }
}
