using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // 씬에 있는 모든 오디오 소스를 가질 딕셔너리
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    #region Singleton
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion

    private void AddSound(string soundName, AudioSource audioSource)
    {
        if (!audioSources.ContainsKey(soundName))
        {
            audioSource.volume = 0.2f;
            audioSources.Add(soundName, audioSource);
        }
        else
        {
            Debug.LogWarning($"{soundName} 와 같은 이름의 오디오 소스가 이미 존재합니다.");
        }
    }


    public void AddAllSounds(GameObject soundSources)
    {
        AudioSource[] sources = soundSources.GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource source in sources)
        {
            AddSound(source.gameObject.name, source);
        }
    }


    public void PlaySound(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
        {
            audioSources[soundName].Play();
        }
        else
        {
            Debug.LogWarning($"{soundName}와 같은 이름의 오디오 소스가 존재하지 않습니다.");
        }
    }
    public bool IsPlaying(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
            return audioSources[soundName].isPlaying;

        else
            return false;
    }

    public void StopSound(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
            audioSources[soundName].Stop();
    }

    public void RemoveSound(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
            audioSources.Remove(soundName);
    }

    public void ClearAllSounds()
    {
        audioSources.Clear();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ClearAllSounds();
    }
}
