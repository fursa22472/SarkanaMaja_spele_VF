using UnityEngine;

public class SceneMusicTrigger : MonoBehaviour
{
    public AudioClip sceneMusic;

    void Start()
    {
        PersistentMusicManager.Instance?.PlayMusic(sceneMusic);
    }
}
