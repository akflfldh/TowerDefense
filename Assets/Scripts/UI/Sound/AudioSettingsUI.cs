using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{



    [SerializeField]
    private AudioSettingsManager audioSettingsManager;


    [SerializeField]
    private Slider bgmSlider;


    [SerializeField]
    private Slider sfxSlider;



    private void OnEnable()
    {
        if (audioSettingsManager == null | bgmSlider == null || sfxSlider == null)
            return;


        bgmSlider.SetValueWithoutNotify(audioSettingsManager.BGMVolume);

        sfxSlider.SetValueWithoutNotify(audioSettingsManager.SFXVolume);


        bgmSlider.onValueChanged.AddListener(
          audioSettingsManager.SetBGMVolume
      );

        sfxSlider.onValueChanged.AddListener(
            audioSettingsManager.SetSFXVolume
        );

    }

    private void OnDisable()
    {


        if (audioSettingsManager == null )
            return;




        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.RemoveListener(
          audioSettingsManager.SetBGMVolume
      );

        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveListener(
                audioSettingsManager.SetSFXVolume
            );
        }


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
