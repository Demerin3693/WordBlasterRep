using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfxSource;
    public AudioClip laserSFX;
    public AudioClip explosionSFX;
    public AudioClip clickSFX;
    public AudioClip slowdownSFX;
    public AudioClip defeatSFX;

    void Awake()
    {
        instance = this;
    }

    public void PlayLaser() => sfxSource.PlayOneShot(laserSFX);
    public void PlayExplosion() => sfxSource.PlayOneShot(explosionSFX);
    public void PlayClick() => sfxSource.PlayOneShot(clickSFX);
    public void PlaySlowdown() => sfxSource.PlayOneShot(slowdownSFX);
    public void PlayDefeat() => sfxSource.PlayOneShot(defeatSFX);

}
