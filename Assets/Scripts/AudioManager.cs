using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicAudioMixer; //audio mixer for bgm
    public AudioMixer sfxAudioMixer; //audio mixer for other sound effects


    public void SetMusicVolume(float volume)
    {
        musicAudioMixer.SetFloat("musicvolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxAudioMixer.SetFloat("sfxvolume", volume);
    }
}
