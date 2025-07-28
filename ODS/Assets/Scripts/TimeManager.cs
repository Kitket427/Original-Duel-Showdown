using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private ColorAdjustments colorAdjustments;
    private ChromaticAberration chromaticAberration;
    [SerializeField] private AudioMixerGroup mixerGroup;
    private float time;
    [SerializeField] private GameObject pause;
    private AudioSource pauseMusic;
    private Control control;
    [SerializeField] private Slider[] sliders;
    void Start()
    {
        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out chromaticAberration);
        pauseMusic = pause.GetComponent<AudioSource>();
        control = InputManager.inputManager.control;
        control.Game.Enable();
        control.Game.Pause.started += Pause;
        if(PlayerPrefs.HasKey("Slow"))
        {
            sliders[0].value = PlayerPrefs.GetFloat("Slow");
            sliders[2].value = PlayerPrefs.GetFloat("Sfx");
            sliders[1].value = PlayerPrefs.GetFloat("Ost");
        }
        Cursor.visible = false;
    }
    void Update()
    {
        mixerGroup.audioMixer.SetFloat("Speed", Time.timeScale);
        mixerGroup.audioMixer.SetFloat("Speed2", Time.timeScale);
        if (Time.timeScale > 0)
        {
            time = pauseMusic.pitch = Time.timeScale;
        }
        if (Time.timeScale > 0 && time < 1)
        {
            Time.timeScale += Time.deltaTime;
        }
        else if (time > 1) Time.timeScale = 1;
        colorAdjustments.saturation.value = time*100 - 100;
        chromaticAberration.intensity.value = 1 - time;

        if (sliders[2].value > 0.02f) mixerGroup.audioMixer.SetFloat("Sfx", sliders[2].value * 30 - 30);
        else mixerGroup.audioMixer.SetFloat("Sfx", -80);

        if (sliders[1].value > 0.02f)
        {
            mixerGroup.audioMixer.SetFloat("Ost", sliders[1].value * 30 - 30);
            mixerGroup.audioMixer.SetFloat("Pause", sliders[1].value * 30 - 30);
        }
        else
        {
            mixerGroup.audioMixer.SetFloat("Ost", -80);
            mixerGroup.audioMixer.SetFloat("Pause", -80);
        }
        PlayerPrefs.SetFloat("Sfx", sliders[2].value);
        PlayerPrefs.SetFloat("Ost", sliders[1].value);
    }
    private void Pause(InputAction.CallbackContext callback)
    {
        if (Time.timeScale == 0)
        {
            pause.SetActive(false);
            Time.timeScale = time;
            Cursor.visible = false;
        }
        else
        {
            pause.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
    }
    public void GetDamage(float slow)
    {
        if (slow == 0 && Time.timeScale > sliders[0].value) Time.timeScale = sliders[0].value;
        else if (Time.timeScale > slow) Time.timeScale = slow;
        PlayerPrefs.SetFloat("Slow", sliders[0].value);
    }
}
