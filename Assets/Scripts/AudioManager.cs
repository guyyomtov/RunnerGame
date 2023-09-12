using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager Instance { get; private set; }
    [SerializeField] public AudioSource[] music;
    [SerializeField] public AudioSource[] sfx;
    [SerializeField] public int levelMusic;
    [SerializeField] public int currentTrack;
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
        PlayMusic(levelMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(int musicToPlay)
    {
        music[currentTrack].Stop();
        music[musicToPlay].Play();
        currentTrack = musicToPlay;
        Debug.Log("play music");
    }
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }
}
