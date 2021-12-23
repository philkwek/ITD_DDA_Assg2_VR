using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    Slider bgmSlider;
    [SerializeField]
    Slider soundEffectSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeVolume()
    {
        AudioListener.volume = bgmSlider.value; //make the game bgm volume equals to slider value
        AudioListener.volume = soundEffectSlider.value; //make the game sound effects volume equals to slider value
    }
}
