using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    public Slider ss;
    public Slider sfx;
    public AudioSource sound;
    public AudioSource beat;
    public AudioSource BGMGAME;

    public List<AudioSource> SFX = new List<AudioSource>();

    private float musicVolume = 1f;
    private float sfxVolume = 1f;
    public bool BGMINGAME;
    public bool InGame;
    public bool MainMenu;
    public bool notplaying;
    public bool isnotplaying;
    private bool sorak = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SFX.Clear();
        if (MainMenu)
        {
            beat.Play();
        }
    }

    private void playsorak()
    {
        if (sorak)
        {
            sorak = false;
            sound.Play();
            Debug.Log("SORAK");
        }
    }

    private void Update()
    {
        sound.volume = musicVolume;
        beat.volume = sfxVolume;
        BGMGAME.volume = musicVolume;
        int countSFX = SFX.Count;

        foreach (AudioSource sffx in SFX)
        {
            sffx.volume = sfxVolume;
        }
        if (MainMenu)
        {
            if (beat.isPlaying)
            {
                Invoke("playsorak", 1);
            }
        }

        if (InGame)
        {
            if (BGMINGAME)
            {
                if (!isnotplaying)
                {
                    GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

                    foreach (GameObject go in allObjects)
                    {
                        if (go.CompareTag("SFX"))
                        {
                            AudioSource ob = go.GetComponent<AudioSource>();

                            SFX.Add(ob);
                        }
                    }
                    isnotplaying = true;
                    BGMGAME.Play();
                }
            }
        }

        UpdateVolume();
    }

    public void UpdateVolume()
    {
        if (InGame)
        {
            sfxVolume = sfx.value;
        }

        musicVolume = ss.value;
    }
}