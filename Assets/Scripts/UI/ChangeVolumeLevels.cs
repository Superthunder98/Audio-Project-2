using UnityEngine;
using UnityEngine.UI;
using AK.Wwise;
using System.Collections;

public class ChangeVolumeLevels : MonoBehaviour
{
    public Slider thisSlider;

    public void SetVolume(string whatValue)
    {
        float sliderValue = thisSlider.value;

        if (whatValue == "SFX")
            AkSoundEngine.SetRTPCValue("Bus_Volume_SFX", sliderValue);
        
        if (whatValue == "Music")
            AkSoundEngine.SetRTPCValue("Bus_Volume_Music", sliderValue);

        if (whatValue == "Ambience")
            AkSoundEngine.SetRTPCValue("Bus_Volume_Ambience", sliderValue);

        if (whatValue == "Footsteps")
            AkSoundEngine.SetRTPCValue("Bus_Volume_Footsteps", sliderValue);

        if (whatValue == "Reverb_Basement")
            AkSoundEngine.SetRTPCValue("Aux_Basement_Reverb", sliderValue);

        if (whatValue == "Reverb_Wooden")
            AkSoundEngine.SetRTPCValue("Aux_Mansion_wooden_Reverb", sliderValue);

        if (whatValue == "Reverb_Demo")
            AkSoundEngine.SetRTPCValue("Aux_Cathedral_Reverb", sliderValue);
    }
}