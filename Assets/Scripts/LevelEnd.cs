using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim;
    [SerializeField] private int levelEndMusic = 8;
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
            anim.SetTrigger("Hit");
            GameManager.Instance.GameOver(levelEndMusic);
        }
    }
}
