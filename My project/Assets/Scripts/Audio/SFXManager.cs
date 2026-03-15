using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
[Header("SFX Pool Size")]
[SerializeField] private int poolSize = 10;
private AudioSource[] audioSourcePool;
private bool poolIsInitialized = false;
private int currentPoolIndex = 0;

//Pool Setup
private void InitializeSFXPool()
    {
        if (poolIsInitialized) return;
        
        audioSourcePool = new AudioSource[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
           GameObject sourceObject = new GameObject ($"PooledAudioSource_SFX_{i}");
           sourceObject.transform.SetParent(transform);
           audioSourcePool[i] = sourceObject.AddComponent<AudioSource>();
           audioSourcePool[i].playOnAwake = false;
        }

        poolIsInitialized = true;
    }

private AudioSource GetAvailableSource()
    {
        foreach (AudioSource source in audioSourcePool)
        {
            if (!source.isPlaying) return source;       
        }
        currentPoolIndex = (currentPoolIndex +1) % audioSourcePool.Length;
        audioSourcePool[currentPoolIndex].Stop();
        return audioSourcePool[currentPoolIndex];
    }

//Pool Initialize
private void Awake()
    {
        if(!poolIsInitialized) InitializeSFXPool();
    }

// Play Setup and Routing
private void Play(AudioClip clip, AudioSource source)
    {
        source.outputAudioMixerGroup = AudioManager.master.SFXMixer;
        source.clip = clip;
        source.Play();
    }

// Play Persistent Setup and Routing
public void PlayPersistent (AudioClip clip, AudioSource source)
    {
        GameObject persisentSFX = new GameObject("Persistent SFX");
        persisentSFX.transform.SetParent(transform);
        source = persisentSFX.AddComponent<AudioSource>();

        source.outputAudioMixerGroup = AudioManager.master.SFXMixer;
        source.clip = clip;
        source.Play();
    }

// 2D One Shot
public void Play2DOneShot(AudioClip clip)
    {
        if(clip == null) return;

        AudioSource source = GetAvailableSource();
        source.loop = false;
        source.spatialBlend = 0f;
        Play(clip, source);
    }

// 2D Loop
public void Play2DLoop (AudioClip clip)
    {
        if(clip == null) return;

        AudioSource source = GetAvailableSource();
        source.loop = true;
        source.spatialBlend = 0f;
        Play(clip, source);
    }

// 3D One Shot
public void Play3DOneShot (AudioClip clip, Vector3 position)
    {
        if(clip == null) return;

        AudioSource source = GetAvailableSource();
        source.loop = false;
        source.transform.position = position;
        source.spatialBlend = 1f;
        Play(clip, source);
    }

// 3D Loop
public void Play3DLoop (AudioClip clip, Vector3 position)
    {
        if(clip == null) return;

        AudioSource source = GetAvailableSource();
        source.loop = true;
        source.transform.position = position;
        source.spatialBlend = 1f;
        Play(clip, source);
    }

}
