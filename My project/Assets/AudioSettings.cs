using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Φορτώνει την αποθηκευμένη ένταση ή default = 1
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;

        // Συνδέει το Slider με τη μέθοδο αλλαγής έντασης
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume); // Αποθήκευση της ρύθμισης
        PlayerPrefs.Save();
    }
}
