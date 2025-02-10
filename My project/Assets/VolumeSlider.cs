using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the Slider in the UI

    void Start()
    {
        // Set the slider's value to match the current AudioListener volume
        if (volumeSlider != null)
        {
            volumeSlider.value = Mathf.Clamp(AudioListener.volume, 0f, 1f);
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
        else
        {
            Debug.LogError("VolumeSlider is not assigned!");
        }
    }

    void OnVolumeChanged(float value)
    {
        // Clamp the volume value to the range [0, 1] to avoid distortion
        float clampedValue = Mathf.Clamp(value, 0f, 1f);
        AudioListener.volume = clampedValue;
        Debug.Log($"Volume set to: {clampedValue}");
    }
}
