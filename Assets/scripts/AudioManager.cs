using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip[] ambientMusicTracks;
    public AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        foreach (var clip in sfxClips)
        {
            sfxDict[clip.name] = clip;
        }
        PlayAmbientSound();
    }

    public void PlayAmbientSound()
    {
        if (ambientMusicTracks.Length > 0)
        {
            int randomIndex = Random.Range(0, ambientMusicTracks.Length);
            musicSource.clip = ambientMusicTracks[randomIndex];
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void RestartMusicAfterDelay(float delay)
    {
        StartCoroutine(RestartMusicCoroutine(delay));
    }

    private IEnumerator RestartMusicCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayAmbientSound();
    }

    public void PlaySound(string sfxName)
    {
        if (sfxDict.TryGetValue(sfxName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found: " + sfxName);
        }
    }
}
