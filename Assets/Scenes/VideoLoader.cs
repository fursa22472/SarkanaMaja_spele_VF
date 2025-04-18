using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;


public class VideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad = "SarkanaMajaIstais"; // <-- replace this with your real game scene name

    private AsyncOperation asyncLoad;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
        StartCoroutine(PreloadGameScene());
    }

    IEnumerator PreloadGameScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;
        yield return asyncLoad;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        asyncLoad.allowSceneActivation = true;
    }
}
