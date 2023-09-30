using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;
    private AudioSource musicSource;
    [SerializeField] private float musicVolume = 0.5f;
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";



    private void Awake()
    {
        Instance = this;
        musicSource = GetComponent<AudioSource>();

        //get volume from player prefs and set to audio source
        musicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.5f);
        musicSource.volume = musicVolume;
    }



    public virtual void SetVolume()
    {
        musicVolume += 0.1f;

        if (musicVolume >= 1.1f)
        {
            musicVolume = 0f;
        }
        //set audio source
        musicSource.volume = musicVolume;

        //store preferences
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicVolume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return musicVolume;
    }


}
