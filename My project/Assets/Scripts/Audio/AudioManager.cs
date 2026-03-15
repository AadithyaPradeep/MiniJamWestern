using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

public static AudioManager master {get; private set;}

public SFXManager SFX {get; private set;}
public MXManager MX {get; private set;}

[Header("Mixers")]
[SerializeField] private AudioMixerGroup sFXMixer;
public AudioMixerGroup SFXMixer => sFXMixer;
[SerializeField] private AudioMixerGroup mXMixer;
public AudioMixerGroup MXMixer => mXMixer;


    private void Awake()
    {
        if (master != null && master != this)
        {
            Destroy(gameObject);
            return;
        }
        master = this;
        DontDestroyOnLoad(gameObject);


        SFX = GetComponentInChildren<SFXManager>();
        MX = GetComponentInChildren<MXManager>();

        if (SFX == null) Debug.LogError("AudioManager: SFX not found in child");
        if (MX == null) Debug.LogError("AudioManager: MX not found in child");
    }

}
