using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{

    private const string BGMKey = "BGM";
    private const string SFXKey = "SFX";


    [SerializeField]
    private AudioMixer audioMixer;

    public float BGMVolume { get; private set; } = 0.8f;
    public float SFXVolume { get; private set; } = 0.8f;


    private void Awake()
    {

        SFXVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(BGMKey, 0.8f));
        BGMVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(SFXKey, 0.8f));

    }

   void OnDisable()
    {
        PlayerPrefs.Save();

    }

    
    private void ApplyVolume(string parameter ,float volume)
    {
        if (audioMixer == null)
            return;


        float decibels = ToDecibels(volume);


        audioMixer.SetFloat(parameter, decibels);
    }

    public void SetBGMVolume(float value)
    {
        BGMVolume = Mathf.Clamp01(value);

        ApplyVolume(BGMKey, BGMVolume);

        PlayerPrefs.SetFloat(BGMKey, BGMVolume);

    }


    void Start()
    {


        ApplyVolume(BGMKey, BGMVolume);
        ApplyVolume(SFXKey, SFXVolume);

    }


    public void SetSFXVolume(float value)
    {
        SFXVolume = Mathf.Clamp01(value);

        ApplyVolume(SFXKey, SFXVolume);

        PlayerPrefs.SetFloat(SFXKey, SFXVolume);

    }

    private static float ToDecibels(float volume)
    {
        if (volume <= 0.0001f)
            return -80f;

        return Mathf.Log10(volume) * 20f;
    }


    // Update is called once per frame
    void Update()
    {

    }



    private void OnApplicationPause(bool paused)
    {
        if (paused)
            PlayerPrefs.Save();
    }
}
