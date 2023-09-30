using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    [SerializeField] private float soundEffectsVolume = 1f;
    private const string PLAYER_PREFS_SFX_VOLUME = "SoundEffectsVolume";


    private void Awake()
    {
        Instance = this;

        //grab volume from player prefs
        soundEffectsVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f);
    }



    public virtual void SetVolume()
    {
        soundEffectsVolume += 0.1f;

        if (soundEffectsVolume >= 1.1f)
        {
            soundEffectsVolume = 0f;
        }

        //store volume in player prefs
        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, soundEffectsVolume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return soundEffectsVolume;
    }



    //TODO: when playing audio, volume = volume * multiplier

    
}
