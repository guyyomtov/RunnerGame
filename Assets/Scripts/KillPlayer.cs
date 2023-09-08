using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        if (other.tag == eCharacters.Player.ToString())
        {
            Debug.Log("here1");
            GameManager.Instance.Respawn();
        }
    }
}
