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
        videoPlayer.loopPointReached += EndReached;
    }

    private void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        // Do something
        pauseMenu.Finish();
    }

   public void ToReuse()
    {
        SceneManager.LoadScene("Reuse");
    }
}
