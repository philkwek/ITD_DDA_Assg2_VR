/******************************************************************************
Author: Eileen, Kelly, Elicia, Phil, Donavan
Name of Class: AudioManager
Description of Class: This script is to allow users to change the music and sound effects volume
Date Created: 24/12/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicAudioMixer; //reference audio mixer for bgm
    public AudioMixer sfxAudioMixer; //reference audio mixer for other sound effects


    public void SetMusicVolume(float volume)
    {
        //synchronize the music volume with the slider value
        musicAudioMixer.SetFloat("musicvolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        //synchronize the sfx volume with the slider value
        sfxAudioMixer.SetFloat("sfxvolume", volume);
    }
}
