/******************************************************************************
Author: Donavan
Name of Class: VideoManager
Description of Class: This script manages what happens after the video has 
finished playing
Date Created: 10/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public PauseMenu pauseMenu;

    void Start()
    {
        //When the video has ended, call the EndReached function
        videoPlayer.loopPointReached += EndReached;
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //Activate the finish menu
        pauseMenu.Finish();
    }

   public void ToReuse()
    {
        //Load the Reuse game scene
        SceneManager.LoadScene("Reuse");
    }
}
