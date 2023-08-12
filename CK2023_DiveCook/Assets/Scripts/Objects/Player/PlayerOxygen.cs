using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class PlayerOxygen : MonoBehaviour
{
    [SerializeField] private bool stopOxygenCycle = false;
    [SerializeField] private float _oxygenLevel = 100;
    [SerializeField] private float oxygenMax = 100;
    [SerializeField] private float oxygenDecrease = -0.334f;
    [SerializeField] public  float oxygenIncrease = 20f;
    [SerializeField] private Slider oxygenLevelSlider;
    [SerializeField] private float ticTime = 0.1f;

    private WaitForSeconds _tic;
    private PlayerControls _playerControls;

    [SerializeField] private List<AudioClip> hartSound;
    [SerializeField] private AudioSource audioSource;

    private bool slowHart = false;
    private bool fastHart = false;
    private float OxygenLevel
    {
        get { return _oxygenLevel; }
        set
        {
            audioSource.volume = 0.2f;
            _oxygenLevel = value;
            if (_oxygenLevel is > 30)
            {
                audioSource.Stop();
                audioSource.clip = hartSound[0];
                audioSource.volume = 0.2f;
                slowHart = false;
                fastHart = false;
            }
            else if (_oxygenLevel is <= 30 and > 25)
                audioSource.volume = 0.2f;
            else if (_oxygenLevel is <= 25 and > 20)
                audioSource.volume = 0.4f;
            else if (_oxygenLevel is <= 20 and > 15)
                audioSource.volume = 0.6f;
            else if (_oxygenLevel is <= 15 and > 10)
                audioSource.volume = 0.8f;
            else
                audioSource.volume = 1f;
            if (_oxygenLevel < 30 && !slowHart)
            {
                audioSource.Play();
                slowHart = true;
            }
        }
    }

    private void Start()
    {
        _playerControls = GetComponent<PlayerControls>();
        _tic = new WaitForSeconds(ticTime);
        StartCoroutine(OxygenCycle());
        audioSource.clip = hartSound[0];
        audioSource.loop = true;
    }

    private void Update()
    {
        if (!(_oxygenLevel < 10) || fastHart || !(audioSource.clip.length < audioSource.time + 0.1f)) return;
        audioSource.clip = hartSound[1];
        audioSource.Play();
        fastHart = true;
    }

    private IEnumerator OxygenCycle()
    {
        while (!stopOxygenCycle)
        {
            yield return _tic;
            AddOxygenLevel(oxygenDecrease);
            if (false)
                OxygenLevel = 100;
        }
    }
    public void AddOxygenLevel(float val)
    {
        if (OxygenLevel + val <= 0)
        {
            GameManager.Instance.GameOver();
            OxygenLevel = 0;
        }
        else
        {
            if (OxygenLevel + val > oxygenMax)
                OxygenLevel = oxygenMax;
            else
                OxygenLevel += val;
        }
        oxygenLevelSlider.value = OxygenLevel;
    }
}
