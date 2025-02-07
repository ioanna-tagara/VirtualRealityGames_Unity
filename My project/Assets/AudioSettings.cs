using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // �������� ��� ������������ ������ � default = 1
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;

        // ������� �� Slider �� �� ������ ������� �������
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume); // ���������� ��� ��������
        PlayerPrefs.Save();
    }
}
