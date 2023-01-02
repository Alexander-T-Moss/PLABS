using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MenuMusic", volume);
        Debug.Log("Music volume set to: " + volume);
    }

    public void SetSoundFXVolume(float volume)
    {
        audioMixer.SetFloat("SimulationMusic", volume);
        Debug.Log("Sound FX volume set to: " + volume);
    }
}
