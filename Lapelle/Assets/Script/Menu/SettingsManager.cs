using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public Slider volumeSlider;

    void Start()
    {
        // Initialise le slider avec la valeur de volume actuelle
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f); // 1f = volume par défaut
        SetVolume(savedVolume);
        volumeSlider.value = savedVolume;
    }

    public void SetVolume(float volume)
    {
        // Cette formule convertit la valeur du slider en décibels (-80 à 0).
        // On utilise 0.0001 pour ne jamais atteindre -infini.
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);

        // Sauvegarde la préférence du joueur pour les prochaines sessions
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}